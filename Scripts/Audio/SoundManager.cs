using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using AYellowpaper.SerializedCollections;

public enum SOUND_TYPE {BGM, SFX}

public class SoundManager : MonoBehaviour
{
    public AudioMixer Mixer;
    public AudioSource[] AudioSources = new AudioSource[2];
    public SerializedDictionary<string, AudioClip> CachedBGMClips = new SerializedDictionary<string, AudioClip>();
    public SerializedDictionary<string, AudioClip> CachedSFXClips = new SerializedDictionary<string, AudioClip>();

    const float MUTE_VALUE          = -80f;
    const float UNMUTE_VALUE        = 0f;
    const string BGM_MuteParam      = "BGM_MuteParam";
    const string BGM_VolumeParam    = "BGM_VolumeParam";
    const string SFX_MuteParam      = "SFX_MuteParam";
    const string SFX_VolumeParam    = "SFX_VolumeParam";

    Sequence sequence;

    [ContextMenu("BGM 뮤트")]
    public void MuteBGM(){ Mixer.SetFloat(BGM_MuteParam, MUTE_VALUE); AudioSources[(int)SOUND_TYPE.BGM].mute = true;}
    
    public void FadeOutBGM(float _duration) {
        DOVirtual.Float(1, 0, _duration, (E) => {AudioSources[(int)SOUND_TYPE.BGM].volume = E;});
    }
    public void FadeInBGM(float _duration) {
        AudioSources[(int)SOUND_TYPE.BGM].volume = -1;
        DOVirtual.Float(0, 1, _duration, (E) => {AudioSources[(int)SOUND_TYPE.BGM].volume = E;});
    }


    [ContextMenu("BGM 언뮤트")]
    public void UnmuteBGM(){ Mixer.SetFloat(BGM_MuteParam, UNMUTE_VALUE); AudioSources[(int)SOUND_TYPE.BGM].mute = false;}

    [ContextMenu("SFX 뮤트")]
    public void MuteSFX(){ Mixer.SetFloat(SFX_MuteParam, MUTE_VALUE); AudioSources[(int)SOUND_TYPE.SFX].mute = true;}
        public void FadeOutSFX(float _duration) {
        DOVirtual.Float(1, 0, _duration, (E) => {AudioSources[(int)SOUND_TYPE.SFX].volume = E;});
    }
    public void FadeInSFX(float _duration) {
        AudioSources[(int)SOUND_TYPE.SFX].volume = -1;
        DOVirtual.Float(0, 1, _duration, (E) => {AudioSources[(int)SOUND_TYPE.SFX].volume = E;});
    }
    [ContextMenu("SFX 언뮤트")]
    public void UnmuteSFX(){ Mixer.SetFloat(SFX_MuteParam, UNMUTE_VALUE); AudioSources[(int)SOUND_TYPE.SFX].mute = false;}

    public void PlayBGMByString(string audioName){
        if(CachedBGMClips.TryGetValue(audioName, out AudioClip audioClip)) 
        {
	        if (AudioSources[(int)SOUND_TYPE.BGM].isPlaying) AudioSources[(int)SOUND_TYPE.BGM].Stop();
	        AudioSources[(int)SOUND_TYPE.BGM].clip = audioClip;
	        AudioSources[(int)SOUND_TYPE.BGM].Play();                
        }
        else {return;}//throw new System.Exception("audioName과 일치하는 캐싱된 BGM이 없음, GameManager -> SoundManager에 BGM 캐싱되었는지 확인.");}
    }
    
    public void StopBGM(){
        if(AudioSources[(int)SOUND_TYPE.BGM].isPlaying) {
            AudioSources[(int)SOUND_TYPE.BGM].Stop();
            AudioSources[(int)SOUND_TYPE.BGM].clip = null;
        }
    }

    public void PlaySFXByString(string audioName){
        if(CachedSFXClips.TryGetValue(audioName, out AudioClip audioClip)) {
		    AudioSources[(int)SOUND_TYPE.SFX].PlayOneShot(audioClip);
        }
        else {return;}//throw new System.Exception("audioName과 일치하는 캐싱된 SFX가 없음, GameManager -> SoundManager에 SFX 캐싱되었는지 확인.");}
    }

    public void PlayByClip(AudioClip audioClip, SOUND_TYPE type)
	{
        if (audioClip == null) {return;}//throw new System.Exception("audioClip에 Null을 넘겨 버림");}

        AudioSource targetSource;
        switch (type) 
        {
            case SOUND_TYPE.BGM : 
            {
                targetSource = AudioSources[(int)SOUND_TYPE.BGM];
			    if (targetSource.isPlaying)
			    	targetSource.Stop();

			    targetSource.clip = audioClip;
			    targetSource.Play();                
                break;
            }

            case SOUND_TYPE.SFX : 
            {
			    targetSource = AudioSources[(int)SOUND_TYPE.SFX];
			    targetSource.PlayOneShot(audioClip); // 효과음 중첩 가능
                break;
            }
        }
	}

    public void StopByClip(AudioClip audioClip, SOUND_TYPE type) {
        if (audioClip == null) return;
        switch (type) 
        {
            case SOUND_TYPE.BGM : 
            {
                AudioSources[(int)SOUND_TYPE.BGM].Stop();
                break;
            }
            case SOUND_TYPE.SFX :
            {
                //사실 의미 없다 모든 SFX는 OneShot으로 이루어 져 있기 때문에..
                AudioSources[(int)SOUND_TYPE.SFX].Stop();
                break;
            }
        } 
    }
}
