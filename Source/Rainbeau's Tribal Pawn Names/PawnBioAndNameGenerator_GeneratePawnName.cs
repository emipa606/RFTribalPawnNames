using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RTPN_Code
{
    [HarmonyPatch(typeof(PawnBioAndNameGenerator), "GeneratePawnName")]
    public static class PawnBioAndNameGenerator_GeneratePawnName
    {
        [HarmonyPriority(Priority.VeryHigh)]
        public static bool Prefix(Pawn pawn, ref Name __result, NameStyle style = 0, string forcedLastName = null)
        {
            if (style != NameStyle.Full)
            {
                return true;
            }

            var nameGenerator = pawn.RaceProps.GetNameGenerator(pawn.gender);
            if (nameGenerator != null)
            {
                if (!nameGenerator.defName.Contains("NamerAnimalGeneric"))
                {
                    return true;
                }

                if (pawn.Faction == null || !pawn.Faction.def.defName.Contains("Tribe") &&
                    pawn.Faction.def.defName != "TribalRaiders")
                {
                    return true;
                }

                var nameBank = RTPN_Initializer.BankOf(PawnNameCategory.HumanStandard);
                var name = nameBank.GetName(RTPN_NameSlot.Tribal, pawn.gender);
                if (Rand.Value < 0.33f)
                {
                    string subname2;
                    var nickDesc = Rand.Value;
                    var subname1 = nameBank.GetName(RTPN_NameSlot.Desc, nickDesc < 0.25 ? Gender.Female : Gender.Male);

                    var nickObject = Rand.Value;
                    if (nickObject < 0.33)
                    {
                        subname2 = nameBank.GetName(RTPN_NameSlot.Object, Gender.Male);
                    }
                    else if (nickObject < 0.67)
                    {
                        subname2 = nameBank.GetName(RTPN_NameSlot.Object, Gender.Female);
                    }
                    else
                    {
                        subname2 = nameBank.GetName(RTPN_NameSlot.Object);
                    }

                    name = Rand.Value < 0.1 ? subname2 : string.Concat(subname1, " ", subname2);
                }

                __result = new NameSingle(name);
                return false;
            }

            if (pawn.Faction == null || !pawn.Faction.def.allowedCultures.Any())
            {
                return true;
            }

            foreach (var defAllowedCulture in pawn.Faction.def.allowedCultures)
            {
                if (defAllowedCulture.pawnNameMaker == null)
                {
                    continue;
                }

                if (!defAllowedCulture.pawnNameMaker.defName.Contains("NamerPersonCorunan"))
                {
                    continue;
                }

                string name2;
                var nameBank = RTPN_Initializer.BankOf(PawnNameCategory.HumanStandard);
                var name3 = nameBank.GetName(RTPN_NameSlot.Tribal, pawn.gender);
                var name1 = nameBank.GetName(RTPN_NameSlot.Tribal, pawn.gender);
                var num = 0;
                do
                {
                    num++;
                    if (Rand.Value >= 0.33f)
                    {
                        name2 = Rand.Value >= 0.67f ? name3 : name1;
                    }
                    else
                    {
                        string subname2;
                        var nickDesc = Rand.Value;
                        var subname1 = nameBank.GetName(RTPN_NameSlot.Desc,
                            nickDesc < 0.25 ? Gender.Female : Gender.Male);

                        var nickObject = Rand.Value;
                        if (nickObject < 0.33)
                        {
                            subname2 = nameBank.GetName(RTPN_NameSlot.Object, Gender.Male);
                        }
                        else if (nickObject < 0.67)
                        {
                            subname2 = nameBank.GetName(RTPN_NameSlot.Object, Gender.Female);
                        }
                        else
                        {
                            subname2 = nameBank.GetName(RTPN_NameSlot.Object);
                        }

                        name2 = Rand.Value < 0.1 ? subname2 : string.Concat(subname1, " ", subname2);
                    }
                } while (num < 50 &&
                         NameUseChecker.AllPawnsNamesEverUsed.Any(x =>
                             x is NameTriple nameTriple && nameTriple.Nick == name2));

                name1 = name1 + " '" + name2 + "'";
                var fullName = NameTriple.FromString(name1 + " " + name3);
                fullName.CapitalizeNick();
                fullName.ResolveMissingPieces();
                __result = fullName;
                return false;
            }

            return true;
        }
    }
}