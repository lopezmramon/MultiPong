using UnityEngine;
using System.Collections;

public class OnlineGameplay : MonoBehaviour {
    public bool playersAreIn;
    public float playTimer;
    public static OnlineGameplay instance;
    // Use this for initialization
    void Start () {
        playersAreIn = false;
        playTimer = 3;
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
