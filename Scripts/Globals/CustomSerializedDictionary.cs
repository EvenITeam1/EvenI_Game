using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AYellowpaper.SerializedCollections
{
    public class CustomSerializedDictionary : MonoBehaviour
    {
        [SerializedDictionary("AudioCip Name", "AudioClip")]
        public SerializedDictionary<string, AudioClip> DictionaryAudioClipByName;
    }
}