using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSwitchToMainMenuMenu : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData data)
    {
        GameObject.Find("Canvas").GetComponent<MenuSwitcher>().SelectMenu("PanelMainMenu");
    }
}
