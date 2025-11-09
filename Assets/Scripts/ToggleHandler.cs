using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ToggleHandler : MonoBehaviour
{
    private const float MinVolumeDb = -80f;
    private const float MinSliderValue = 0.0001f;
    private const float LogMultiplier = 20f;

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Button _muteToggleButton;
    [SerializeField] private AudioSource _backgroundMusicSource;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private AudioMixerGroup _masterGroup;

    private bool _isMuted = false;

    private void Start()
    {
        _muteToggleButton.onClick.AddListener(ToggleMute);

        if (_backgroundMusicSource != null)
        {
            _backgroundMusicSource.loop = true;
            _backgroundMusicSource.Play();
        }
    }

    private void ToggleMute()
    {
        _isMuted = !_isMuted;
        string parameterName;

        if (_masterGroup != null)
        {
            parameterName = _masterGroup.name;
        }
        else
        {
            parameterName = "MasterVolume";
        }

        if (_isMuted == true)
        {
            _audioMixer.SetFloat(parameterName, MinVolumeDb);
        }
        else
        {
            float sliderValue;

            if (_masterSlider != null && _masterSlider.value > MinSliderValue)
            {
                sliderValue = _masterSlider.value;
            }
            else
            {
                sliderValue = MinSliderValue;
            }

            float volumeDb = Mathf.Log10(sliderValue) * LogMultiplier;
            _audioMixer.SetFloat(parameterName, volumeDb);
        }
    }
}