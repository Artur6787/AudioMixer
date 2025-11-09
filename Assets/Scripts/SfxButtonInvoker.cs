using UnityEngine;
using UnityEngine.UI;

public class SfxButtonInvoker : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private AudioSource sfxSource;

    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClicked);
        }
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }
    }

    private void OnButtonClicked()
    {
        if (sfxSource != null)
        {
            sfxSource.Play();
        }
    }
}