using IJunior.TypedScenes;

public class FourthLevel : Level
{
    protected override void Restart(CompletedLevelsCounter counter)
        => Level_4.Load(counter);

    protected override void LoadNextLevel(CompletedLevelsCounter counter)
        => Level_5.Load(counter);

    protected override int GetIndex() => 4;
}