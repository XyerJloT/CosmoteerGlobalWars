using System;

public class Star
{
    public readonly int Incom;
    public readonly string Name;
    public readonly Hangar Hangar = new Hangar();

    public event Action<Team> OnCaptured;

    public Star(string name, int incom)
    {
        Name = name;
        Incom = incom;
    }

    public void Capture(Team invader)
    {
        OnCaptured?.Invoke(invader);
    }
}
