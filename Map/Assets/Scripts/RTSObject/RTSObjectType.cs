using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Available kinds of objects.
/// </summary>
public enum RTSObjectType
{
    UnitArcher,
    UnitArcherAdvanced,
    UnitCavalry,
    UnitCavalryAdvanced,
    UnitCivil,
    UnitCivilAxe,
    UnitCivilPick,
    UnitCivilRack,
    UnitWarrior,
    UnitWarriorAdvanced,

    BuildingAcademy,
    BuildingArmyBuilding,
    BuildingCivilHouse,
    BuildingTownCenter,
    BuildingWall,
    BuildingWallEntrance,
    BuildingWallTower
};

/// <summary>
/// Utilities for working with the RTSObjectType enum.
/// </summary>
public static class RTSObjectTypeExt
{
    /// <summary>
    /// Mapping from object types to their names.
    /// </summary>
    static Dictionary<RTSObjectType, string> objectNames = new Dictionary<RTSObjectType, string>()
    {
        { RTSObjectType.UnitArcher, "Archer" },
        { RTSObjectType.UnitArcherAdvanced, "Archer Advanced" },
        { RTSObjectType.UnitCavalry, "Cavalry" },
        { RTSObjectType.UnitCavalryAdvanced, "Cavalry Advanced" },
        { RTSObjectType.UnitWarrior, "Warrior" },
        { RTSObjectType.UnitWarriorAdvanced, "Warrior Advanced" },
        { RTSObjectType.UnitCivil, "Civil Unit" },
        { RTSObjectType.UnitCivilAxe, "Civil Unit Axe" },
        { RTSObjectType.UnitCivilPick, "Civil Unit Pick" },
        { RTSObjectType.UnitCivilRack, "Civil Unit Rack" },
        { RTSObjectType.BuildingTownCenter, "Town Center" },
        { RTSObjectType.BuildingArmyBuilding, "Army Building" },
        { RTSObjectType.BuildingWallTower, "Wall Tower" },
        { RTSObjectType.BuildingWallEntrance, "Wall Entrance" },
        { RTSObjectType.BuildingWall, "Wall" },
        { RTSObjectType.BuildingCivilHouse, "Civil House" },
        { RTSObjectType.BuildingAcademy, "Academy" }
    };
    /// <summary>
    /// Mapping from names to their object types.
    /// </summary>
    static Dictionary<string, RTSObjectType> objectNamesRev = objectNames.ToDictionary(kv => kv.Value, kv => kv.Key);

    /// <summary>
    /// Get a human-readable name for the given RTS object type.
    /// </summary>
    /// <param name="objectType">The type of the object.</param>
    /// <returns>A human-readable name corresponding to the object type.</returns>
    public static string GetObjectName(RTSObjectType objectType)
    {
        return objectNames[objectType];
    }

    /// <summary>
    /// Get the corresponding RTSObjectType from its human-readable name.
    /// </summary>
    /// <param name="objectName">The human-readable name of the object.</param>
    /// <returns>The type of the object</returns>
    public static RTSObjectType GetObjectTypeFromName(string objectName)
    {
        return objectNamesRev[objectName];
    }
}