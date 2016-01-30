using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameCreateMenuScript : Photon.MonoBehaviour {

    private InputField gameName, gamePoints;
    void Start()
    {
        gameName = GameObject.Find("GameName").gameObject.GetComponent<InputField>();
        gamePoints = GameObject.Find("MaxPoints").gameObject.GetComponent<InputField>();

    }

	public void JoinGamebyName()
    {
        RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(gameName.text, roomOptions, TypedLobby.Default);
    }
    public void CreateGamebyName()
    {
        if(int.TryParse(gamePoints.text,out Gameplay.instance.maxPoints))
        {
            Gameplay.instance.maxPoints = int.Parse(gamePoints.text);
            RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
            PhotonNetwork.JoinOrCreateRoom(gameName.text, roomOptions, TypedLobby.Default);

        }
        else if(!int.TryParse(gamePoints.text, out Gameplay.instance.maxPoints))
        {
            gamePoints.text = "You didn't input a number, silly";
        }
       

    }

    public void MultiplayerMenu()
    {
        GameManager.instance.ChangeState(GameManager.GameState.MultiplayerMenu);

    }

    
}
