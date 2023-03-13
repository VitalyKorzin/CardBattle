public class ScoreSaver : Saver
{
    private const string Score = nameof(Score);

    private readonly int _defaultValue = 0;

    public void Save(int score)
        => SaveIntegerValue(Score, score);

    public int LoadScore()
    {
        if (TryLoadIntegerValue(Score, out int score))
            return score;
        else
            return _defaultValue;
    }
}