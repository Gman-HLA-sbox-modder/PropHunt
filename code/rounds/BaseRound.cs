using Sandbox;

namespace PropHunt
{
    public abstract partial class BaseRound : NetworkComponent
    {
        public virtual int RoundDuration => 0;
        public virtual string RoundName => "";
    }
}
