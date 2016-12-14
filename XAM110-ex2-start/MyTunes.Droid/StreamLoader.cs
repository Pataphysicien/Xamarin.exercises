using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyTunes.Shared;

namespace MyTunes
{
    class StreamLoader : IStreamLoader
    {
        private readonly Context _droidContext;
        public StreamLoader(Context droidContext)
        {
            if (droidContext == null)
                throw new ArgumentNullException(nameof(droidContext));
            _droidContext = droidContext;
        }
        public Stream GetStreamForFilename(string pFileName)
        {
            return _droidContext.Assets.Open(pFileName);
        }
    }
}