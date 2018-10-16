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

        public void Execute(IRocketPlayer caller, string[] command) {

            ulong? id = command.GetCSteamIDParameter(0);
            if (id == null) {

                UnturnedChat.Say(caller, ZebraPlugin.Self.Translate(ZebraPlugin.TK_ERR_STEAMID), Color.red);

                throw new WrongUsageOfCommandException(caller, this);
            }

            string stringId = id.ToString();

            IList<string> ids = ZebraPlugin.Self.Persister.PersistedIds;
            if (!ids.Remove(stringId)) {

                UnturnedChat.Say(caller, ZebraPlugin.Self.Translate(ZebraPlugin.TK_STEAMID_NOT_WHITELISTED, stringId), Color.yellow);

                return;
            }

            Provider.kick((CSteamID) id, ZebraPlugin.Self.Translate(ZebraPlugin.TK_KICK_REASON_UNWHITELISTED));

            ZebraPlugin.Self.Persister.Save();

            UnturnedChat.Say(caller, ZebraPlugin.Self.Translate(ZebraPlugin.TK_STEAMID_UNWHITELISTED, stringId), Color.green);

        }

        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "unwhitelist";

        public string Help => "Unwhitelist SteamID from playing on server";

        public string Syntax => "<steam ID>";

        public List<string> Aliases => new List<string> {"uwl"};

        public List<string> Permissions => new List<string> {"avi.zebra.uwl"};

    }

}