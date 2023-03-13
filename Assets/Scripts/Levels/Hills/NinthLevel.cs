using IJunior.TypedScenes;

public class NinthLevel : Level
{
    protected override void Restart(CompletedLevelsCounter counter)
        => Level_9.Load(counter);

    protected override void LoadNextLevel(CompletedLevelsCounter counter)
        => Level_10.Load(counter);

    protected override int GetIndex() => 9;
}