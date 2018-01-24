using System.Collections.Generic;
using System.Xml.Serialization;

using JetBrains.Annotations;

using Rocket.API;

namespace com.avirockets.unturned.AviZebra {

    [UsedImplicitly, XmlRoot("Whitelist")]
    public class WhitelistXml : IDefaultable {

        [XmlArray("PersistedIds"), XmlArrayItem("Id")]
        public List<string> PersistedIds { get; set; }

        public void LoadDefaults() => PersistedIds = new List<string> {"76561198113967823"};

    }

}