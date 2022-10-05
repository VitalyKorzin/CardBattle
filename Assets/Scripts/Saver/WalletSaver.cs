using System;

public class WalletSaver : Saver
{
    private const string Balance = nameof(Balance);

    public void SaveBalance(int value)
        => SaveIntegerValue(Balance, value);

    public int LoadBalance()
    {
        if (TryLoadIntegerValue(Balance, out int result))
            return result;
        else
            throw new InvalidOperationException();
    }
}