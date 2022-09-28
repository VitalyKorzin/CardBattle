using UnityEngine;

public class CurrentLevelTextDisplay : TextDisplay
{
    [Min(0)]
    [SerializeField] private int _number;

    protected override string GetEnglishText() => GetText("Level");

    protected override string GetRussianText() => GetText("Уровень");

    protected override string GetTurkishText() => GetText("Seviye");

    private string GetText(string translation) => translation + " " + _number;
}