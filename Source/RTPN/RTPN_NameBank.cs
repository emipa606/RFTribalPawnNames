using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RimWorld;
using Verse;

namespace RTPN_Code;

public class RTPN_NameBank
{
    private static readonly int numGenders = Enum.GetValues(typeof(Gender)).Length;
    private static readonly int numSlots = Enum.GetValues(typeof(RTPN_NameSlot)).Length;

    private readonly string modBasePath = LoadedModManager.RunningMods
        .First(mcp => mcp.assemblies.loadedAssemblies.Contains(typeof(RTPN_Initializer).Assembly)).RootDir;

    private readonly List<string>[,] names;
    public PawnNameCategory nameType;

    public RTPN_NameBank(PawnNameCategory id)
    {
        nameType = id;
        names = new List<string>[numGenders, numSlots];
        for (var i = 0; i < numGenders; i++)
        {
            for (var j = 0; j < numSlots; j++)
            {
                names[i, j] = [];
            }
        }
    }

    private IEnumerable<List<string>> AllNameLists
    {
        get
        {
            for (var i = 0; i < numGenders; i++)
            {
                for (var j = 0; j < numSlots; j++)
                {
                    yield return names[i, j];
                }
            }
        }
    }

    private void addNames(RTPN_NameSlot slot, Gender gender, IEnumerable<string> namesToAdd)
    {
        var enumerator = namesToAdd.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                namesFor(slot, gender).Add(current);
            }
        }
        finally
        {
            enumerator.Dispose();
        }
    }

    public void AddNamesFromFile(RTPN_NameSlot slot, Gender gender, string fileName)
    {
        var namesPath = Path.GetFullPath(Path.Combine(modBasePath, "Name Lists/"))
            .Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
        addNames(slot, gender, RTPN_FileRead.LinesFromFile(string.Concat(namesPath, fileName)));
    }

    public void ErrorCheck()
    {
        var enumerator = AllNameLists.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                var list = (
                    from x in current
                    group x by x
                    into g
                    where g.Count() > 1
                    select g.Key).ToList();
                var enumerator1 = list.GetEnumerator();
                try
                {
                    while (enumerator1.MoveNext())
                    {
                        Log.Error($"Duplicated name: {enumerator1.Current}");
                    }
                }
                finally
                {
                    enumerator1.Dispose();
                }

                if (current == null)
                {
                    continue;
                }

                var enumerator2 = current.GetEnumerator();
                try
                {
                    while (enumerator2.MoveNext())
                    {
                        var str = enumerator2.Current;
                        if (str?.Trim() == str)
                        {
                            continue;
                        }

                        Log.Error($"Trimmable whitespace on name: [{str}]");
                    }
                }
                finally
                {
                    enumerator2.Dispose();
                }
            }
        }
        finally
        {
            enumerator.Dispose();
        }
    }

    public string GetName(RTPN_NameSlot slot, Gender gender = 0)
    {
        string str;
        var strings = namesFor(slot, gender);
        var num = 0;
        if (strings.Count == 0)
        {
            Log.Error($"Name list for gender={gender} slot={slot} is empty.");
            return "Errorname";
        }

        while (true)
        {
            str = strings.RandomElement();
            if (!NameUseChecker.NameWordIsUsed(str))
            {
                return str;
            }

            num++;
            if (num > 50)
            {
                break;
            }
        }

        return str;
    }

    private List<string> namesFor(RTPN_NameSlot slot, Gender gender)
    {
        return names[(int)gender, (int)slot];
    }
}