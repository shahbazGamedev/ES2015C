using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Behaviour to display the name of the player who owns a given RTSObject.
/// </summary>
public class HUDOwner : HUDElement
{
    private Text textComponent;

    // DisplayObject.player is not implemented yet.
    // Use a mock instead and it will require to be changed later.
    private Player DisplayObjectPlayerMock;

    void Start()
    {
        DisplayObjectPlayerMock = transform.root.GetComponent<Player>();

        textComponent = GetComponent<Text>();
        if (textComponent == null)
        {
            Debug.Log("Script of type " + GetType().Name + " without a Text component won't work.");
        }
    }

    /// <summary>
    /// Updates the object action in the HUD.
    /// </summary>
    protected override void UpdateObjectInformationInHud()
    {
        if (textComponent == null)
            return;

        if (DisplayObject != null)
        {
            textComponent.text = DisplayObjectPlayerMock.username;
        }
        else
        {
            textComponent.text = "";
        }
    }
}
