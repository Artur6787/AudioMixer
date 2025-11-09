using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderHandler : MonoBehaviour
{
    private const float LogMultiplier = 20f;
    private const float MinVolumeDb = -80f;
    private const float MinSliderValue = 0.0001f;

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private AudioMixerGroup _masterGroup;
    [SerializeField] private AudioMixerGroup _sfxGroup;
    [SerializeField] private AudioMixerGroup _musicGroup;

    private void Start()
    {
        _masterSlider.onValueChanged.AddListener(OnMasterSliderChanged);
        _sfxSlider.onValueChanged.AddListener(OnSfxSliderChanged);
        _musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        SetVolume(_masterGroup, _masterSlider.value);
        SetVolume(_sfxGroup, _sfxSlider.value);
        SetVolume(_musicGroup, _musicSlider.value);
    }

    private void OnMasterSliderChanged(float level)
    {
        SetVolume(_masterGroup, level);
    }

    private void OnSfxSliderChanged(float level)
    {
        SetVolume(_sfxGroup, level);
    }

    private void OnMusicSliderChanged(float level)
    {
        SetVolume(_musicGroup, level);
    }

    private float CalculateVolume(float level)
    {
        return level > MinSliderValue ? Mathf.Log10(level) * LogMultiplier : MinVolumeDb;
    }

    private void SetVolume(AudioMixerGroup group, float sliderValue)
    {
        if (group == null) 
            return;

        string parameterName = group.name;
        float volumeDb = CalculateVolume(sliderValue);
        _audioMixer.SetFloat(parameterName, volumeDb);
    }
}