using IJunior.TypedScenes;

public class ThirdLevel : Level
{
    protected override void Restart(CompletedLevelsCounter counter) 
        => Level_3.Load(counter);

    protected override void LoadNextLevel(CompletedLevelsCounter counter)
        => Level_4.Load(counter);

    protected override int GetIndex() => 3;
}