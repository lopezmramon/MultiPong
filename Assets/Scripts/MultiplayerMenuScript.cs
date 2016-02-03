using UnityEngine;
using System.Collections;

public class MultiplayerMenuScript : Photon.MonoBehaviour {
    public AudioClip clip;
	void Start () {
	
	}
	
	void Update () {
	
	}

   public  void JoinRandomGame()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        PhotonNetwork.JoinRandomRoom();
    }
    public void CreateRandomGame()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        PhotonNetwork.JoinRandomRoom();
    }
    public void JoinOrCreateGameByName()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        GameManager.instance.ChangeState(GameManager.GameState.GameCreateMenu);

    }

    public void MainMenu()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        GameManager.instance.ChangeState(GameManager.GameState.MainMenu);
    }
}
