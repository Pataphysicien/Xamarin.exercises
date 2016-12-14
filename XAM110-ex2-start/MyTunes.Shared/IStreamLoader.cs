using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTunes.Shared
{
    public interface IStreamLoader
    {
        Stream GetStreamForFilename(string pFileName);
    }
}
