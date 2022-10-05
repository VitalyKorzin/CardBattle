using UnityEngine;

[RequireComponent(typeof(WinScreen))]
public class VictorySound : Sound
{
    private WinScreen _screen;

    private void OnEnable() 
        => _screen.Showed += OnScreenShowed;

    private void OnDisable() 
        => _screen.Showed -= OnScreenShowed;

    protected override void Awake()
    {
        base.Awake();
        _screen = GetComponent<WinScreen>();
    }

    private void OnScreenShowed() => Play();
}