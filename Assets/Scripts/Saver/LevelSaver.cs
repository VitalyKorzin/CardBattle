public class LevelSaver : Saver
{
    private const string CurrentLevel = nameof(CurrentLevel);

    private readonly int _defaultIndex = 0;

    public void Save(int index)
        => SaveIntegerValue(CurrentLevel, index);

    public int LoadLevelIndex()
    {
        if (TryLoadIntegerValue(CurrentLevel, out int index))
            return index;
        else
            return _defaultIndex;
    }
}