using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Applies the options selected in the options menu.
/// </summary>
public class MenuActionApplyOptions : MonoBehaviour
{
    public Toggle toggleFullScreen;

    public void ApplyOptions()
    {
        // Get fullscreen parameter
        if (toggleFullScreen != null)
        {
            Screen.fullScreen = toggleFullScreen.isOn;
        }
        else
        {
            Debug.LogError("Fullscreen toggle not associated.");
        }
    }
}
