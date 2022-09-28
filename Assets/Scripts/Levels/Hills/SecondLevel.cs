using IJunior.TypedScenes;
using UnityEngine;

public class SecondLevel : MonoBehaviour, ISceneLoadHandler<CompletedLevelsCounter>
{
    private CompletedLevelsCounter _counter;

    public void Restart() => Level_2.Load(_counter);

    public void LoadNextLevel() => Level_3.Load(_counter);

    public void OnSceneLoaded(CompletedLevelsCounter argument)
    {
        _counter = argument;
        _counter.Increase();
    }
}