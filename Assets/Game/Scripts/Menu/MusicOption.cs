using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicOption : MonoBehaviour
{
    public AudioMixerGroup audioMixer;
    public Toggle MusicToggle;
    public Toggle SoundToggle;


    private void Start()
    {
        MusicToggle.isOn = PlayerPrefs.GetInt("MusicEnable") == 1;
        SoundToggle.isOn = PlayerPrefs.GetInt("SoundEnable") == 1;
    }

    public void ToggleMusic(bool enable)
    {
        if (enable)
        {
            audioMixer.audioMixer.SetFloat("MusicVolume", 0);
        }
        else
        {
            audioMixer.audioMixer.SetFloat("MusicVolume", -80);
        }

        PlayerPrefs.SetInt("MusicEnable", enable ? 1 : 0);
    }

    public void ToggleSound(bool enable)
    {
        if (enable)
        {
            audioMixer.audioMixer.SetFloat("EffectsVolume", 0);
        }
        else
        {
            audioMixer.audioMixer.SetFloat("EffectsVolume", -80);
        }
        PlayerPrefs.SetInt("SoundEnable", enable ? 1 : 0);
    }
}
