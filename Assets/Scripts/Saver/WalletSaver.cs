using System;

public class WalletSaver : Saver
{
    private const string Balance = nameof(Balance);

    public void Save(int balance)
        => SaveIntegerValue(Balance, balance);

    public int LoadBalance()
    {
        if (TryLoadIntegerValue(Balance, out int result))
            return result;
        else
            throw new InvalidOperationException();
    }
}