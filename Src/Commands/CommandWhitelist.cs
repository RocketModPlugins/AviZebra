using System.Collections.Generic;

using JetBrains.Annotations;

using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Commands;

using UnityEngine;

namespace com.avirockets.unturned.AviZebra {

    [UsedImplicitly]
    public class CommandWhitelist : IRocketCommand {

        public void Execute(IRocketPlayer pCaller, string[] pCommand) {

            ulong? id = pCommand.GetCSteamIDParameter(0);
            if (id == null) {

                UnturnedChat.Say(pCaller, ZebraPlugin.Self.Translate(ZebraPlugin.TK_ERR_STEAMID), Color.red);

                throw new WrongUsageOfCommandException(pCaller, this);
            }

            string stringId = id.ToString();

            IList<string> ids = ZebraPlugin.Self.Persister.PersistedIds;
            if (ids.Contains(stringId)) {

                UnturnedChat.Say(pCaller, ZebraPlugin.Self.Translate(ZebraPlugin.TK_STEAMID_ALREADY_WHITELISTED, stringId), Color.yellow);

                return;
            }

            ids.Add(stringId);
            ZebraPlugin.Self.Persister.Save();

            UnturnedChat.Say(pCaller, ZebraPlugin.Self.Translate(ZebraPlugin.TK_STEAMID_WHITELISTED, stringId), Color.green);

        }

        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "whitelist";

        public string Help => "WhitelistXml SteamID to play on server";

        public string Syntax => "<steam ID>";

        public List<string> Aliases => new List<string> {"wl"};

        public List<string> Permissions => new List<string> {"avi.zebra.wl"};

    }

}