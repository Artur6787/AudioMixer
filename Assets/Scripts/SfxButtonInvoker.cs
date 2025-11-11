using UnityEngine;
using UnityEngine.UI;

public class SfxButtonInvoker : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioSource _sfxSource;

    private void Start()
    {
         _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
         _button.onClick.RemoveListener(OnButtonClicked);

    }

    private void OnButtonClicked()
    {
         _sfxSource.Play();
    }
}