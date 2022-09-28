using UnityEngine;
using IJunior.TypedScenes;

public class FirstLevel : MonoBehaviour, ISceneLoadHandler<CompletedLevelsCounter>
{
    [SerializeField] private SdkYandex _sdkYandex;
    [SerializeField] private Leaderboard _leaderboard;

    private readonly int _number = 1;

    private CompletedLevelsCounter _counter;

    private void OnEnable()
        => _sdkYandex.Initialized += OnSdkYandexInitialized;

    private void OnDisable()
        => _sdkYandex.Initialized -= OnSdkYandexInitialized;

    private void Start() => _counter = new CompletedLevelsCounter();

    public void Restart() => Level_1.Load(_counter);

    public void LoadNextLevel() => Level_2.Load(_counter);

    public void OnSceneLoaded(CompletedLevelsCounter argument)
    {
        _counter = argument;
        _counter.Increase();
    }

    private void OnSdkYandexInitialized() => _leaderboard.Set(_number);
}