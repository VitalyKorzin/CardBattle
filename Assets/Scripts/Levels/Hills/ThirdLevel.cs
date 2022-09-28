using UnityEngine;
using IJunior.TypedScenes;

public class ThirdLevel : MonoBehaviour, ISceneLoadHandler<CompletedLevelsCounter>
{
    private CompletedLevelsCounter _counter;

    public void Restart() => Level_3.Load(_counter);

    public void LoadNextLevel() => Level_4.Load(_counter);

    public void OnSceneLoaded(CompletedLevelsCounter argument)
    {
        _counter = argument;
        _counter.Increase();
    }
}