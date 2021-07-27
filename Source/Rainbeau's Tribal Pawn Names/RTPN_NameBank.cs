using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RimWorld;
using Verse;

namespace RTPN_Code
{
    public class RTPN_NameBank
    {
        private static readonly int numGenders = Enum.GetValues(typeof(Gender)).Length;
        private static readonly int numSlots = Enum.GetValues(typeof(RTPN_NameSlot)).Length;

        private readonly string modBasePath = LoadedModManager.RunningMods
            .First(mcp => mcp.assemblies.loadedAssemblies.Contains(typeof(RTPN_Initializer).Assembly)).RootDir;

        private readonly List<string>[,] names;
        public PawnNameCategory nameType;

        public RTPN_NameBank(PawnNameCategory ID)
        {
            nameType = ID;
            names = new List<string>[numGenders, numSlots];
            for (var i = 0; i < numGenders; i++)
            {
                for (var j = 0; j < numSlots; j++)
                {
                    names[i, j] = new List<string>();
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

        public void AddNames(RTPN_NameSlot slot, Gender gender, IEnumerable<string> namesToAdd)
        {
            var enumerator = namesToAdd.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    NamesFor(slot, gender).Add(current);
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
            AddNames(slot, gender, RTPN_FileRead.LinesFromFile(string.Concat(namesPath, fileName)));
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
                            Log.Error(string.Concat("Duplicated name: ", enumerator1.Current));
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

                            Log.Error(string.Concat("Trimmable whitespace on name: [", str, "]"));
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
            var strs = NamesFor(slot, gender);
            var num = 0;
            if (strs.Count == 0)
            {
                Log.Error(string.Concat("Name list for gender=", gender, " slot=", slot, " is empty."));
                return "Errorname";
            }

            while (true)
            {
                str = strs.RandomElement();
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

        public List<string> NamesFor(RTPN_NameSlot slot, Gender gender)
        {
            return names[(int) gender, (int) slot];
        }
    }
}