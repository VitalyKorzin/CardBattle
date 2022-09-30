using UnityEngine;
using UnityEngine.UI;

public class CardsDeckOpening : ButtonPressing
{
    [SerializeField] private Button _openShopButton;
    [SerializeField] private Button _openLeaderboardButton;
    [SerializeField] private RaycastTarget _raycastTargetMenu;

    protected override void OnEnable()
    {
        base.OnEnable();
        _openShopButton.interactable = false;
        _openLeaderboardButton.interactable = false;
        _raycastTargetMenu.raycastTarget = false;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _openShopButton.interactable = true;
        _openLeaderboardButton.interactable = true;
    }
}