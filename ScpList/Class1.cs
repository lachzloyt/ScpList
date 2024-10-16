using Exiled.API.Features;
using Exiled.Loader;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;
using player = Exiled.Events.Handlers.Player;
using server = Exiled.Events.Handlers.Server;

namespace ScpList
{
    public class Class1 : Plugin<Config>
    {

        public override string Name => "Scplist";
        public override string Prefix => Name;
        public override string Author => "Lachzloyt";


        public static Class1 Instance;
        public CoroutineHandle coroutineHandle;
        public StringBuilder sb;
        private IEnumerator<float> coroutine;

        public override void OnEnabled()
        {
            Instance = this;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnded;
            base.OnEnabled();
           
        }
        public override void OnDisabled()
        {
            Instance = null;
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= OnRoundEnded;
            Timing.KillCoroutines(coroutineHandle);
            base.OnDisabled();
        }
        public void OnRoundStarted()
        {

            coroutineHandle = Timing.RunCoroutine(UpdateInfo());


        }
        
        private void OnRoundEnded(Exiled.Events.EventArgs.Server.RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines(coroutineHandle);
        }

        public IEnumerator<float> UpdateInfo()
        {
            while (Round.IsStarted)
            {

                string FirstColorHex = Instance.Config.FirstColorHex; 
                string SecondColorHex = Instance.Config.SecondColorHex; 
                string MainColorHex = Instance.Config.MainColorHex; 
                string TextVar = Instance.Config.TextShow;

                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"<color={MainColorHex}>{TextVar}</color>");  
               
               foreach (Player player in Player.List.Where(p => p.Role.Team == Team.SCPs && p.IsAlive))
                {
                    sb.Append($"|<size=32><color={FirstColorHex}>{player.Role.Name} </color> :  <color={SecondColorHex}>{Convert.ToInt32(player.Health)}HP</color></size>| " );
                    
                }

                string scpInfo = sb.ToString();

                float Showtime = Instance.Config.Showtime;
                int Showduration = Instance.Config.Showduration;
                // Display the information to all players
                foreach (Player player in Player.List)
                {
                    Map.Broadcast((ushort)Showduration, scpInfo); // Display for 5 seconds
                }

                // Wait for 5 seconds before updating the SCP info again
                yield return Timing.WaitForSeconds(Showtime);


            }

        }
    }
}