using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Base class for all elements in the HUD.
/// </summary>
public abstract class HUDElement : MonoBehaviour
{
    /// <summary>The object that is currently displayed in the HUD element.</summary>
    protected RTSObject DisplayObject { get; private set; }

    /// <summary>
    /// Called at the beginning to initialize the HUD element.
    /// </summary>
    void Start()
    {
        DisplayObject = null;

        UpdateObjectInformationInHud();
    }

    /// <summary>
    /// Called every frame to update the information in the HUD element.
    /// </summary>
    void Update()
    {
        UpdateObjectInformationInHud();
    }

    /// <summary>
    /// Set the object whose information is displayed in the HUD element.
    /// </summary>
    /// <param name="displayObject">The object to display in the HUD element.</param>
    public void SetDisplayObject(RTSObject newDisplayObject)
    {
        DisplayObject = newDisplayObject;
        UpdateObjectInformationInHud();
    }

    /// <summary>
    /// Called when the information about the selected object must be updated in the HUD.
    /// </summary>
    protected abstract void UpdateObjectInformationInHud();

    /// <summary>
    /// Called when the element is clicked on the UI.
    /// </summary>
    public virtual void HandleClick()
    {
    }
}
