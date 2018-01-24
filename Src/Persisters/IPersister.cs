using System.Collections.Generic;

namespace com.avirockets.unturned.AviZebra {

    public interface IPersister {

        IList<string> PersistedIds { get; }

        void Save();

    }

}