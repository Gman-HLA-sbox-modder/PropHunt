
using Sandbox;
using Sandbox.UI.Construct;
using System;
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

		public PropHuntGame()
		{
			if(IsServer)
			{
				Log.Info("My Gamemode Has Created Serverside!");

				// Create a HUD entity. This entity is globally networked
				// and when it is created clientside it creates the actual
				// UI panels. You don't have to create your HUD via an entity,
				// this just feels like a nice neat way to do it.
				new MinimalHudEntity();
			}

			if(IsClient)
			{
				Log.Info("My Gamemode Has Created Clientside!");
                MainHud = new MainHud();
			}
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
	}

}
