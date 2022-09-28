using TMPro;
using UnityEngine;

public abstract class TextDisplay : MonoBehaviour
{
    [SerializeField] private Localization _localization;
    [SerializeField] protected TMP_Text _value;

    private void OnEnable() 
        => _localization.LanguageChanged += OnLanguageChanged;

    private void OnDisable() 
        => _localization.LanguageChanged -= OnLanguageChanged;

    protected abstract string GetRussianText();

    protected abstract string GetEnglishText();

    protected abstract string GetTurkishText();

    private void OnLanguageChanged(Language language)
    {
        _value.text = language switch
        {
            Language.Russian => GetRussianText(),
            Language.English => GetEnglishText(),
            Language.Turkish => GetTurkishText(),
            _ => GetEnglishText(),
        };
    }
}