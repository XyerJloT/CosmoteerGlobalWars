using Assets.Scripts.Map;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public enum TeamId { Neutral, Red, Blue }

    public readonly static Team Neutral = new Team(TeamId.Neutral, Color.grey);

    public readonly TeamId Id;
    public readonly Color Color;
    public readonly Balance Balance = new Balance();

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

    public Team(TeamId id, Color color)
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
        star.TakeOver(this);

        Incom += star.Incom;

        // Подписываемся на событие когда кто-то захватит нашу звезду
        star.OnOwnerChanged += StarHandler;

        void StarHandler(Team olwOwner, Team invader)
        {
            star.OnOwnerChanged -= StarHandler;
            _captured.Remove(star);
            Incom -= star.Incom;
        }
    }
}
