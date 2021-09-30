
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
    [Library("prophunt", Title = "Prop Hunt")]
    public partial class PropHuntGame : Sandbox.Game
	{
        public MainHud MainHud;

        public static SeekerTeam SeekerTeam { get; private set; }
        public static PropTeam PropTeam { get; private set; }

        [Net]
        public static BaseRound Round { get; private set; }

        private static List<BaseTeam> teams;

        [ServerVar("ph_min_players", Help = "The minimum players required to start.")]
        public static int MinPlayers { get; set; } = 2;

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

        public static void ChangeRound(BaseRound round)
        {
            Round?.Finish();
            Round = round;
            Round.Start();
            UpdateRound(round.RoundName);
        }

        [Event.Hotload]
        public void HotloadUpdate()
        {
            if(!IsClient)
                return;

            MainHud?.Delete();
            MainHud = new MainHud();
        }

        [ClientRpc]
        public static void UpdateRound(string roundName)
        {
            BaseRound round;
            if(roundName == "Hiding")
                round = new HidingRound();
            else if(roundName == "Seeking")
                round = new SeekingRound();
            else if(roundName == "Finished")
                round = new FinishedRound();
            else
                round = new WaitingRound();

            Round = round;
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

        public override void OnKilled(Entity pawn)
        {
            if(pawn is PropHuntPlayer player)
                Round?.OnPlayerKilled(player);

            base.OnKilled(pawn);
        }

        public override void ClientDisconnect(Client client, NetworkDisconnectionReason reason)
        {
            Round?.OnPlayerLeave(client.Pawn as PropHuntPlayer);

            base.ClientDisconnect(client, reason);
        }

        public override void Simulate(Client cl)
        {
            base.Simulate(cl);

            if(IsServer)
            {
                if(Client.All.Count >= MinPlayers)
                {
                    if(Round is WaitingRound || Round == null)
                        ChangeRound(new HidingRound());
                }
                else if(Round is not WaitingRound)
                    ChangeRound(new WaitingRound());
            }
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
                if(player.TeamIndex == SeekerTeam.Index)
                    return;

                player.SetTeam(SeekerTeam.Index);
                Log.Info("Joined team " + SeekerTeam.HudName);
            }
            else if(team == "props")
            {
                if(player.TeamIndex == PropTeam.Index)
                    return;

                player.SetTeam(PropTeam.Index);
                Log.Info("Joined team " + PropTeam.HudName);
            }
        }

        [ServerCmd("ph_round")]
        public static void SetRound(string round)
        {
            int index = int.Parse(round);
            switch(index)
            {
                case 1:
                {
                    ChangeRound(new HidingRound());
                    break;
                }
                case 2:
                {
                    ChangeRound(new SeekingRound());
                    break;
                }
                case 3:
                {
                    ChangeRound(new FinishedRound());
                    break;
                }
                default:
                {
                    ChangeRound(new WaitingRound());
                    break;
                }
            }
        }
    }
}
