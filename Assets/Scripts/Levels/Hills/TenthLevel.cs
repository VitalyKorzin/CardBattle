using UnityEngine;
using IJunior.TypedScenes;

public class TenthLevel : MonoBehaviour, ISceneLoadHandler<CompletedLevelsCounter>
{
    private CompletedLevelsCounter _counter;

    public void Restart() => Level_10.Load(_counter);

    public void LoadNextLevel()
    {

    }

    public void OnSceneLoaded(CompletedLevelsCounter argument)
    {
        _counter = argument;
        _counter.Increase();
    }
}