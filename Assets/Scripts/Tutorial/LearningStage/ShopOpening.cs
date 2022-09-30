using UnityEngine;
using UnityEngine.UI;

public class ShopOpening : ButtonPressing
{
    [SerializeField] private Button _openCardsDeckButton;
    [SerializeField] private Button _openLeaderboardButton;
    [SerializeField] private RaycastTarget _raycastTargetMenu;

    protected override void OnEnable()
    {
        base.OnEnable();
        _openCardsDeckButton.interactable = false;
        _openLeaderboardButton.interactable = false;
        _raycastTargetMenu.raycastTarget = false;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _openCardsDeckButton.interactable = true;
        _openLeaderboardButton.interactable = true;
    }
}