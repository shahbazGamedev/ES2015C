using UnityEngine;
using System.Collections;
using System;

public class RTSElement : MonoBehaviour {
    /// <summary>
    /// Reference to the currently selected RTS element.
    /// </summary>
    public static RTSElement SelectedElement { get; set; }

    /// <summary>
    /// Called when the RTS element selection begins.
    /// </summary>
    protected virtual void OnSelectionStart()
    {
        Debug.Log("RTS element selected: " + name);
    }

    /// <summary>
    /// Called when the RTS element selection end.
    /// </summary>
    protected virtual void OnSelectionEnd()
    {
        Debug.Log("RTS element unselected: " + name);
    }

    /// <summary>
    /// Changes the currently selected RTS element, or unselects it.
    /// </summary>
    /// <param name="objectRtsElement">The new RTS element to select, or null to unselect any element.</param>
    internal static void ChangeSelection(RTSElement objectRtsElement)
    {
        if (SelectedElement != null)
        {
            SelectedElement.OnSelectionEnd();
        }

        SelectedElement = objectRtsElement;

        if (SelectedElement != null)
        {
            SelectedElement.OnSelectionStart();
        }
    }
}
