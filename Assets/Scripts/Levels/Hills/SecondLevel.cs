using IJunior.TypedScenes;

public class SecondLevel : Level
{
    protected override void Restart(CompletedLevelsCounter counter)
        => Level_2.Load(counter);

    protected override void LoadNextLevel(CompletedLevelsCounter counter)
        => Level_3.Load(counter);

    protected override int GetIndex() => 2;
}