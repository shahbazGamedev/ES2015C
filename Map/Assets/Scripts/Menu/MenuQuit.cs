using UnityEngine;
using UnityEngine.EventSystems;

public class MenuQuit : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData data)
    {
        Application.Quit();
    }
}
