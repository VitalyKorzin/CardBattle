using UnityEngine;
using IJunior.TypedScenes;

public class SixthLevel : MonoBehaviour, ISceneLoadHandler<CompletedLevelsCounter>
{
    private CompletedLevelsCounter _counter;

    public void Restart() => Level_6.Load(_counter);

    public void LoadNextLevel() => Level_7.Load(_counter);

    public void OnSceneLoaded(CompletedLevelsCounter argument)
    {
        _counter = argument;
        _counter.Increase();
    }
}