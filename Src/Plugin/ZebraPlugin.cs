using System;

using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Permissions;

using SDG.Unturned;

using Steamworks;

namespace com.avirockets.unturned.AviZebra {

    public class ZebraPlugin : RocketPlugin /*<ZebraConfig>*/ {

        internal const string TK_ERR_STEAMID = "err_steamid";
        internal const string TK_STEAMID_WHITELISTED = "steamid_whitelisted";
        internal const string TK_STEAMID_ALREADY_WHITELISTED = "steamid_already_whitelisted";
        internal const string TK_STEAMID_UNWHITELISTED = "steamid_unwhitelisted";
        internal const string TK_STEAMID_NOT_WHITELISTED = "steamid_not_whitelisted";
        internal const string TK_KICK_REASON_UNWHITELISTED = "kick_reason_unwhitelisted";

        public static ZebraPlugin Self;

        internal IPersister Persister { get; private set; }

        protected override void Load() {

            Self = this;

            Logger.Log("AviZebra Copyright (C) 2018 AviRockets", ConsoleColor.DarkMagenta);
            Logger.Log("Zebra came to the field", ConsoleColor.Green);
            Logger.Log("Join AviRockets Discord channel http://go.avirockets.com/discord - lots of awesome plugins await ;)", ConsoleColor.Cyan);

            Persister = TryAddComponent<XmlPersister>();

            UnturnedPermissions.OnJoinRequested += OnJoinRequested;

        }

        protected override void Unload() {

            UnturnedPermissions.OnJoinRequested -= OnJoinRequested;

            TryRemoveComponent<XmlPersister>();

            Logger.Log("Zebra left the field", ConsoleColor.Green);

        }

        public override TranslationList DefaultTranslations { get; } = new TranslationList {
            {TK_ERR_STEAMID, "Please input valid Steam ID"},
            {TK_STEAMID_WHITELISTED, "Steam ID {0} whitelisted!"},
            {TK_STEAMID_ALREADY_WHITELISTED, "Steam ID {0} is already whitelisted!"},
            {TK_STEAMID_UNWHITELISTED, "Steam ID {0} unwhitelisted!"},
            {TK_STEAMID_NOT_WHITELISTED, "Steam ID {0} is not whitelisted!" },
            {TK_KICK_REASON_UNWHITELISTED, "no longer whitelisted. Bye!"}
        };

        private void OnJoinRequested(CSteamID pSteamId, ref ESteamRejection? pRejectionReason) {

            string playerId = pSteamId.ToString();

            bool sayGoodbye = !Persister.PersistedIds.Contains(playerId);
#if DEBUG
            Logger.Log($"Join requested from: {playerId}, saying goodbye: {sayGoodbye}");
#endif
            if (sayGoodbye) {
                pRejectionReason = ESteamRejection.WHITELISTED;
            }

        }

    }

}