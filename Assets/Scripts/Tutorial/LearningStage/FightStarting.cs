using UnityEngine;
using UnityEngine.UI;

public class FightStarting : LearningStage
{
    [SerializeField] private Menu _menu;
    [SerializeField] private Button _openCardsDeckButton;
    [SerializeField] private Button _openLeaderboardButton;
    [SerializeField] private Button _openShopButton;

    private void OnEnable()
    {
        _menu.GameBegun += OnGameBegun;
        _openCardsDeckButton.interactable = false;
        _openLeaderboardButton.interactable = false;
        _openShopButton.interactable = false;
    }

    private void OnDisable()
    {
        _menu.GameBegun -= OnGameBegun;
        _openCardsDeckButton.interactable = true;
        _openLeaderboardButton.interactable = true;
        _openShopButton.interactable = true;
    }

    private void OnGameBegun() => Finish();
}