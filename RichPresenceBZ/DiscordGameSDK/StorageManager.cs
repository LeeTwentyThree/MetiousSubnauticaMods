using System.Collections.Generic;

namespace RichPresenceBZ.DiscordGameSDK;

public partial class StorageManager
{
    public IEnumerable<FileStat> Files()
    {
        var fileCount = Count();
        var files = new List<FileStat>();
        for (var i = 0; i < fileCount; i++)
        {
            files.Add(StatAt(i));
        }
        return files;
    }
}