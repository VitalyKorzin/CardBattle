using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonPressing : LearningStage
{
    [SerializeField] private Button _continueButton;

    protected virtual void OnEnable()
        => _continueButton.onClick.AddListener(OnContinueButtonClick);

    protected virtual void OnDisable()
        => _continueButton.onClick.RemoveListener(OnContinueButtonClick);

    private void OnContinueButtonClick() => Finish();
}