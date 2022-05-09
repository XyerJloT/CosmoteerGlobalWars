using System;

class MetricPrefix
{
    public readonly int Number;
    public readonly int Exponent;
    public readonly string Prefix;

    public MetricPrefix(int number)
    {
        Number = number;
        Exponent = RoundExponent(CalculateExponent(number));
        Prefix = GetPrefix(Exponent);
    }

    public override string ToString()
    {
        if (Number == 0) return "0";

        var value = Number / Math.Pow(10, Exponent);

        if (Prefix.Length == 0) return $"{value:###.##}";

        return $"{value:###.##} {Prefix}";
    }

    private int CalculateExponent(int number)
    {
        number = Math.Abs(number);
     
        int power = 0;
        while (number >= 10)
        {
            number /= 10;
            power++;
        }

        return power;
    }

    private int RoundExponent(int exponent)
    {
        return exponent - (exponent % 3);
    }

    private string GetPrefix(int roundedExponent)
    {
        switch (roundedExponent)
        {
            case 0: return "";
            case 3: return "K";
            case 6: return "M";
            case 9: return "G";
            default: throw new ArgumentException($"Недопустимое значение экспоненты: {roundedExponent}");
        }
    }
}
