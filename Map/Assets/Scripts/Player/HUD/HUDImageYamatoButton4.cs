using UnityEngine;
using UnityEngine.UI;

public class HUDImageYamatoButton4 : HUDElement
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
        if (DisplayObject != null && DisplayObject.imageWall != null)
        {
            imageComponent.sprite = DisplayObject.imageWall;
        }
        else
        {
            imageComponent.sprite = null;
        }
    }
}
