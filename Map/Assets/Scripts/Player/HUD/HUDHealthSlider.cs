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
    protected override void UpdateObjectInformationInHud()
    {
        if (sliderComponent == null)
            return;

        if (DisplayObject != null)
        {
            sliderComponent.enabled = true;
            sliderComponent.minValue = 0;
            sliderComponent.maxValue = DisplayObject.maxHitPoints;
            sliderComponent.value = DisplayObject.hitPoints;
        }
        else
        {
            sliderComponent.enabled = false;
            sliderComponent.minValue = 0;
            sliderComponent.maxValue = 0;
            sliderComponent.value = 0;
        }
    }
}
