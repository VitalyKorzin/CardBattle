using System;

[Serializable]
public class CompletedLevelsCounter
{
    public int Value { get; private set; }
    public bool CanShowInterstitialAd => Value == 3;

    public void Increase() => Value++;

    public void Clear() => Value = 0;
}