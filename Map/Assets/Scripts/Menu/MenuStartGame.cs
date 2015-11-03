using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuStartGame : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData data)
    {
        Dropdown dropDownCampaign = GameObject.Find("DropdownSelectCampaign").GetComponent<Dropdown>();

        // All scenes after the main menu are the campaign: 1 is the first campaign, 2 is the second, etc.
        Application.LoadLevel(1+dropDownCampaign.value);
    }
}
