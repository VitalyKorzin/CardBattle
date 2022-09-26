using TMPro;
using UnityEngine;

public class WalletDisplay : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _balanceText;

    private void OnEnable() 
        => _wallet.Changed += OnBalanceChanged;

    private void OnDisable() 
        => _wallet.Changed -= OnBalanceChanged;

    private void Start() 
        => OnBalanceChanged(_wallet.Balance);

    private void OnBalanceChanged(int balance) 
        => _balanceText.text = balance.ToString();
}