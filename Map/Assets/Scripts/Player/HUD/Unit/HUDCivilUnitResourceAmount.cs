using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the amount of harvested resource for a civil unit.
/// </summary>
public class HUDCivilUnitResourceAmount : HUDElement
{
    private Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
        if (textComponent == null)
        {
            Debug.Log("Script of type " + GetType().Name + " without a Text component won't work.");
        }
    }

    /// <summary>
    /// Update the amount of harvested resource that is shown in the HUD.
    /// </summary>
	void Update()
    {
        float? harvestedAmount = null;
        if (DisplayObject != null && DisplayObject.IsHarvesting())
            harvestedAmount = DisplayObject.GetHarvestAmount();

        if (harvestedAmount.HasValue)
        {
            textComponent.enabled = true;
			textComponent.text = Mathf.Round(harvestedAmount.Value).ToString();
        }
        else
        {
            textComponent.enabled = false;
        }
    }
}
