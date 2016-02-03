using UnityEngine;
using System.Collections;
using Photon;

public class EndCanvas : Photon.MonoBehaviour {

    public AudioClip clip;
	
    void OnEnable()
    {
        if(PhotonNetwork.connected)
        PhotonNetwork.Disconnect();

    }

    public void MainMenu()
    {
        if(!PhotonNetwork.connected)
        PhotonNetwork.ConnectUsingSettings("1");
        SoundManager.instance.audioSource.PlayOneShot(clip);
        GameManager.instance.ChangeState(GameManager.GameState.MainMenu);

    }
    public void Quit()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        Application.Quit();

    }
}
