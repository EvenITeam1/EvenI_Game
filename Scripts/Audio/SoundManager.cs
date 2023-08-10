using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using AYellowpaper.SerializedCollections;

public enum SOUND_TYPE { BGM, SFX, CONFLICTED }

public class SoundManager : MonoBehaviour
{
    public AudioMixer Mixer;
    public AudioSource[] AudioSources = new AudioSource[3];
    public SerializedDictionary<string, AudioClip> CachedBGMClips = new SerializedDictionary<string, AudioClip>();
    public SerializedDictionary<string, AudioClip> CachedSFXClips = new SerializedDictionary<string, AudioClip>();

    const float MUTE_VALUE = -80f;
    const float UNMUTE_VALUE = 0f;
    const string BGM_MuteParam = "BGM_MuteParam";
    const string BGM_VolumeParam = "BGM_VolumeParam";
    const string SFX_MuteParam = "SFX_MuteParam";
    const string SFX_VolumeParam = "SFX_VolumeParam";
    private void Awake()
    {
    }
    public void ClearAudioSetting()
    {
        AudioSources[0].Stop();
        AudioSources[0].clip = null;
        AudioSources[1].Stop();
        AudioSources[1].clip = null;
        AudioSources[0].pitch = 1f;
        AudioSources[1].pitch = 1f;
        UnmuteSFX();
        UnmuteBGM();
    }
    Sequence sequence;
    /////////////////////////////////////////////////////////////////////////////////
    #region BGM

    [ContextMenu("BGM 뮤트")]
    public void MuteBGM() { Mixer.SetFloat(BGM_MuteParam, MUTE_VALUE);}

    public void FadeOutBGM(float _duration)
    {
        DOVirtual.Float(1, 0, _duration, (E) => {
            Mixer.SetFloat(BGM_MuteParam, LinearToDecibel(E));
        });
    }
    public void FadeInBGM(float _duration)
    {
        Mixer.SetFloat(BGM_MuteParam, LinearToDecibel(0));
        DOVirtual.Float(0, 1, _duration, (E) => { 
            Mixer.SetFloat(BGM_MuteParam, LinearToDecibel(E)); 
        });
    }

    public void SetBGMVolume(float _volume){
        Mixer.SetFloat(BGM_VolumeParam, LinearToDecibel(_volume));
    }

    public float GetBGMVolume(){
        float res;
        Mixer.GetFloat(BGM_VolumeParam, out res);
        return DecibelToLinear(res);
    }


    [ContextMenu("BGM 언뮤트")]
    public void UnmuteBGM() { Mixer.SetFloat(BGM_MuteParam, UNMUTE_VALUE);}

    #endregion
    /////////////////////////////////////////////////////////////////////////////////
    
    #region SFX

    public void MuteSFX() { Mixer.SetFloat(SFX_MuteParam, MUTE_VALUE);}

    public void FadeOutSFX(float _duration)
    {
        DOVirtual.Float(1, 0, _duration, (E) => {
            Mixer.SetFloat(SFX_MuteParam, LinearToDecibel(E));
        });
    }
    public void FadeInSFX(float _duration)
    {
        Mixer.SetFloat(SFX_MuteParam, LinearToDecibel(0));
        DOVirtual.Float(0, 1, _duration, (E) => { 
            Mixer.SetFloat(SFX_MuteParam, LinearToDecibel(E)); 
        });
    }

    public void SetSFXVolume(float _volume){
        Mixer.SetFloat(SFX_VolumeParam, LinearToDecibel(_volume));
    }

    public float GetSFXVolume(){
        float res;
        Mixer.GetFloat(SFX_VolumeParam, out res);
        return DecibelToLinear(res);
    }


    [ContextMenu("SFX 언뮤트")]
    public void UnmuteSFX() { Mixer.SetFloat(SFX_MuteParam, UNMUTE_VALUE);}
    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    public void PlayBGMByString(string audioName)
    {
        if (CachedBGMClips.TryGetValue(audioName, out AudioClip audioClip))
        {
            if (AudioSources[(int)SOUND_TYPE.BGM].isPlaying) AudioSources[(int)SOUND_TYPE.BGM].Stop();
            AudioSources[(int)SOUND_TYPE.BGM].clip = audioClip;

            AudioSources[(int)SOUND_TYPE.BGM].Play();
        }
        else { return; }//throw new System.Exception("audioName과 일치하는 캐싱된 BGM이 없음, GameManager -> SoundManager에 BGM 캐싱되었는지 확인.");}
    }

    public void StopBGM()
    {
        if (AudioSources[(int)SOUND_TYPE.BGM].isPlaying)
        {
            AudioSources[(int)SOUND_TYPE.BGM].Stop();
            AudioSources[(int)SOUND_TYPE.BGM].clip = null;
        }
    }

    public void StopSFX(){
        if(AudioSources[(int)SOUND_TYPE.SFX].isPlaying || AudioSources[(int)SOUND_TYPE.CONFLICTED].isPlaying) {
            StartCoroutine(AsyncStopSFX());
        }
    }
    IEnumerator AsyncStopSFX(){
        MuteSFX();
        yield return new WaitUntil(() => {return !AudioSources[(int)SOUND_TYPE.SFX].isPlaying && !AudioSources[(int)SOUND_TYPE.CONFLICTED].isPlaying;});
        FadeInSFX(0.25f);
    }

    public void PlaySFXByString(string audioName)
    {
        if (CachedSFXClips.TryGetValue(audioName, out AudioClip audioClip))
        {
            AudioSources[(int)SOUND_TYPE.SFX].PlayOneShot(audioClip);
        }
        else { return; }//throw new System.Exception("audioName과 일치하는 캐싱된 SFX가 없음, GameManager -> SoundManager에 SFX 캐싱되었는지 확인.");}
    }

    public void PlayByClip(AudioClip audioClip, SOUND_TYPE type)
    {
        if (audioClip == null) { return; }//throw new System.Exception("audioClip에 Null을 넘겨 버림");}

        AudioSource targetSource;
        switch (type)
        {
            case SOUND_TYPE.BGM:
                {
                    targetSource = AudioSources[(int)SOUND_TYPE.BGM];
                    if (targetSource.isPlaying)
                        targetSource.Stop();

                    targetSource.clip = audioClip;

                    targetSource.Play();
                    break;
                }

            case SOUND_TYPE.SFX:
                {
                    targetSource = AudioSources[(int)SOUND_TYPE.SFX];
                    targetSource.PlayOneShot(audioClip); // 효과음 중첩 가능
                    break;
                }
            case SOUND_TYPE.CONFLICTED : 
                {
                    targetSource = AudioSources[(int)SOUND_TYPE.CONFLICTED];
                    targetSource.PlayOneShot(audioClip); // 효과음 중첩 가능
                    break;
                }
        }
    }

    public void StopByClip(AudioClip audioClip, SOUND_TYPE type)
    {
        if (audioClip == null) return;
        switch (type)
        {
            case SOUND_TYPE.BGM:
                {
                    AudioSources[(int)SOUND_TYPE.BGM].Stop();
                    break;
                }
            case SOUND_TYPE.SFX:
                {
                    //사실 의미 없다 모든 SFX는 OneShot으로 이루어 져 있기 때문에..
                    AudioSources[(int)SOUND_TYPE.SFX].Stop();
                    break;
                }
            case SOUND_TYPE.CONFLICTED : 
                {
                    AudioSources[(int)SOUND_TYPE.CONFLICTED].Stop();
                    break;
                }
        }
    }
    public float LinearToDecibel(float linear)
    {
        float dB;
        if (linear != 0)
            dB = 20f * Mathf.Log10(linear);
        else
            dB = -144.0f;
        return dB;
    }

    public float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20.0f);
        return linear;
    }
}
