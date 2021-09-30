using Sandbox;

namespace PropHunt
{
    public abstract partial class BaseRound : BaseNetworkable
    {
        public virtual int RoundDuration => 0;
        public virtual string RoundName => "";

        public void Start()
        {
            OnStart();
        }

        public void Finish()
        {
            OnFinish();
        }

        public virtual void OnStart()
        {
        }

        public virtual void OnFinish()
        {
        }

        public virtual void OnPlayerKilled(PropHuntPlayer player)
        {
        }

        public virtual void OnPlayerLeave(PropHuntPlayer player)
        {
        }
    }
}
