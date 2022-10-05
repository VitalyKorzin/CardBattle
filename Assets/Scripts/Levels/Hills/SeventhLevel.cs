using IJunior.TypedScenes;

public class SeventhLevel : Level
{
    protected override void Restart(CompletedLevelsCounter counter)
        => Level_7.Load(counter);

    protected override void LoadNextLevel(CompletedLevelsCounter counter)
        => Level_8.Load(counter);

    protected override int GetIndex() => 7;
}