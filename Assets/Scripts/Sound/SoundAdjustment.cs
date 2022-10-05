using UnityEngine;

public class SoundAdjustment : MonoBehaviour
{
    [SerializeField] private SdkYandex _sdkYandex;
    [SerializeField] private SoundButton _soundButton;

    private void OnEnable()
    {
        _sdkYandex.VideoAdOpened += OnVideoAdOpened;
        _sdkYandex.VideoAdClosed += OnVideoAdClosed;
        _soundButton.Click += SetVolume;
    }

    private void OnDisable()
    {
        _sdkYandex.VideoAdOpened -= OnVideoAdOpened;
        _sdkYandex.VideoAdClosed -= OnVideoAdClosed;
        _soundButton.Click -= SetVolume;
    }

    private void OnVideoAdOpened()
    {
        if (_soundButton.IsSoundOn)
            SetVolume(false);
    }

    private void OnVideoAdClosed()
    {
        if (_soundButton.IsSoundOn)
            SetVolume(true);
    }

    private void SetVolume(bool isActive)
        => AudioListener.volume = isActive ? 1 : 0;
}