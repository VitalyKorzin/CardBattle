using IJunior.TypedScenes;

public class FirstLevel : Level
{
    protected override void Restart(CompletedLevelsCounter counter)
        => Level_1.Load(counter);

    protected override void LoadNextLevel(CompletedLevelsCounter counter)
        => Level_2.Load(counter);

    protected override int GetIndex() => 1;
}