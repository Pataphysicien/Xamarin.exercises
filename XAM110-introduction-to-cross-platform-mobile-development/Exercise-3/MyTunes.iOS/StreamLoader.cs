using System;
using MyTunes.Shared;
using System.IO;

namespace MyTunes
{
    public class StreamLoader : IStreamLoader
    {
        public StreamLoader ()
        {
        }

        #region IStreamLoader implementation

        public System.IO.Stream GetStreamForFilename (string filename)
        {
            return File.OpenRead (filename);
        }

        #endregion
    }
}

