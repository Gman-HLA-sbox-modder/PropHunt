
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
        private static List<BaseTeam> teams;
        private static List<BaseRound> rounds;

        public MainHud MainHud;

        public static List<MapProp> MapProps = new List<MapProp>();

        public static SeekerTeam SeekerTeam { get; private set; }
        public static PropTeam PropTeam { get; private set; }

        public static WaitingRound WaitingRound { get; private set; }
        public static HidingRound HidingRound { get; private set; }
        public static SeekingRound SeekingRound { get; private set; }
        public static FinishedRound FinishedRound { get; private set; }

        [Net]
        public static BaseRound Round { get; private set; }

        [Net]
        public static float TimerEnd { get; private set; }

        [Net]
        public static int Winner { get; private set; }

        [ServerVar("ph_min_players", Help = "The minimum players required to start.")]
        public static int MinPlayers { get; set; } = 2;

        [ServerVar("ph_outofbounds_height", Help = "The height at which players are killed when falling outside the map.")]
        public static float KillHeight { get; set; } = 0f;

        public PropHuntGame()
        {
            teams = new List<BaseTeam>();
            rounds = new List<BaseRound>();

            SeekerTeam = new SeekerTeam();
            PropTeam = new PropTeam();

            AddTeam(SeekerTeam);
            AddTeam(PropTeam);

            WaitingRound = new WaitingRound();
            HidingRound = new HidingRound();
            SeekingRound = new SeekingRound();
            FinishedRound = new FinishedRound();

            AddRound(WaitingRound);
            AddRound(HidingRound);
            AddRound(SeekingRound);
            AddRound(FinishedRound);

            if(IsClient)
                MainHud = new MainHud();
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

        public static List<PropHuntPlayer> GetPlayersByTeam(int team, bool alive = false)
        {
            List<PropHuntPlayer> list = new List<PropHuntPlayer>();

            foreach(Client client in Client.All)
            {
                if(client.Pawn is PropHuntPlayer player && player.TeamIndex == team)
                {
                    if(!alive || player.LifeState == LifeState.Alive)
                    {
                        list.Add(player);
                    }
                }
            }

            return list;
        }

        public void AddRound(BaseRound round)
        {
            rounds.Add(round);
            round.Index = rounds.Count;
        }

        public static BaseRound GetRound(int index)
        {
            if(index <= 0 || index > rounds.Count)
                return null;

            return rounds[index - 1];
        }

        public static void ChangeRound(BaseRound round)
        {
            Round?.Finish();
            Round = round;
            Round.Start();
            UpdateRound(round.Index);
        }

        public static void SetTimerEnd(float time)
        {
            TimerEnd = time;
            UpdateTimerEnd(time);
        }

        public static void SetWinner(BaseTeam team)
        {
            Winner = team.Index;
            UpdateWinner(team.Index);
        }

        [Event.Hotload]
        public void HotloadUpdate()
        {
            if(!IsClient)
                return;

            MainHud?.Delete();
            MainHud = new MainHud();
        }

        [Event("PropHunt.JoinTeam")]
        public void OnJoinTeam(PropHuntPlayer player, BaseTeam team)
        {
            if(IsServer)
                OnClientJoinTeam(player, team.Index);
            else
                MainHud.OnJoinTeam(player, team);
        }

        [Event("PropHunt.LeaveTeam")]
        public void OnLeaveTeam(PropHuntPlayer player, BaseTeam team)
        {
            if(IsServer)
                OnClientLeaveTeam(player, team.Index);
            else
                MainHud.OnLeaveTeam(player, team);
        }

        /// <summary>
        /// A client has joined the server. Make them a pawn to play with
        /// </summary>
        public override void ClientJoined(Client client)
        {
            base.ClientJoined(client);

            var player = new PropHuntPlayer(client);
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

        public override void PostLevelLoaded()
        {
            base.PostLevelLoaded();

            if(IsClient)
                return;

            foreach(Entity entity in All)
            {
                if(entity is Prop prop && entity.ClassInfo?.Name == "prop_physics")
                {
                    MapProp mapProp = new MapProp();
                    mapProp.ClassName = prop.ClassInfo.Name;
                    mapProp.Model = prop.GetModel();
                    mapProp.Position = prop.Position;
                    mapProp.Rotation = prop.Rotation;
                    mapProp.Scale = prop.Scale;
                    mapProp.Color = prop.RenderColor;
                    MapProps.Add(mapProp);
                }
            }
        }

        public override void Simulate(Client cl)
        {
            base.Simulate(cl);

            if(IsServer)
            {
                int seekerCount = GetPlayersByTeam(SeekerTeam.Index).Count;
                int propCount = GetPlayersByTeam(PropTeam.Index).Count;
                if(seekerCount + propCount >= MinPlayers)
                {
                    if(Round is WaitingRound || Round == null)
                        ChangeRound(HidingRound);
                }
                else if(!(Round is WaitingRound))
                    ChangeRound(WaitingRound);

                if(TimerEnd > 0 && Time.Now >= TimerEnd)
                {
                    TimerEnd = 0f;
                    Round?.OnTimerEnd();
                }
            }
        }

        [ServerCmd("ph_jointeam")]
        public static void JoinTeam(string team)
        {
            if(ConsoleSystem.Caller.Pawn is not PropHuntPlayer player)
                return;

            ToggleTeamSelection(player, false);

            team = team.ToLower();
            team = team.Trim();

            if(Round != WaitingRound && player.TeamIndex > 0)
                return;

            if(team == "seeker" || team == "seekers")
            {
                if(player.TeamIndex == SeekerTeam.Index)
                    return;

                player.SetTeam(SeekerTeam.Index);
                Log.Info("Joined team " + SeekerTeam.HudName);
            }
            else if(team == "prop" || team == "props")
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
            if(index > 0)
                ChangeRound(GetRound(index));
            else
                ChangeRound(WaitingRound);
        }

        [ClientRpc]
        public static void UpdateRound(int index)
        {
            Round = GetRound(index);
        }

        [ClientRpc]
        public static void UpdateTimerEnd(float time)
        {
            TimerEnd = time;
        }

        [ClientRpc]
        public static void ToggleTeamSelection(PropHuntPlayer player, bool b)
        {
            player.ToggleTeamSelection(b);
        }

        [ClientRpc]
        public static void UpdateWinner(int teamIndex)
        {
            Winner = teamIndex;
        }

        [ClientRpc]
        public static void OnClientJoinTeam(PropHuntPlayer player, int team)
        {
            Event.Run("PropHunt.JoinTeam", player, GetTeam(team));
        }

        [ClientRpc]
        public static void OnClientLeaveTeam(PropHuntPlayer player, int team)
        {
            Event.Run("PropHunt.LeaveTeam", player, GetTeam(team));
        }
    }
}
