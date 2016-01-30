using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource audioSource;
    public static SoundManager instance;
	// Use this for initialization
	void Start () {
        instance = this;
        audioSource = GetComponent<AudioSource>();
	}
	
}
