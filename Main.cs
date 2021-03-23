using Rocket.API;
using Rocket.Core.Commands;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Linq;

namespace OfflineAdmins
{
    public class Main : RocketPlugin<Config>
    {
        protected override void Load()
        {
            U.Events.OnPlayerConnected += Connect;
        }
        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= Connect;
        }
        private void Connect(UnturnedPlayer player)
        {
            var Admins = Configuration.Instance.Admins.FirstOrDefault((Admin A) => A.SteamID == player.CSteamID.ToString());
            if (Admins != null)
            {
                if (Admins.Value == "1")
                {
                    if (!player.IsAdmin)
                    {
                        Configuration.Instance.Admins.Remove(Admins);
                        player.Admin(true);
                    }
                    else return;
                }
                else if (Admins.Value == "0")
                {
                    if (player.IsAdmin) 
                    {
                        Configuration.Instance.Admins.Remove(Admins);
                        player.Admin(false);
                    } 
                    else return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        [RocketCommand("adminadd", "", "", AllowedCaller.Both)]
        [RocketCommandPermission("mixy.offlineadmin")]
        public void Adminadd(IRocketPlayer caller, string[] command)
        {
            string playerid = command[0];
            var Admins = Configuration.Instance.Admins.FirstOrDefault((Admin A) => A.SteamID == playerid);
            if (playerid.Length < 5 || playerid == null)
            {
                if (Admins == null)
                {
                    Configuration.Instance.Admins.Add(new Admin { SteamID = playerid, Value = "1" });
                    Configuration.Save();
                }
                else
                {
                    Admins.Value = "1";
                    Configuration.Save();
                }
            }
            else
            {
                UnturnedChat.Say(caller, "Wrong usage. True: /adminadd <id>");
            }           
        }
        [RocketCommand("adminremove", "", "", AllowedCaller.Both)]
        [RocketCommandPermission("mixy.offlineadmin")]
        public void Adminremove(IRocketPlayer caller, string[] command)
        {
            string playerid = command[0];
            var Admins = Configuration.Instance.Admins.FirstOrDefault((Admin A) => A.SteamID == playerid);
            if(playerid.Length < 5 || playerid == null)
            {
                if (Admins == null)
                {
                    Configuration.Instance.Admins.Add(new Admin { SteamID = playerid, Value = "0" });
                    Configuration.Save();
                }
                else
                {
                    Admins.Value = "0";
                    Configuration.Save();
                }
            }
            else
            {
                UnturnedChat.Say(caller, "Wrong usage. True: /adminadd <id>");
            }
        }
    }
}
