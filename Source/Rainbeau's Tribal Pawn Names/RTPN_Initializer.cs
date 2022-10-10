using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RTPN_Code;

[StaticConstructorOnStartup]
internal static class RTPN_Initializer
{
    private static Dictionary<PawnNameCategory, RTPN_NameBank> banks;

    static RTPN_Initializer()
    {
        var harmony = new Harmony("net.rainbeau.rimworld.mod.pawnnames");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        LongEventHandler.QueueLongEvent(Setup, "LibraryStartup", false, null);
    }

    public static void Setup()
    {
        banks = new Dictionary<PawnNameCategory, RTPN_NameBank>
        {
            { PawnNameCategory.HumanStandard, new RTPN_NameBank(PawnNameCategory.HumanStandard) }
        };
        var nameBank = BankOf(PawnNameCategory.HumanStandard);
        nameBank.AddNamesFromFile(RTPN_NameSlot.Tribal, Gender.Female, "Tribal_Name_Female.txt");
        nameBank.AddNamesFromFile(RTPN_NameSlot.Tribal, Gender.Male, "Tribal_Name_Male.txt");
        nameBank.AddNamesFromFile(RTPN_NameSlot.Desc, Gender.Male, "Tribal_Adjectives.txt");
        nameBank.AddNamesFromFile(RTPN_NameSlot.Desc, Gender.Female, "Tribal_Colors.txt");
        nameBank.AddNamesFromFile(RTPN_NameSlot.Desc, Gender.None, "Tribal_FactionUnits.txt");
        nameBank.AddNamesFromFile(RTPN_NameSlot.Object, Gender.Female, "Tribal_Animals.txt");
        nameBank.AddNamesFromFile(RTPN_NameSlot.Object, Gender.None, "Tribal_Terrains.txt");
        nameBank.AddNamesFromFile(RTPN_NameSlot.Object, Gender.Male, "Tribal_Weapons.txt");
        foreach (var value in banks.Values)
        {
            value.ErrorCheck();
        }
    }

    public static RTPN_NameBank BankOf(PawnNameCategory category)
    {
        return banks[category];
    }
}