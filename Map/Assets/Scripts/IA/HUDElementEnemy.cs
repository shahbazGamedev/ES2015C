using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Base class for all elements in the HUD.
/// </summary>
public abstract class HUDElementEnemy : MonoBehaviour
{
    /// <summary>The player for which the information in the HUD is displayed.</summary>
    protected Player EnemyPlayer1
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
            return EnemyPlayer1.SelectedObject;
        }
    }

    /// <summary>
    /// Called when the element is clicked on the UI.
    /// </summary>
    public virtual void HandleClick()
    {
    }
}
