using UnityEngine;
using UnityEngine.EventSystems;

public class MenuStartGame : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData data)
    {
        // Load the second level of the scene, which should be the game itself
        Application.LoadLevel(1);
    }
}
