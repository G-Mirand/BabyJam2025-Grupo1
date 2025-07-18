using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider MasterSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume") || PlayerPrefs.HasKey("masterVolume"))
        {
            loadAudioSettings();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
        }
    }

    public void SetMusicVolume()
    {
        float musicVolume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log(musicVolume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }
    public void SetMasterVolume()
    {
        float masterVolume = MasterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log(masterVolume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
    }


    private void loadAudioSettings()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");

        SetMusicVolume();
        SetMasterVolume();
    }
}
