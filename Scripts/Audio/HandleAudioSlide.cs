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

    Color MuteColor;
    private void Awake() {
        ColorUtility.TryParseHtmlString("#666666", out MuteColor);
    }

    private void OnEnable() {
        float loadedBGMVolume = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.BGMAmount;
        float loadedSFXVolume = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SFXAmount;
        BgmMutePressed = !GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.IsBGMMuted;
        SfxMutePressed = !GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.IsSFXMuted;
        BgmSlider.value = loadedBGMVolume;
        SfxSlider.value = loadedSFXVolume;
        GameManager.Instance.GlobalSoundManager.SetBGMVolume(BgmSlider.value);
        GameManager.Instance.GlobalSoundManager.SetSFXVolume(SfxSlider.value);
        HandleBGMMute();
        HandleSFXMute();
    }

    public void HandleBGMMute()
    {
        if (BgmMutePressed == false)
        {
            BgmMutePressed = true;
            GameManager.Instance.GlobalSoundManager.MuteBGM();
            BgmSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = MuteColor;
            BgmSlider.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().color = MuteColor;
        }
        else
        {
            BgmMutePressed = false;
            GameManager.Instance.GlobalSoundManager.UnmuteBGM();
            BgmSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.white;
            BgmSlider.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().color = Color.white;
        }
    }

    public void HandleSFXMute()
    {
        if (SfxMutePressed == false)
        {
            SfxMutePressed = true;
            GameManager.Instance.GlobalSoundManager.MuteSFX();
            SfxSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = MuteColor;
            SfxSlider.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().color = MuteColor;
        }
        else
        {
            SfxMutePressed = false;
            GameManager.Instance.GlobalSoundManager.UnmuteSFX();

            SfxSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.white;
            SfxSlider.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().color = Color.white;
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
