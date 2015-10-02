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
        foreach (HUDObjectName name in GetComponentsInChildren<HUDObjectName>())
            name.SetDisplayObject(newDisplayObject);
        foreach (HUDObjectHealthSlider slider in GetComponentsInChildren<HUDObjectHealthSlider>())
            slider.SetDisplayObject(newDisplayObject);
        foreach (HUDObjectHealthText text in GetComponentsInChildren<HUDObjectHealthText>())
            text.SetDisplayObject(newDisplayObject);
    }
}
