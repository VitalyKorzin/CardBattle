using System;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    [SerializeField] private WalletSaver _saver;

    public int Balance { get; private set; }

    public event UnityAction<int> Changed;

    private void Start()
        => Balance = _saver.LoadBalance();

    public void Withdraw(int price)
    {
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price));

        if (CheckSolvency(price) == false)
            throw new InvalidOperationException();

        Balance -= price;
        Changed?.Invoke(Balance);
        _saver.SaveBalance(Balance);
    }

    public bool CheckSolvency(int price)
    {
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price));

        return Balance >= price;
    }

    public void Replenish(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        Balance += value;
        Changed?.Invoke(Balance);
        _saver.SaveBalance(Balance);
    }
}