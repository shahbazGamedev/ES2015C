using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    private struct PlayerDetails
    {
        private string name;
        private int avatar;
        public PlayerDetails(string name, int avatar)
        {
            this.name = name;
            this.avatar = avatar;
        }
        public string Name { get { return name; } }
        public int Avatar { get { return avatar; } }
    }
    private static List<PlayerDetails> players = new List<PlayerDetails>();
    private static PlayerDetails currentPlayer;

    void Start () {
	
	}
	
	void Update () {
	
	}
}
