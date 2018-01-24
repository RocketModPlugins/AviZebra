using System.Collections.Generic;

using JetBrains.Annotations;

using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Commands;

using SDG.Unturned;

using Steamworks;

using UnityEngine;

namespace com.avirockets.unturned.AviZebra {

    [UsedImplicitly]
    public class CommandUnwhitelist : IRocketCommand {

        public void Execute(IRocketPlayer pCaller, string[] pCommand) {

            ulong? id = pCommand.GetCSteamIDParameter(0);
            if (id == null) {

                UnturnedChat.Say(pCaller, ZebraPlugin.Self.Translate(ZebraPlugin.TK_ERR_STEAMID), Color.red);

                throw new WrongUsageOfCommandException(pCaller, this);
            }

            string stringId = id.ToString();

            IList<string> ids = ZebraPlugin.Self.Persister.PersistedIds;
            if (!ids.Remove(stringId)) {

                UnturnedChat.Say(pCaller, ZebraPlugin.Self.Translate(ZebraPlugin.TK_STEAMID_NOT_WHITELISTED, stringId), Color.yellow);

                return;
            }
            
            Provider.kick((CSteamID) id, ZebraPlugin.Self.Translate(ZebraPlugin.TK_KICK_REASON_UNWHITELISTED));

            ZebraPlugin.Self.Persister.Save();

            UnturnedChat.Say(pCaller, ZebraPlugin.Self.Translate(ZebraPlugin.TK_STEAMID_UNWHITELISTED, stringId), Color.green);

        }

        public AllowedCaller AllowedCaller { get; } = AllowedCaller.Both;

        public string Name { get; } = "unwhitelist";

        public string Help { get; } = "Unwhitelist SteamID from playing on server";

        public string Syntax { get; } = "<steam ID>";

        public List<string> Aliases { get; } = new List<string> {"uwl"};

        public List<string> Permissions { get; } = new List<string> {"avi.zebra.uwl"};

    }

}