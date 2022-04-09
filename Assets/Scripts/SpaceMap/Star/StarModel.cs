public class StarModel
{
    public readonly int Incom;
    public readonly string Name;
    public readonly Hangar Hangar = new Hangar();

    public delegate void CaptureHandler(Team invader);
    public event CaptureHandler OnCaptured;

    public StarModel(string name, int incom)
    {
        Name = name;
        Incom = incom;
    }

    public void Capture(Team invader)
    {
        OnCaptured?.Invoke(invader);
    }
}
