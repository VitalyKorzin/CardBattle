using System;
using IJunior.TypedScenes;
using UnityEngine;

public abstract class Level : MonoBehaviour, ISceneLoadHandler<CompletedLevelsCounter>
{
    [SerializeField] private LevelSaver _saver;

    private CompletedLevelsCounter _counter;

    public int Index => GetIndex();

    private void Start() 
        => _saver.SaveLevel(Index);

    public void Initialize(CompletedLevelsCounter counter)
    {
        if (counter == null)
            throw new ArgumentNullException(nameof(counter));

        _counter = counter;
    }

    public void Restart() => Restart(_counter);

    public void LoadNextLevel() => LoadNextLevel(_counter);

    public void OnSceneLoaded(CompletedLevelsCounter argument)
    {
        _counter = argument;
        _counter.Increase();
    }

    protected abstract void Restart(CompletedLevelsCounter counter);

    protected abstract void LoadNextLevel(CompletedLevelsCounter counter);

    protected abstract int GetIndex();
}