using System.Linq;
using DynamicForms.Library.Core;
using DynamicForms.Library.Core.Attributes;
using TrackerCouncil.Smz3.Shared;
using TrackerCouncil.Smz3.Data.ViewModels;
using TrackerCouncil.Smz3.Shared.Enums;
using YamlDotNet.Serialization;

namespace TrackerCouncil.Smz3.Data.Logic;

[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, name: "Cas' Logic")]
[DynamicFormGroupBasic(DynamicFormLayout.Vertical, name: "CasTop", parentGroup: "Cas' Logic")]
[DynamicFormGroupBasic(DynamicFormLayout.TwoColumns, name: "CasMiddle", parentGroup: "Cas' Logic")]
[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, name: "Tricks and Advanced Logic")]
[DynamicFormGroupBasic(DynamicFormLayout.Vertical, name: "TricksTop", parentGroup: "Tricks and Advanced Logic")]
[DynamicFormGroupBasic(DynamicFormLayout.TwoColumns, name: "TricksMiddle", parentGroup: "Tricks and Advanced Logic")]
[DynamicFormGroupBasic(DynamicFormLayout.SideBySide, name: "TricksBottom", parentGroup: "Tricks and Advanced Logic")]
public class LogicConfig : ViewModelBase
{
    public LogicConfig()
    {

    }

    public LogicConfig(bool enableAllCasOptions, bool enableAllTricks, WallJumpDifficulty wallJumpDifficulty)
    {
        foreach (var property in GetType().GetProperties().Where(x => x.PropertyType == typeof(bool)))
        {
            var attribute = property.GetCustomAttributes(true).Cast<DynamicFormObjectAttribute>()
                .FirstOrDefault(x => x != null);

            if (attribute?.GroupName.StartsWith("Cas") == true)
            {
                property.SetValue(this, enableAllCasOptions);
            }
            else if (attribute?.GroupName.StartsWith("Tricks") == true)
            {
                property.SetValue(this, enableAllTricks);
            }
        }

        WallJumpDifficulty = wallJumpDifficulty;
    }

    [YamlIgnore, Newtonsoft.Json.JsonIgnore]
    [DynamicFormFieldText(groupName: "CasTop")]
    public string CasLogicDescription => "Logic settings that will make the experience more relaxed and easier to play.";

    [DynamicFormFieldCheckBox(checkBoxText: "Prevent Screw Attack Soft Locks", toolTipText: "You're not expected to use Screw Attack without the morph ball.", groupName: "CasMiddle")]
    public bool PreventScrewAttackSoftLock { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Prevent Five Power Bomb Seeds", toolTipText: "You're expected to have at least two power bomb upgrades for navigation in Super Metroid. Will require tracking multiple power bombs.", groupName: "CasMiddle")]
    public bool PreventFivePowerBombSeed { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Left Sand Pit Needs Spring Ball", toolTipText: "You're expected to have the spring ball to navigate the left sand pit in Maridia.", groupName: "CasMiddle")]
    public bool LeftSandPitRequiresSpringBall { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Launchpad Needs Ice Beam", toolTipText: "You're expected to have the ice beam to freeze the boyons to run over to get speed for a shine spark in the old Tourian launchpad in central Crateria.", groupName: "CasMiddle")]
    public bool LaunchPadRequiresIceBeam { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Waterway Needs Gravity Suit", toolTipText: "You're expected to have the gravity suit to be able to access the Waterway in Pink Brinstar.", groupName: "CasMiddle")]
    public bool WaterwayNeedsGravitySuit { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Easy East Crateria Sky Item", toolTipText: "You're expected to have the space jump or speed booster to get the item in the sky in East Crateria after Wrecked Ship.", groupName: "CasMiddle")]
    public bool EasyEastCrateriaSkyItem { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Easy Blue Brinstar Top Access", toolTipText: "You're expected to have the space jump or gravity suit to get to the room up at the top of blue brinstar with the two items.", groupName: "CasMiddle")]
    public bool EasyBlueBrinstarTop { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Kholdstare Needs Somaria", toolTipText: "You're expected to have the Cane of Somaria to get to Kholdstare to prevent from having to clear Ice Palace the vanilla way.", groupName: "CasMiddle")]
    public bool KholdstareNeedsCaneOfSomaria { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Zora Needs Rupee Items", toolTipText: "You're expected to find enough rupee items at locations in order to purchase the item from Zora so that you don't have to farm rupees from enemies, the Houlihan room, etc.", groupName: "CasMiddle")]
    public bool ZoraNeedsRupeeItems { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Include Quarter Magic", toolTipText: "Adds an additional progressive half magic to the item pool.", groupName: "CasMiddle")]
    public bool QuarterMagic { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Prevent Low Resource Dark World", toolTipText: "You're expected to at least 6 hearts total and some weapon that can deal damage before going to the Dark World.", groupName: "CasMiddle")]
    public bool PreventLowResourceDarkWorld { get; set; }

    [YamlIgnore, Newtonsoft.Json.JsonIgnore]
    [DynamicFormFieldText(groupName: "TricksTop")]
    public string TricksDescription => "Logic settings that will make the game more difficult by requiring you to do techniques or maneuvers not typically required in the vanilla games.";

    [DynamicFormFieldCheckBox(checkBoxText: "Fire Rod for Dark Rooms", toolTipText: "You're expected to be able to use the fire rod to light torches for navigating Hyrule Castle escape, Eastern Palace Armos Knights, and select rooms in Palace of Darkness.", groupName: "TricksMiddle")]
    public bool FireRodDarkRooms { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Infinite Bomb Jump", toolTipText: "You're expected to be able to use specially timed morph bombs to access high locations.", groupName: "TricksMiddle")]
    public bool InfiniteBombJump { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Parlor Speed Booster Break In", toolTipText: "You're expected to be able to use the speed booster to burst through to the Terminator Room/Brinstar without morph/power bombs or screw attack.", groupName: "TricksMiddle")]
    public bool ParlorSpeedBooster { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Moat Speed Booster Fly By", toolTipText: "You're expected to be able to use the speed booster to shine spark over the moat in order to get to the Wrecked Ship.", groupName: "TricksMiddle")]
    public bool MoatSpeedBooster { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Mockball", toolTipText: "You're expected to be able to use to mockball to avoid having the speed booster at the entrance to Green Brinstar and Upper Norfair West.", groupName: "TricksMiddle")]
    public bool MockBall { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Sword Only Dark Rooms", toolTipText: "Excluding Hyrule Castle Tower, you're expected to be able to pass through dark rooms without any light and using only your sword to navigate.", groupName: "TricksMiddle")]
    public bool SwordOnlyDarkRooms { get; set; }

    [DynamicFormFieldCheckBox(checkBoxText: "Light World South Fake Flippers", toolTipText: "You're expected to be able to use fake flippers in the Light World South near Lake Hylia to access under the bridge and, if you have the moon pearl, the waterfall fairy chests.", groupName: "TricksMiddle")]
    public bool LightWorldSouthFakeFlippers { get; set; }

    [DynamicFormFieldComboBox(label: "Wall jump difficulty:", toolTipText: "The maximum difficulty of wall jumps you're comfortable with.", groupName: "TricksBottom")]
    public WallJumpDifficulty WallJumpDifficulty { get; set; }
        = WallJumpDifficulty.Medium; // Not very cas’, but this seems to be closest to previous assumptions

    public LogicConfig Clone()
    {
        return (LogicConfig)MemberwiseClone();
    }
}
