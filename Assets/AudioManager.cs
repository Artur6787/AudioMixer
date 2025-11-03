using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource[] sfxSources = new AudioSource[3];
    [SerializeField] private Button[] playSfxButtons = new Button[3];
    [SerializeField] private Button muteToggleButton;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private bool isMuted = false;
    private const float LogMultiplier = 20f;
    private const float MinVolumeDb = -80f;
    private const float MinSliderValue = 0.0001f;

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);

        for (int i = 0; i < playSfxButtons.Length; i++)
        {
            int index = i;
            playSfxButtons[i].onClick.AddListener(() => PlaySfx(index));
        }

        muteToggleButton.onClick.AddListener(ToggleMute);

        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.loop = true;

            if (backgroundMusicSource != null)
            {
                backgroundMusicSource.Play();
            }
        }

        SetMasterVolume(masterSlider.value);
        SetSfxVolume(sfxSlider.value);
        SetMusicVolume(musicSlider.value);
    }

    private float CalculateVolume(float level)
    {
        if (level > MinSliderValue)
        {
            return Mathf.Log10(level) * LogMultiplier;
        }
        else
        {
            return MinVolumeDb;
        }
    }

    private void SetMasterVolume(float level)
    {
        float volume = CalculateVolume(level);
        audioMixer.SetFloat("MasterVolume", volume);
    }

    private void SetSfxVolume(float level)
    {
        float volume = CalculateVolume(level);
        audioMixer.SetFloat("SfxVolume", volume);
    }

    private void SetMusicVolume(float level)
    {
        float volume = CalculateVolume(level);
        audioMixer.SetFloat("MusicVolume", volume);
    }

    private void PlaySfx(int index)
    {
        if (index >= 0 && index < sfxSources.Length)
        {
            if (sfxSources[index] != null)
            {
                sfxSources[index].Play();
            }
        }
    }

    private void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            audioMixer.SetFloat("MasterVolume", MinVolumeDb);
        }
        else
        {
            SetMasterVolume(masterSlider.value);
        }
    }
}