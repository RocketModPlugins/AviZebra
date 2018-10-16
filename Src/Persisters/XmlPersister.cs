using System.Collections.Generic;
using System.IO;

using Rocket.Core;
using Rocket.Core.Assets;

using UnityEngine;

namespace com.avirockets.unturned.AviZebra {

    internal class XmlPersister : MonoBehaviour, IPersister {

        private XMLFileAsset<WhitelistXml> _whitelistAsset;

        private void Awake() {
            _whitelistAsset = new XMLFileAsset<WhitelistXml>(
                Path.Combine(Path.Combine(Environment.PluginsDirectory, ZebraPlugin.Self.Name), "AviZebra.whitelist.xml"));
            _whitelistAsset.Load();
        }

        private void OnDestroy() => _whitelistAsset.Unload();

        IList<string> IPersister.PersistedIds => _whitelistAsset.Instance.PersistedIds;

        void IPersister.Save() => _whitelistAsset.Save();

    }

}