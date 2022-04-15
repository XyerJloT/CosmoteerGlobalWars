using System;

public class Balance
{
    public event Action<int> OnChange;

    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            OnChange?.Invoke(value);
        }
    }

    private int _money;

    public Balance(int money = 0)
    {
        Money = money;
    }
}
