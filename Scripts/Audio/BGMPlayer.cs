using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public AudioClip audioClip;
    private void Start() {
        GameManager.Instance.GlobalSoundManager.PlayByClip(audioClip, SOUND_TYPE.BGM);
    }
}
