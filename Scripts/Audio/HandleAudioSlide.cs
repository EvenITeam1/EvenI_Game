using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleAudioSlide : MonoBehaviour
{

    const float MUTE_VALUE = -80f;
    const float UNMUTE_VALUE = 0f;
    const string BGM_MuteParam = "BGM_MuteParam";
    const string BGM_VolumeParam = "BGM_VolumeParam";
    const string SFX_MuteParam = "SFX_MuteParam";
    const string SFX_VolumeParam = "SFX_VolumeParam";
    
    public Button BgmMute;
    public bool BgmMutePressed = false;
    public Button SfxMute;
    public bool SfxMutePressed = false;
    public Slider BgmSlider;
    public Slider SfxSlider;

    private void OnEnable() {
        float loadedBGMVolume = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.BGMAmount;
        float loadedSFXVolume = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SFXAmount;
        GameManager.Instance.GlobalSoundManager.SetBGMVolume(loadedBGMVolume);
        GameManager.Instance.GlobalSoundManager.SetSFXVolume(loadedSFXVolume);
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
        GameManager.Instance.GlobalSoundManager.SetBGMVolume((BgmSlider.value / BgmSlider.maxValue));
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.BGMAmount = GameManager.Instance.GlobalSoundManager.GetBGMVolume();
    }
    public void HandleSfxSliderChange(){
        GameManager.Instance.GlobalSoundManager.SetSFXVolume((SfxSlider.value / SfxSlider.maxValue));
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SFXAmount = GameManager.Instance.GlobalSoundManager.GetSFXVolume();
    }
}
