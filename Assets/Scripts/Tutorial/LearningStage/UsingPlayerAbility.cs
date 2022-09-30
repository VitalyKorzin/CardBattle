using UnityEngine;

public class UsingPlayerAbility : LearningStage
{
    [SerializeField] private PlayerAbility _playerAbility;

    private void OnEnable()
    {
        _playerAbility.Used += OnPlayerAbilityUsed;
        Time.timeScale = 0f;
    }

    private void OnDisable() 
        => _playerAbility.Used += OnPlayerAbilityUsed;

    private void OnPlayerAbilityUsed()
    {
        Time.timeScale = 1f;
        Finish();
    }
}