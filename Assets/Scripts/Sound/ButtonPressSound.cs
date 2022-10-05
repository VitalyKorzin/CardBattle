using UnityEngine;
using UnityEngine.UI;

public class ButtonPressSound : Sound
{
    [SerializeField] private Button[] _buttons;

    private void OnEnable()
    {
        foreach (Button button in _buttons)
            button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        foreach (Button button in _buttons)
            button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick() => Play();
}