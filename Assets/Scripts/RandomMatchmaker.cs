using UnityEngine;

public class RandomMatchmaker : Photon.PunBehaviour {
    public static int myID;
    [SerializeField]
    GameObject onlineMPButton;
	void Start () {
        PhotonNetwork.ConnectUsingSettings("1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnPhotonRandomJoinFailed()
    {
        RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
        PhotonNetwork.CreateRoom(null,roomOptions,TypedLobby.Default);
        Gameplay.instance.maxPoints = 5;

    }

    public override void OnJoinedLobby()
    {
        onlineMPButton.SetActive(true);


    }

    public override void OnJoinedRoom()
    {
        GameManager.instance.ChangeState(GameManager.GameState.Gameplay);
        Debug.Log(GameManager.instance.currentState);
        if (PhotonNetwork.isMasterClient)
        {
            GameObject playerpaddle = PhotonNetwork.Instantiate("Paddle", GameObject.Find("LeftPaddleLocation").transform.position, transform.rotation, 0) as GameObject;
            playerpaddle.GetComponent<PaddleController>().enabled = true;
        }
        if (!PhotonNetwork.isMasterClient)
        {
            GameObject playerpaddle = PhotonNetwork.Instantiate("Paddle", GameObject.Find("RightPaddleLocation").transform.position, transform.rotation, 0) as GameObject;
            playerpaddle.GetComponent<PaddleController>().enabled = true;
        }

    }

    //Sending the Max Points for the Server
   

   /* void OnPhotonPlayerConnected()
    {
        if (PhotonNetwork.isMasterClient)
        {
            sendMaxPoints( Gameplay.instance.maxPoints);
            Debug.Log(Gameplay.instance.maxPoints);
        }
    }*/

    void OnPhotonPlayerDisconnected()
    {
        GameManager.instance.ChangeState(GameManager.GameState.MainMenu);

    }

}
