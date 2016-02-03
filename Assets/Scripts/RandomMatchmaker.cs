using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomMatchmaker : Photon.PunBehaviour {
    public static int myID;
    [SerializeField]
    GameObject onlineMPButton;
    List<GameObject> spawnedList;
    float timer;
    public int pointInfo;
    public static RandomMatchmaker instance;

	void Start () {
        instance = this;
        PhotonNetwork.ConnectUsingSettings("1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 30;
        spawnedList = new List<GameObject>();
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
        pointInfo = 5;
        if (PhotonNetwork.isMasterClient)
        {
            GameObject playerpaddle = PhotonNetwork.Instantiate("Paddle", GameObject.Find("LeftPaddleLocation").transform.position, transform.rotation, 0) as GameObject;
            playerpaddle.GetComponent<PaddleController>().enabled = true;
            spawnedList.Add(playerpaddle);
            
        }
        if (!PhotonNetwork.isMasterClient)
        {
            GameObject playerpaddle = PhotonNetwork.Instantiate("Paddle", GameObject.Find("RightPaddleLocation").transform.position, transform.rotation, 0) as GameObject;
            playerpaddle.GetComponent<PaddleController>().enabled = true;
            spawnedList.Add(playerpaddle);
        }

    }

    //Sending the Max Points to the Client
   

    void OnPhotonPlayerConnected()
    {
        if (PhotonNetwork.isMasterClient)
        {
            Gameplay.instance.gamePlayPhotonView.RPC("SendMaxPoints",PhotonTargets.All, pointInfo);
            Debug.Log(Gameplay.instance.maxPoints);
        }
    }

    void OnPhotonPlayerDisconnected()
    {
        spawnedList.ForEach(PhotonNetwork.Destroy);
        if (GameManager.instance.currentState == GameManager.GameState.Gameplay)
        {
            if (Gameplay.instance.currentPointsLeft == Gameplay.instance.maxPoints)
            {
                if (PhotonNetwork.isMasterClient)
                {
                    GameManager.instance.ChangeState(GameManager.GameState.Victory);
                }
                if (!PhotonNetwork.isMasterClient)
                {
                    GameManager.instance.ChangeState(GameManager.GameState.Defeat);

                }

            }
            else if (Gameplay.instance.currentPointsRight == Gameplay.instance.maxPoints)
            {
                if (PhotonNetwork.isMasterClient)
                {
                    GameManager.instance.ChangeState(GameManager.GameState.Defeat);
                }
                if (!PhotonNetwork.isMasterClient)
                {
                    GameManager.instance.ChangeState(GameManager.GameState.Victory);

                }

            }
            
        }

    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        spawnedList.ForEach(Destroy);
        spawnedList.Clear();
    }

    public override void OnDisconnectedFromPhoton()
    {
        base.OnDisconnectedFromPhoton();
        PhotonNetwork.ConnectUsingSettings("1");

    }
    //Ping Testing
   /* void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Debug.Log(PhotonNetwork.networkingPeer.RoundTripTime);
            timer = 1;
        }
    }*/
}
