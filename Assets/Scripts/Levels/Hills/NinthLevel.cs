using UnityEngine;
using IJunior.TypedScenes;

public class NinthLevel : MonoBehaviour, ISceneLoadHandler<CompletedLevelsCounter>
{
    private CompletedLevelsCounter _counter;

    public void Restart() => Level_9.Load(_counter);

    public void LoadNextLevel() => Level_10.Load(_counter);

    public void OnSceneLoaded(CompletedLevelsCounter argument)
    {
        _counter = argument;
        _counter.Increase();
    }
}