using System;
using System.Collections.ObjectModel;

public class Star
{
    public readonly int Incom;
    public readonly string Name;
    public readonly ObservableCollection<Ship> Fleet = new ObservableCollection<Ship>();
    //public readonly ObservableCollection<Blueprint> Blueprints = new ObservableCollection<Blueprint>();

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
