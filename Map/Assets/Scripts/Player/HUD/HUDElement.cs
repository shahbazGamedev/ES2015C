using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Base class for all elements in the HUD.
/// </summary>
public abstract class HUDElement : MonoBehaviour
{
    /// <summary>
    /// The sprites corresponding to each type of resource.
    /// </summary>
    private Dictionary<RTSObject.ResourceType, Sprite> resourceSprites;

    /// <summary>
    /// Get the HUD sprite corresponding the given resource.
    /// </summary>
    /// <param name="resourceType">The type of the resource to get.</param>
    /// <returns>HUD resource sprite corresponding to the given resource type.</returns>
    protected Sprite GetResourceSprite(RTSObject.ResourceType resourceType)
    {
        // Lazily load the resource sprites.
        if (resourceSprites == null)
        {
            resourceSprites = new Dictionary<RTSObject.ResourceType, Sprite>();
            resourceSprites[RTSObject.ResourceType.Gold] = Resources.Load<Sprite>("HUD/Resource/GoldIcon");
            resourceSprites[RTSObject.ResourceType.Food] = Resources.Load<Sprite>("HUD/Resource/FoodIcon");
            resourceSprites[RTSObject.ResourceType.Wood] = Resources.Load<Sprite>("HUD/Resource/WoodIcon");
        }

        return resourceSprites[resourceType];
    }

    /// <summary>The player for which the information in the HUD is displayed.</summary>
    protected Player Player
    {
        get
        {
            return GetComponentInParent<Player>();
        }
    }

    /// <summary>Object for which information is displayed in the HUD, or null to specify that no information is shown.</summary>
    protected RTSObject DisplayObject
    {
        get
        {
            return Player.SelectedObject;
        }
    }
}
