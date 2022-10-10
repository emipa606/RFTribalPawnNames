using System.Collections.Generic;
using Verse;

namespace RTPN_Code;

public static class RTPN_FileRead
{
    public static IEnumerable<string> LinesFromFile(string filePath)
    {
        var rawText = GenFile.TextFromRawFile(filePath);
        foreach (var line in GenText.LinesFromString(rawText))
        {
            yield return line;
        }
    }
}