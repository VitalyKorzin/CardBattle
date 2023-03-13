using IJunior.TypedScenes;

public class EighthLevel : Level
{
    protected override void Restart(CompletedLevelsCounter counter)
        => Level_8.Load(counter);

    protected override void LoadNextLevel(CompletedLevelsCounter counter)
        => Level_9.Load(counter);

    protected override int GetIndex() => 8;
}