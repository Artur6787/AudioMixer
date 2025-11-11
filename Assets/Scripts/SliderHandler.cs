using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderHandler : MonoBehaviour
{
    private const float LogMultiplier = 20f;
    private const float MinVolumeDb = -80f;
    private const float MinSliderValue = 0.0001f;

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    private void Start()
    {
        if (_slider != null)
        {
            _slider.onValueChanged.AddListener(OnSliderChanged);
            SetVolume(_slider.value);
        }
        else
        {
            SetVolume(1f);
        }
    }

    private void OnSliderChanged(float value)
    {
        SetVolume(value);
    }

    private float CalculateVolume(float level)
    {
        if (level > MinSliderValue)
            return Mathf.Log10(level) * LogMultiplier;
        else
            return MinVolumeDb;
    }

    private void SetVolume(float sliderValue)
    {
        if (_audioMixer == null || _audioMixerGroup == null)
            return;

        string parameterName = _audioMixerGroup.name;
        float volumeDb = CalculateVolume(sliderValue);
        _audioMixer.SetFloat(parameterName, volumeDb);
    }
}