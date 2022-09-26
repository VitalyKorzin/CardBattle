using System;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _startBalance;

    public int Balance { get; private set; }

    public event UnityAction<int> Changed;

    private void Awake() => Balance = _startBalance;

    public void Withdraw(int price)
    {
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price));

        if (CheckSolvency(price) == false)
            throw new InvalidOperationException();

        Balance -= price;
        Changed?.Invoke(Balance);
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
    }
}