using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSwitchToNewGameMenu : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData data)
    {
        GameObject.Find("Canvas").GetComponent<MenuSwitcher>().SelectMenu("PanelNewGame");
    }
}
