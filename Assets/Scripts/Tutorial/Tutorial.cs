using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _startWalletBalance;
    [SerializeField] private WalletSaver _walletSaver;
    [SerializeField] private LearningStage _firstStage;
    [SerializeField] private StartFightAnnunciator _annunciator;
    [SerializeField] private UsingPlayerAbility _usingPlayerAbility;

    private LearningStage _previousStage;
    private LearningStage _currentStage;

    private void OnEnable()
    {
        _annunciator.FightStarted += OnFightStarted;
        _currentStage = _firstStage;
        ActiveCurrentStage();
    }

    private void OnDisable() 
        => _annunciator.FightStarted -= OnFightStarted;

    private void Awake()
        => _walletSaver.SaveBalance(_startWalletBalance);

    private void OnStageEnded()
    {
        _currentStage.Ended -= OnStageEnded;

        if (_currentStage.NextStage == null)
        {
            _currentStage.gameObject.SetActive(false);
            return;
        }

        _previousStage = _currentStage;
        _currentStage = _previousStage.NextStage;
        _previousStage.gameObject.SetActive(false);
        ActiveCurrentStage();
    }

    private void ActiveCurrentStage()
    {
        _currentStage.gameObject.SetActive(true);
        _currentStage.Ended += OnStageEnded;
    }

    private void OnFightStarted()
    {
        _currentStage = _usingPlayerAbility;
        ActiveCurrentStage();
    }
}