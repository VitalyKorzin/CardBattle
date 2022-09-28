using IJunior.TypedScenes;
using UnityEngine;

public class SecondLevel : MonoBehaviour, ISceneLoadHandler<CompletedLevelsCounter>
{
    [SerializeField] private SdkYandex _sdkYandex;
    [SerializeField] private Leaderboard _leaderboard;

    private readonly int _number = 2;

    private CompletedLevelsCounter _counter;

    private void OnEnable()
        => _sdkYandex.Initialized += OnSdkYandexInitialized;

    private void OnDisable()
        => _sdkYandex.Initialized -= OnSdkYandexInitialized;

    public void Restart() => Level_2.Load(_counter);

    public void LoadNextLevel() => Level_3.Load(_counter);

    public void OnSceneLoaded(CompletedLevelsCounter argument)
    {
        _counter = argument;
        _counter.Increase();
    }

    private void OnSdkYandexInitialized() => _leaderboard.Set(_number);
}