public class CardsDeckSaver : Saver
{
    private readonly int _defaultCount = 0;
    private readonly string _defaultCardName = "";

    public void SaveSelectedCard(string slotName, string cardName)
        => SaveStringValue(slotName, cardName);

    public void SaveCardsSet(string cardName, int count)
        => SaveIntegerValue(cardName, count);

    public string LoadSelectedCard(string slotName)
    {
        if (TryLoadStringValue(slotName, out string cardName))
            return cardName;
        else
            return _defaultCardName;
    }

    public int LoadCardsSet(string cardName)
    {
        if (TryLoadIntegerValue(cardName, out int count))
            return count;
        else
            return _defaultCount;
    }
}