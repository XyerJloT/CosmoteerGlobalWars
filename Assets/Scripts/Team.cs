using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public enum TeamId { Neutral, Red, Blue }

    public readonly static Team Neutral = new Team(TeamId.Neutral, Color.grey);

    public readonly TeamId Id;
    public readonly Color Color;
    public readonly Balance Balance = new Balance();

    public IEnumerable<Blueprint> Blueprints => _blueprints;

    public delegate void ChangeIncomHandler(int total, int delta);
    public event ChangeIncomHandler OnChangedIncom;

    public int Incom
    {
        get => _incom;
        private set
        {
            _incom = value;

            OnChangedIncom?.Invoke(_incom, value);
        }
    }

    private int _incom = 0;
    private ISet<Star> _captured = new HashSet<Star>();
    private List<Blueprint> _blueprints = new List<Blueprint>();

    Team(TeamId id, Color color)
    {
        Id = id;
        Color = color;
    }

    public bool IsCaptered(Star star)
    {
        return _captured.Contains(star);
    }

    public void CaptureStar(Star star)
    {
        if (IsCaptered(star)) return;
        _captured.Add(star);
        star.Capture(this);

        Incom += star.Incom;

        // Подписываемся на событие когда кто-то захватит нашу звезду
        star.OnCaptured += StarHandler;

        void StarHandler(Team invader)
        {
            star.OnCaptured -= StarHandler;
            _captured.Remove(star);
            Incom -= star.Incom;
        }
    }

    public Blueprint AddBlueprint(string name, int cost, Ship.RankType type)
    {
        var blueprint = new Blueprint(name, type, cost);
        _blueprints.Add(blueprint);
        return blueprint;
    }

    public bool RemoveBlueprint(Blueprint blueprint)
    {
        return _blueprints.Remove(blueprint);
    }
}
