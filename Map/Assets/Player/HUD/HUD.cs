using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    /// <summary>
    /// Set the object whose information is displayed in the HUD.
    /// </summary>
    /// <param name="displayObject">The object to display in the HUD.</param>
    public void SetDisplayObject(RTSObject newDisplayObject)
    {
        // Pass the object to each of the HUD's constituting components
        foreach (HUDElement elm in GetComponentsInChildren<HUDElement>())
            elm.SetDisplayObject(newDisplayObject);
    }
}
