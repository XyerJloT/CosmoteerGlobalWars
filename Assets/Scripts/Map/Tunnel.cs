namespace Assets.Scripts.Map
{
    public class Tunnel
    {
        public readonly Star From;
        public readonly Star To;

        public Tunnel(Star from, Star to)
        {
            From = from;
            To = to;
        }
    }
}
