using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using MyTunes.Shared;

namespace MyTunes
{
	public static class SongLoader
	{
		const string Filename = "songs.json";

        public static IStreamLoader StreamLoader { get; set; }

        public static async Task<IEnumerable<Song>> Load()
		{
            using (var stream = OpenData())
			using (var reader = new StreamReader(stream))
				return JsonConvert.DeserializeObject<List<Song>>(await reader.ReadToEndAsync());
		}

		private static Stream OpenData()
        {
            if (StreamLoader == null)
                throw new Exception("Must set platform Loader before calling Load.");
            return StreamLoader.GetStreamForFilename(Filename);
		}
	}
}

