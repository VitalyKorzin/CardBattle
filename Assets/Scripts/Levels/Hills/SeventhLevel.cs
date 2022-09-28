using UnityEngine;
using IJunior.TypedScenes;

public class SeventhLevel : MonoBehaviour, ISceneLoadHandler<CompletedLevelsCounter>
{
    private CompletedLevelsCounter _counter;

    public void Restart() => Level_7.Load(_counter);

    public void LoadNextLevel() => Level_8.Load(_counter);

    public void OnSceneLoaded(CompletedLevelsCounter argument)
    {
        _counter = argument;
        _counter.Increase();
    }
}