using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Behaviour to control the object health display (as a slider) in the HUD.
/// </summary>
public class HUDHealthSlider : HUDElement
{
    private Slider sliderComponent;

    void Start()
    {
        sliderComponent = GetComponent<Slider>();
        if (sliderComponent == null)
        {
            Debug.Log("Script of type " + GetType().Name + " without a Slider component won't work.");
        }
    }

    /// <summary>
    /// Updates the object health in the HUD slider.
    /// </summary>
    void Update()
    {
        if (sliderComponent == null)
            return;

        // Show/hide the slider (to do it, hide the images that make up the components)
        // Making the current gameobject inactive also works, but then this script won't execute again, oops.
        foreach (var img in GetComponentsInChildren<Image>())
            img.enabled = (DisplayObject != null);

        // Set the slider to the object's current HP
        if (DisplayObject != null)
        {
            sliderComponent.minValue = 0;
            sliderComponent.maxValue = DisplayObject.maxHitPoints;
            sliderComponent.value = DisplayObject.hitPoints;
        }
    }
}
