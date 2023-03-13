using IJunior.TypedScenes;

public class TenthLevel : Level
{
    protected override void Restart(CompletedLevelsCounter counter)
        => Level_10.Load(counter);

    protected override void LoadNextLevel(CompletedLevelsCounter counter) { }

    protected override int GetIndex() => 10;
}