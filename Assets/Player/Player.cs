using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public string username;
    public bool human;
    public RTSObject SelectedObject { get; set; }
    public int startMoney, startPower;
    public Color teamColor;

    private bool findingPlacement = false;
    
    void Start () {
	
	}
	
	void Update () {
	
	}
}
