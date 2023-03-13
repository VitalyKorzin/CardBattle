using IJunior.TypedScenes;

public class TutorialLevel : Level
{
    protected override void Restart(CompletedLevelsCounter counter)
        => IJunior.TypedScenes.TutorialLevel.Load(counter);

    protected override void LoadNextLevel(CompletedLevelsCounter counter)
        => Level_1.Load(counter);

    protected override int GetIndex() => 0;
}