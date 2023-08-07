using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleAudioSlide : MonoBehaviour
{
    public Button BgmMute;
    public bool BgmMutePressed = false;
    public Button SfxMute;
    public bool SfxMutePressed = false;
    public Slider BgmSlider;
    public Slider SfxSlider;

    private void OnEnable() {
        BgmSlider.value = GameManager.Instance.GlobalSoundManager.AudioSources[(int)SOUND_TYPE.BGM].volume;
        SfxSlider.value = GameManager.Instance.GlobalSoundManager.AudioSources[(int)SOUND_TYPE.SFX].volume;
    }

    public void HandleBGMMute()
    {
        if (BgmMutePressed == false)
        {
            BgmMutePressed = true;
            GameManager.Instance.GlobalSoundManager.MuteBGM();
        }
        else
        {
            BgmMutePressed = false;
            GameManager.Instance.GlobalSoundManager.UnmuteBGM();
        }
    }

    public void HandleSFXMute()
    {
        if (SfxMutePressed == false)
        {
            SfxMutePressed = true;
            GameManager.Instance.GlobalSoundManager.MuteSFX();
        }
        else
        {
            SfxMutePressed = false;
            GameManager.Instance.GlobalSoundManager.UnmuteSFX();
        }
    }

    public void HandleBgmSliderChange(){
        GameManager.Instance.GlobalSoundManager.AudioSources[(int)SOUND_TYPE.BGM].volume = BgmSlider.value / BgmSlider.maxValue;
    }
    public void HandleSfxSliderChange(){
        GameManager.Instance.GlobalSoundManager.AudioSources[(int)SOUND_TYPE.SFX].volume = SfxSlider.value / SfxSlider.maxValue;
    }
}
