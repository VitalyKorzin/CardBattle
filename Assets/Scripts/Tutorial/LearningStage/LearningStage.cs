using UnityEngine;
using UnityEngine.Events;

public abstract class LearningStage : MonoBehaviour
{
    [SerializeField] private LearningStage _nextStage;

    public LearningStage NextStage => _nextStage;

    public event UnityAction Ended;

    protected void Finish() => Ended?.Invoke();
}