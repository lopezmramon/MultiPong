using UnityEngine;
using System.Collections;

public class MultiplayerMenuScript : Photon.MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

   public  void JoinRandomGame()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void CreateRandomGame()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void JoinOrCreateGameByName()
    {
        GameManager.instance.ChangeState(GameManager.GameState.GameCreateMenu);

    }

    public void MainMenu()
    {

        GameManager.instance.ChangeState(GameManager.GameState.MainMenu);
    }
}
