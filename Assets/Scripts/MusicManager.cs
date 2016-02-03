using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {

    public AudioClip[] clips;
    public int selection;
    public AudioSource source;
    Text songArtist, songTitle;
    internal static MusicManager instance;

    void Start () {
        instance = this;
        source = GetComponent<AudioSource>();
        selection = Random.Range(0, 3);
        source.clip = clips[selection];
        source.Play();
        songArtist = GameObject.Find("SongArtist").GetComponent<Text>();
        songTitle = GameObject.Find("SongTitle").GetComponent<Text>();
        songArtist.text = "Kevin MacLeod";
        switch (selection)
        {

            case 0:
                songTitle.text = "Ouroboros";
                break;
            case 1:
                songTitle.text = "Pixelland";
                break;
            case 2:
                songTitle.text = "Bit Quest";
                break;


        }

    }
	
}
