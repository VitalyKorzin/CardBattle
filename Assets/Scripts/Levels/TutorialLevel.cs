using UnityEngine;
using IJunior.TypedScenes;

public class TutorialLevel : MonoBehaviour
{
    private CompletedLevelsCounter _counter;

    private void Start() => _counter = new CompletedLevelsCounter();

    public void Restart() => IJunior.TypedScenes.TutorialLevel.Load(_counter);

    public void LoadNextLevel() => Level_1.Load(_counter);
}