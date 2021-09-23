
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace PropHunt
{

	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// </summary>
	public partial class PropHuntGame : Sandbox.Game
	{
        public MainHud MainHud;

        public static SeekerTeam SeekerTeam { get; private set; }
        public static PropTeam PropTeam { get; private set; }

        private static List<BaseTeam> teams;

		public PropHuntGame()
		{
			if(IsServer)
			{
				Log.Info("My Gamemode Has Created Serverside!");
			}

			if(IsClient)
			{
				Log.Info("My Gamemode Has Created Clientside!");
                MainHud = new MainHud();
			}

            teams = new List<BaseTeam>();

            SeekerTeam = new SeekerTeam();
            PropTeam = new PropTeam();

            AddTeam(SeekerTeam);
            AddTeam(PropTeam);
        }

        public void AddTeam(BaseTeam team)
        {
            teams.Add(team);
            team.Index = teams.Count;
        }

        public static BaseTeam GetTeam(int index)
        {
            if(index <= 0 || index > teams.Count)
                return null;

            return teams[index - 1];
        }

        [Event.Hotload]
        public void HotloadUpdate()
        {
            if(!IsClient)
                return;

            MainHud?.Delete();
            MainHud = new MainHud();
        }

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined(Client client)
		{
			base.ClientJoined(client);

			var player = new PropHuntPlayer();
			client.Pawn = player;

			player.Respawn();
		}

        [ServerCmd("ph_jointeam")]
        public static void JoinTeam(string team)
        {
            if(ConsoleSystem.Caller.Pawn is not PropHuntPlayer player)
                return;

            team = team.ToLower();
            team = team.Trim();

            if(team == "seeker")
            {
                player.SetTeam(SeekerTeam.Index);
                Log.Info("Joined team " + SeekerTeam.HudName);
            }
            else if(team == "props")
            {
                player.SetTeam(PropTeam.Index);
                Log.Info("Joined team " + PropTeam.HudName);
            }
        }
	}
}
