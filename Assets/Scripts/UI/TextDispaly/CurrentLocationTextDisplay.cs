using UnityEngine;

public class CurrentLocationTextDisplay : TextDisplay
{
    [SerializeField] private string _russianName;
    [SerializeField] private string _englishName;
    [SerializeField] private string _turkishName;

    protected override string GetEnglishText() => _englishName;

    protected override string GetRussianText() => _russianName;

    protected override string GetTurkishText() => _turkishName;
}