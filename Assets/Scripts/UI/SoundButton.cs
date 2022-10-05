using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SoundButton : MonoBehaviour
{
    [SerializeField] private Sprite _soundOff;
    [SerializeField] private Sprite _soundOn;

    private Button _button;

    public bool IsSoundOn { get; private set; } = true;

    public event UnityAction<bool> Click;

    private void OnEnable() 
        => _button.onClick.AddListener(OnButtonClick);

    private void OnDisable()
        => _button.onClick.RemoveListener(OnButtonClick);

    private void Awake()
        => _button = GetComponent<Button>();

    private void OnButtonClick()
    {
        IsSoundOn = !IsSoundOn;
        Click?.Invoke(IsSoundOn);
        _button.image.sprite = IsSoundOn ? _soundOn : _soundOff;
    }
}