using System;
using MyTunes.Shared;
using Android.Content;

namespace MyTunes
{
    public class StreamLoader : IStreamLoader
    {
        readonly Context context;

        public StreamLoader (Context context)
        {
            this.context = context;
        }

        #region IStreamLoader implementation

        public System.IO.Stream GetStreamForFilename (string filename)
        {
            return context.Assets.Open (filename);
        }

        #endregion
    }
}

