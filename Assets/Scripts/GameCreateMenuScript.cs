using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameCreateMenuScript : Photon.MonoBehaviour {

    private InputField gameName, gamePoints;
    public AudioClip clip;
    void Start()
    {
        gameName = GameObject.Find("GameName").gameObject.GetComponent<InputField>();
        gamePoints = GameObject.Find("MaxPoints").gameObject.GetComponent<InputField>();

    }

	public void JoinGamebyName()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(gameName.text, roomOptions, TypedLobby.Default); 
    }
    public void CreateGamebyName()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        if (int.TryParse(gamePoints.text,out RandomMatchmaker.instance.pointInfo))
        {
            RandomMatchmaker.instance.pointInfo = int.Parse(gamePoints.text);
            RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
            PhotonNetwork.JoinOrCreateRoom(gameName.text, roomOptions, TypedLobby.Default);

        }
        else if(!int.TryParse(gamePoints.text, out RandomMatchmaker.instance.pointInfo))
        {
            gamePoints.text = "You didn't input a number, silly";
        }
       

    }

    public void MultiplayerMenu()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        GameManager.instance.ChangeState(GameManager.GameState.MultiplayerMenu);

    }

    
}
