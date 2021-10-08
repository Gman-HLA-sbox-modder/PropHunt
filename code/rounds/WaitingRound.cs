using Sandbox;
using System.Collections.Generic;

namespace PropHunt
{
    public class WaitingRound : BaseRound
    {
        public override string RoundName => "Waiting";

        public override void OnStart()
        {
            Log.Info(RoundName + " Round has started.");
        }

        public override void OnFinish()
        {
            foreach(Entity entity in Entity.All)
            {
                if(entity is Prop && entity.ClassInfo?.Name == "prop_physics")
                {
                    entity.Delete();
                }
            }

            List<Prop> props = new List<Prop>();
            foreach(MapProp mapProp in PropHuntGame.MapProps)
            {
                Prop prop = Library.Create<Entity>(mapProp.ClassName) as Prop;
                prop.SetModel(mapProp.Model);
                prop.Position = mapProp.Position;
                prop.Rotation = mapProp.Rotation;
                prop.Scale = mapProp.Scale;
                prop.RenderColor = mapProp.Color;

                prop.MoveType = MoveType.None;
                prop.PhysicsEnabled = false;

                props.Add(prop);
            }

            foreach(Prop prop in props)
            {
                prop.Spawn();
            }
        }
    }
}