using IJunior.TypedScenes;

public class SixthLevel : Level
{
    protected override void Restart(CompletedLevelsCounter counter)
        => Level_6.Load(counter);

    protected override void LoadNextLevel(CompletedLevelsCounter counter)
        => Level_7.Load(counter);

    protected override int GetIndex() => 6;
}