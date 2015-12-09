using UnityEngine;
using UnityEngine.UI;

public class HUDImageYamatoButton0 : HUDElement
{
    private Image imageComponent;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.Log("Script of type " + GetType().Name + " without a Text component won't work.");
        }
    }

    /// <summary>
    /// Update the icon related to the selected unit.
    /// </summary>
	void Update()
    {
        if (DisplayObject != null && DisplayObject.imageTownCenter != null)
        {
            imageComponent.sprite = DisplayObject.imageTownCenter;
        }
        else
        {
            imageComponent.sprite = null;
        }
    }
}
