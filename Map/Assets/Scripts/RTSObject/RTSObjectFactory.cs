using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Utility class used to easily get prefab references for the various objects available in the game.
/// </summary>
public static class RTSObjectFactory
{
    /// <summary>
    /// Resource prefab names for each object type and civilization.
    /// </summary>
    private static readonly Dictionary<RTSObjectType, Dictionary<PlayerCivilization, string>> resourceNames =
        new Dictionary<RTSObjectType, Dictionary<PlayerCivilization, string>>()
        {
            { RTSObjectType.UnitArcher, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_archer" },
                { PlayerCivilization.Persians, "Prefabs/Persian_archer" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_archer" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_archer" },
            } },
            { RTSObjectType.UnitArcherAdvanced, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_archer_advanced" },
                { PlayerCivilization.Persians, "Prefabs/Persian_archer_advanced" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_archer_advanced" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_archer_advanced" },
            } },
            { RTSObjectType.UnitCavalry, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_cavalry" },
                { PlayerCivilization.Persians, "Prefabs/Persian_cavalry" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_cavalry" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_cavalry" },
            } },
            { RTSObjectType.UnitCavalryAdvanced, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_cavalry_advanced" },
                { PlayerCivilization.Persians, "Prefabs/Persian_cavalry_advanced" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_cavalry_advanced" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_cavalry_advanced" },
            } },
            { RTSObjectType.UnitCivil, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_civil" },
                { PlayerCivilization.Persians, "Prefabs/Persian_civil" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_civil" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_civil" },
            } },
            { RTSObjectType.UnitCivilAxe, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_civil_axe" },
                { PlayerCivilization.Persians, "Prefabs/Persian_civil_axe" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_civil_axe" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_civil_axe" },
            } },
            { RTSObjectType.UnitCivilPick, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_civil_pick" },
                { PlayerCivilization.Persians, "Prefabs/Persian_civil_pick" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_civil_pick" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_civil_pick" },
            } },
            { RTSObjectType.UnitCivilRack, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_civil_rack" },
                { PlayerCivilization.Persians, "Prefabs/Persian_civil_rack" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_civil_rack" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_civil_rack" },
            } },
            { RTSObjectType.UnitWarrior, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_warrior" },
                { PlayerCivilization.Persians, "Prefabs/Persian_warrior" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_warrior" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_samurai" }, // (Samurai = Warrior)
            } },
            { RTSObjectType.UnitWarriorAdvanced, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_warrior_advanced" },
                { PlayerCivilization.Persians, "Prefabs/Persian_warrior_advanced" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_warrior_advanced" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_samurai_advanced" }, // (Samurai = Warrior)
            } },

            { RTSObjectType.BuildingAcademy, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_Academy" },
                { PlayerCivilization.Persians, "Prefabs/Persian_Academy" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_Academy" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_Academy" },
            } },
            { RTSObjectType.BuildingArmyBuilding, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_ArmyBuilding" },
                { PlayerCivilization.Persians, "Prefabs/Persian_ArmyBuilding" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_ArmyBuilding" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_ArmyBuilding" },
            } },
            { RTSObjectType.BuildingCivilHouse, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_CivilHouse" },
                { PlayerCivilization.Persians, "Prefabs/Persian_CivilHouse" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_CivilHouse" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_CivilHouse" },
            } },
            { RTSObjectType.BuildingTownCenter, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_TownCenter" },
                { PlayerCivilization.Persians, "Prefabs/Persian_TownCenter" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_TownCenter" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_TownCenter" },
            } },
            { RTSObjectType.BuildingWall, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_Wall" },
                { PlayerCivilization.Persians, "Prefabs/Persian_Wall" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_Wall" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_Wall" },
            } },
            { RTSObjectType.BuildingWallEntrance, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_WallEntrance" },
                { PlayerCivilization.Persians, "Prefabs/Persian_WallEntrance" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_WallEntrance" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_WallEntrance" },
            } },
            { RTSObjectType.BuildingWallTower, new Dictionary<PlayerCivilization, string>()
            {
                { PlayerCivilization.Hittites, "Prefabs/Hittite_WallTower" },
                { PlayerCivilization.Persians, "Prefabs/Persian_WallTower" },
                { PlayerCivilization.Sumerians, "Prefabs/Sumerian_WallTower" },
                { PlayerCivilization.Yamato, "Prefabs/Yamato_WallTower" },
            } }
        };

    /// <summary>
    /// Get the default object resource prefab for the specified civilization.
    /// </summary>
    /// <param name="type">The type of object to load.</param>
    /// <param name="civilization">The civilization of the player.</param>
    /// <param name="useFallback">If resource is not available for given civilization, return an equivalent for another civilization.</param>
    /// <returns>A reference to the object resource prefab, or null if not available.</returns>
    public static GameObject GetObjectTemplate(RTSObjectType type, PlayerCivilization civilization, bool useFallback = false)
    {
        // Validate input arguments correctness
        if (!Enum.IsDefined(typeof(RTSObjectType), type))
            throw new ArgumentOutOfRangeException("type");

        if (!Enum.IsDefined(typeof(PlayerCivilization), civilization))
            throw new ArgumentOutOfRangeException("civilization");

        // Load list of prefabs for the given object
        if (!resourceNames.ContainsKey(type))
            throw new NotSupportedException("The object type '" + type + "' doesn't have its prefabs defined. " +
                "Define them in " + typeof(RTSObjectFactory) + ".");

        var prefabsForObject = resourceNames[type];

        // Try to load the resource for the exact civilization passed as an argument
        if (prefabsForObject.ContainsKey(civilization))
        {
            var loadedResource = LoadAndCheckPrefab(prefabsForObject[civilization], true);
            if (loadedResource != null)
                return loadedResource;
        }

        if (useFallback)
        {
            // If the resource couldn't be loaded and useFallback was given, try to load the object
            // Iterate over the civilizations in order in order to get a consistent behaviour
            // (dictionary enumeration order is undefined)
            foreach (var fallbackCivilization in prefabsForObject.Keys.Where(k => k != civilization).OrderBy(k => k))
            {
                var loadedResource = LoadAndCheckPrefab(prefabsForObject[fallbackCivilization], false);
                if (loadedResource != null)
                {
                    Debug.LogWarning("Using model for civilization " + fallbackCivilization + " for object of type " +
                        type + " because no model is available for civilization " + civilization + ".");
					HUDInfo.insertMessage("Object " + type + " not available for civilization " + civilization + "; Using fallback.");
                    return loadedResource;
                }
            }

            throw new NotSupportedException("No prefab available for object type '" + type + " for any civilization. " +
                "Check the prefab definition in " + typeof(RTSObjectFactory) + ".");
        }

        return null;
    }

    /// <summary>
    /// Loads a prefab (as in Resources.Load) and checks that the prefab is correctly configured.
    /// </summary>
    /// <param name="prefabPath">The path of the prefab. Same as in Resources.Load.</param>
    /// <param name="warnNotExisting">True to warn if the prefab doesn't exist, false otherwise.</param>
    /// <returns>The instance of the prefab, or null if it couldn't be found.</returns>
    private static GameObject LoadAndCheckPrefab(string prefabPath, bool warnNotExisting)
    {
        // Try to load the prefab resource
        var prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab == null)
        {
            if (warnNotExisting)
            {
                HUDInfo.insertMessage("Error loading prefab '" + prefabPath + "': Resource does not exist.");
                Debug.LogWarning("Error loading prefab '" + prefabPath + "': Resource does not exist.");
            }

            return null;
        }

        // Check that the prefab has one, and only one, RTSObject script
        // In this case we will just print a warning but return it anyway to get the model working at least
        var scripts = prefab.GetComponents<RTSObject>();
        if (scripts.Length == 0)
        {
            HUDInfo.insertMessage("Error loading prefab '" + prefabPath + "': Object doesn't have a RTSObject script associated.");
            Debug.LogWarning("Error loading prefab '" + prefabPath + "': Object doesn't have a RTSObject script associated.");
        }
        else if (scripts.Length >= 2)
        {
            HUDInfo.insertMessage("Error loading prefab '" + prefabPath + "': Object has multiple RTSObject scripts associated.");
            Debug.LogWarning("Error loading prefab '" + prefabPath + "': Object has multiple RTSObject scripts associated.");
        }

        return prefab;
    }

    class ObjectInfo
    {
        public int Cost { get; set; }
        public Sprite ObjectIconSprite { get; set; }
    }

    static Dictionary<KeyValuePair<RTSObjectType, PlayerCivilization>, ObjectInfo> memoizedObjectInfo
        = new Dictionary<KeyValuePair<RTSObjectType, PlayerCivilization>, ObjectInfo>();

    /// <summary>
    /// Memoize, if required, the information for the specified object and civilization which is initialized on the start script.
    /// </summary>
    /// <param name="type">The type of the object.</param>
    /// <param name="civilization">The civilization of the object.</param>
    private static ObjectInfo GetObjectInfo(RTSObjectType type, PlayerCivilization civilization)
    {
        // We have a pretty big problem here, which is that the costs of the object
        // are actually defined in the object initialization scripts and not in the prefab
        // For this reason, to get the cost, we need to actually initialize the object,
        // get the cost, and destroy it again. Since this is pretty costly, once we get it,
        // memoize the value to avoid recaculating it in future calls
        if (!memoizedObjectInfo.ContainsKey(new KeyValuePair<RTSObjectType, PlayerCivilization>(type, civilization)))
        {
            // Get object template. If not existing return empty ObjectInfo.
            var objectTemplate = GetObjectTemplate(type, civilization);
            if (objectTemplate == null)
            {
                memoizedObjectInfo[new KeyValuePair<RTSObjectType, PlayerCivilization>(type, civilization)] = new ObjectInfo();
                return memoizedObjectInfo[new KeyValuePair<RTSObjectType, PlayerCivilization>(type, civilization)];
            }

            // Initialize object instance and script. Otherwise return empty ObjectInfo.
            var objectInstance = GameObject.Instantiate(objectTemplate);
            try
            {
                var objectScript = objectInstance.GetComponent<RTSObject>();
                if (objectScript == null)
                {
                    memoizedObjectInfo[new KeyValuePair<RTSObjectType, PlayerCivilization>(type, civilization)] = new ObjectInfo();
                    return memoizedObjectInfo[new KeyValuePair<RTSObjectType, PlayerCivilization>(type, civilization)];
                }

                // Get properties from initialized script
                memoizedObjectInfo[new KeyValuePair<RTSObjectType, PlayerCivilization>(type, civilization)] = new ObjectInfo
                {
                    Cost = objectScript.cost,
                    ObjectIconSprite = objectScript.objectIconSprite
                };
            }
            finally
            {
                // Make sure we destroy the temporary GameObject we created
                GameObject.Destroy(objectInstance);
            }
        }

        return memoizedObjectInfo[new KeyValuePair<RTSObjectType, PlayerCivilization>(type, civilization)];
    }

    /// <summary>
    /// Get the cost of creating the specified object for the specified civilization.
    /// </summary>
    /// <param name="type">The type of the object to create.</param>
    /// <param name="civilization">The civilization of the object to create.</param>
    /// <returns>The cost in resources required to create the object</returns>
    public static int GetObjectCost(RTSObjectType type, PlayerCivilization civilization)
    {
        return GetObjectInfo(type, civilization).Cost;
    }

    /// <summary>
    /// Get the preview icon sprite the specified object for the specified civilization.
    /// </summary>
    /// <param name="type">The type of the object.</param>
    /// <param name="civilization">The civilization of the object.</param>
    /// <returns>The preview icon sprite for the required object.</returns>
    public static Sprite GetObjectIconSprite(RTSObjectType type, PlayerCivilization civilization)
    {
        return GetObjectInfo(type, civilization).ObjectIconSprite;
    }

    /// <summary>
    /// Prints a table to the log showing which objects are available for each civilization.
    /// </summary>
    public static void PrintAvailableObjectTable(StreamWriter sw)
    {
        // Print header
        sw.Write("|{0,20}", "OBJECT");
        foreach (PlayerCivilization civ in Enum.GetValues(typeof(PlayerCivilization)))
        {
            sw.Write(string.Format("|{0,9}", civ.ToString().ToUpper()));
        }
        sw.WriteLine("|");

        // Print values
        foreach (RTSObjectType type in Enum.GetValues(typeof(RTSObjectType)))
        {
            sw.Write(string.Format("|{0,20}", type));
            foreach (PlayerCivilization civ in Enum.GetValues(typeof(PlayerCivilization)))
            {
                GameObject objectTemplate = GetObjectTemplate(type, civ, false);
                RTSObject objectScript = (objectTemplate != null) ? objectTemplate.GetComponent<RTSObject>() : null;
                sw.Write(string.Format("|    {0}    ", (objectTemplate != null) ? ((objectScript != null) ? 'X' : 'N') : ' '));
            }
            sw.WriteLine("|");
        }
    }
}