using UnityEngine;

public class PlayerAbilityDisplay : MonoBehaviour
{
    [SerializeField] private StartFightAnnunciator _annunciator;
    [SerializeField] private HeroesSquad _heroesSquad;
    [SerializeField] private EnemiesSquad _enemiesSquad;
    [SerializeField] private PlayerAbility _value;

    private bool _enemiesDied;

    private void OnEnable()
    {
        _annunciator.FightStarted += OnFightStarted;
        _heroesSquad.Died += OnSquadDied;
        _enemiesSquad.Died += OnSquadDied;
    }

    private void OnDisable()
    {
        _annunciator.FightStarted -= OnFightStarted;
        _heroesSquad.Died -= OnSquadDied;
        _enemiesSquad.Died -= OnSquadDied;
    }

    private void OnFightStarted()
    {
        if (_enemiesDied == false)
            _value.gameObject.SetActive(true);
    }

    private void OnSquadDied()
    {
        _value.gameObject.SetActive(false);
        _enemiesDied = true;
    }
}