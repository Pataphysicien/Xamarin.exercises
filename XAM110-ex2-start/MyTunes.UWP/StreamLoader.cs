using MyTunes.Shared;
using System;
using System.IO;

namespace MyTunes.UWP
{
    class StreamLoader : IStreamLoader
    {
        public Stream GetStreamForFilename(string pFileName)
        {
            return Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(pFileName)
                                                                             .AsTask().Result
                                                                             .OpenStreamForReadAsync().Result;
        }
    }
}
