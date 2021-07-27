using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RTPN_Code
{
    [HarmonyPatch(typeof(NameGenerator), "GenerateName", typeof(RulePackDef), typeof(Predicate<string>), typeof(bool),
        typeof(string), typeof(string))]
    public static class NameGenerator_GenerateName
    {
        [HarmonyPriority(Priority.VeryHigh)]
        public static bool Prefix(RulePackDef rootPack, ref string __result, Predicate<string> validator = null)
        {
            if (rootPack != null && rootPack.defName.Contains("NamerFactionTribal"))
            {
                var nameBank = RTPN_Initializer.BankOf(PawnNameCategory.HumanStandard);
                string name1;
                string name3;
                var format = Rand.Value;
                var factionUnit = nameBank.GetName(RTPN_NameSlot.Desc);
                switch (format)
                {
                    case < 0.25f:
                        name1 = "The " + factionUnit + " of the ";
                        name3 = "";
                        break;
                    case < 0.5f:
                        name1 = factionUnit + " of the ";
                        name3 = "";
                        break;
                    case < 0.75f:
                        name1 = "The ";
                        name3 = " " + factionUnit;
                        break;
                    default:
                        name1 = "";
                        name3 = " " + factionUnit;
                        break;
                }

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

                var name2 = string.Concat(subname1, " ", subname2);
                __result = name1 + name2 + name3;
                return false;
            }

            if (rootPack == null || !rootPack.defName.Contains("NamerSettlementTribal"))
            {
                return true;
            }

            var humanNameBank = RTPN_Initializer.BankOf(PawnNameCategory.HumanStandard);
            string name;
            var randFormat = Rand.Value;
            switch (randFormat)
            {
                case < 0.25f:
                    name = humanNameBank.GetName(RTPN_NameSlot.Tribal, Gender.Female);
                    break;
                case < 0.5f:
                    name = humanNameBank.GetName(RTPN_NameSlot.Tribal, Gender.Male);
                    break;
                default:
                {
                    var nickDesc = Rand.Value;
                    var subname1 = humanNameBank.GetName(RTPN_NameSlot.Desc,
                        nickDesc < 0.25 ? Gender.Female : Gender.Male);

                    var subname2 = humanNameBank.GetName(RTPN_NameSlot.Object);
                    name = string.Concat(subname1, " ", subname2);
                    break;
                }
            }


            for (var j = 0; j < 100; j++)
            {
                for (var k = 0; k < 5; k++)
                {
                    var titleCaseSmart1 = name;
                    if (j != 0)
                    {
                        titleCaseSmart1 = string.Concat(titleCaseSmart1, " ", j + 1);
                    }

                    if (validator != null && !validator(titleCaseSmart1))
                    {
                        continue;
                    }

                    __result = titleCaseSmart1;
                    return false;
                }
            }


            __result = name;
            return false;
        }
    }
}