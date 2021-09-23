namespace PropHunt
{
    public abstract class BaseTeam
    {
        public int Index { get; internal set; }

        public virtual string Name => "";
        public virtual string HudName => "";

        public void Join(PropHuntPlayer player)
        {
            OnJoin(player);
        }

        public void Leave(PropHuntPlayer player)
        {
            OnLeave(player);
        }

        public virtual void OnJoin(PropHuntPlayer player) { }

        public virtual void OnLeave(PropHuntPlayer player) { }
    }
}