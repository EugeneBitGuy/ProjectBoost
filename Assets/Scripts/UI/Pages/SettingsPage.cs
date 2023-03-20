using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Buttons
{
    public class SettingsPage : UIPage
    {
        [SerializeField] private AudioMixer mixer = null;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private void Start()
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
        }

        public void ChangeMusicVolume(float sliderValue)
        {
            mixer.SetFloat("MusicValue", sliderValue); 
            PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        }
        
        public void ChangeSfxVolume(float sliderValue)
        {
            mixer.SetFloat("SfxValue", sliderValue); 
            PlayerPrefs.SetFloat("SfxVolume", sliderValue);
        }
    }
}