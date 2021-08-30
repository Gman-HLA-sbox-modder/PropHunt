namespace PropHunt
{
    public abstract class BaseTeam
    {
        public int Index { get; internal set; }

        public virtual string Name => "";
        public virtual string HudName => "";
    }
}