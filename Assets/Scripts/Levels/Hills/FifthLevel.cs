using IJunior.TypedScenes;

public class FifthLevel : Level
{
    protected override void Restart(CompletedLevelsCounter counter)
        => Level_5.Load(counter);

    protected override void LoadNextLevel(CompletedLevelsCounter counter)
        => Level_6.Load(counter);

    protected override int GetIndex() => 5;
}