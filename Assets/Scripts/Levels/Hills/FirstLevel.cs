using UnityEngine;
using IJunior.TypedScenes;

public class FirstLevel : MonoBehaviour, ISceneLoadHandler<CompletedLevelsCounter>
{
    private CompletedLevelsCounter _counter;

    private void Start() => _counter = new CompletedLevelsCounter();

    public void Restart() => Level_1.Load(_counter);

    public void LoadNextLevel() => Level_2.Load(_counter);

    public void OnSceneLoaded(CompletedLevelsCounter argument)
    {
        _counter = argument;
        _counter.Increase();
    }
}