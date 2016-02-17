using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CnControls;

public class Gameplay : Photon.MonoBehaviour {
    public PhotonView gamePlayPhotonView;
    public static Gameplay instance;
    public Text pointsLeft, pointsRight;

    public int currentPointsLeft, currentPointsRight, maxPoints;
    
    private Text rightPlayerText, leftPlayerText;
    float timer;
    public bool playersInRoom, gameStart,gameStarted, gameOver;

    public AudioClip victoryClip, defeatClip;
    public List<GameObject> spawnedList;

	void Awake () {
        instance = this;
        gamePlayPhotonView = PhotonView.Get(this);
        spawnedList = new List<GameObject>();
        


        //Finding GameObjects
        rightPlayerText = GameObject.Find("RightPlayerText").gameObject.GetComponent<Text>();
        leftPlayerText = GameObject.Find("LeftPlayerText").gameObject.GetComponent<Text>();
        pointsLeft = GameObject.Find("PointsLeft").gameObject.GetComponent<Text>();
        pointsRight = GameObject.Find("PointsRight").gameObject.GetComponent<Text>();
        pointsLeft.text = "Points: 0";
        pointsRight.text = "Points: 0";


        playersInRoom = false;
        gameStart = false;
        gameStarted = false;
        gameOver = false;
        timer = 3;
        currentPointsLeft = currentPointsRight = 0;
        maxPoints = 5;
    }

    void OnEnable()
    {
        currentPointsLeft = currentPointsRight = 0;
        pointsLeft.text = "Points: " + currentPointsLeft;
        pointsRight.text = "Points: " + currentPointsRight;
        playersInRoom = false;
        gameStart = false;
        gameStarted = false;
        gameOver = false;
        timer = 3;
        

    }
	void OnJoinedRoom()
    {
           
        if (PhotonNetwork.isMasterClient)
        {
            rightPlayerText.enabled = false;
            leftPlayerText.enabled = true;
        }
        if (!PhotonNetwork.isMasterClient)
        {
            leftPlayerText.enabled = false;
            rightPlayerText.enabled = true;
        }
    }
	void Update () {
        Debug.Log(maxPoints);
        //If there are 2 players in the room, players ARE IN!
        if (PhotonNetwork.playerList.Length == 2) playersInRoom = true;
      
        //When both players are in lobby, start countdown for game.
        if (playersInRoom & !gameStarted)
        {
            timer -= Time.deltaTime;
        }

        if (timer == 3)
        {
            if (PhotonNetwork.isMasterClient)
            {
                leftPlayerText.text = "Waiting for Player";
            }
            if (!PhotonNetwork.isMasterClient)
            {
                rightPlayerText.text = "Waiting for Player";
            }

        }
        else if (timer < 3 & timer >2) {
            if (PhotonNetwork.isMasterClient)
            {
                leftPlayerText.text = "3...";
            }
            if (!PhotonNetwork.isMasterClient)
            {
                rightPlayerText.text = "3...";
            }
        }

        else if (timer <= 2 & timer >1 )
        {
            if (PhotonNetwork.isMasterClient)
            {
                leftPlayerText.text = "2...";
            }
            if (!PhotonNetwork.isMasterClient)
            {
                rightPlayerText.text = "2...";
            }
        }
        else if (timer <= 1 & timer >0)
        {
            if (PhotonNetwork.isMasterClient)
            {
                leftPlayerText.text = "1...";
            }
            if (!PhotonNetwork.isMasterClient)
            {
                rightPlayerText.text = "1...";
            }

        }
        else if (timer <= 0)
        {
            if (PhotonNetwork.isMasterClient)
            {
                leftPlayerText.text = "Tap to Play!";
            }
            if (!PhotonNetwork.isMasterClient)
            {
                rightPlayerText.text = "Tap to Play!";
            }
            gameStart = true;
        }

        //When both players are in, countdown has finished, we wait for a tap and then

        if(gameStart)
        {
            if (PhotonNetwork.isMasterClient & CnInputManager.GetButtonDown("LeftPaddleUp") || CnInputManager.GetButtonDown("LeftPaddleDown") || CnInputManager.GetButtonDown("RightPaddleUp") || CnInputManager.GetButtonDown("RightPaddleDown"))
            {
                rightPlayerText.text = "";
                leftPlayerText.text = "";
                GameStarted();
                
            }
        }

        //Handling Player Victory or defeat
       if (currentPointsLeft == maxPoints)
        {
            gameOver = true;
            if (PhotonNetwork.isMasterClient) {
                gamePlayPhotonView.RPC("FinishGame", PhotonTargets.All, "Victory");
                SoundManager.instance.audioSource.PlayOneShot(victoryClip);
            }
            if (!PhotonNetwork.isMasterClient) {
                gamePlayPhotonView.RPC("FinishGame", PhotonTargets.All, "Defeat");

                SoundManager.instance.audioSource.PlayOneShot(defeatClip);
            }
            
        }
       if (currentPointsRight == maxPoints)
        {
            gameOver = true;

            if (PhotonNetwork.isMasterClient)
            {
              gamePlayPhotonView.RPC("FinishGame",PhotonTargets.All,"Defeat");
                SoundManager.instance.audioSource.PlayOneShot(defeatClip);
            }
            if (!PhotonNetwork.isMasterClient)
            {
                gamePlayPhotonView.RPC("FinishGame", PhotonTargets.All, "Victory");

                SoundManager.instance.audioSource.PlayOneShot(victoryClip);
            }

        }




	}

    void GameStarted()
    {
        timer = 4;
        if (PhotonNetwork.isMasterClient)
        {
            GameObject ball = PhotonNetwork.Instantiate("Ball", Vector3.zero, transform.rotation, 0);
            ball.GetComponent<BallScript>().enabled = true;
            spawnedList.Add(ball);
        }
        gameStart = false;
        gameStarted = true;
    }
    [PunRPC]
    public void UpdatePoints(string side)
    {
        if (side == "Right")
        {
            currentPointsLeft++;
            pointsLeft.text = "Points: " + currentPointsLeft;

        }
        if (side == "Left")
        {
            currentPointsRight++;
            pointsRight.text = "Points: " + currentPointsRight;

        }


    }
    //MaxPoints
    [PunRPC]
    public void SendMaxPoints(int data)
    {
       maxPoints = data;
    }
    [PunRPC]
    public void FinishGame(string condition)
    {
        spawnedList.ForEach(PhotonNetwork.Destroy);
        spawnedList.Clear();
        currentPointsLeft = 0;
        currentPointsRight = 0;

        if (condition == "Victory")
        {
            GooglePlayServices.instance.IncreaseEvent("CgkI78r8qZIMEAIQDg", 1);
            GooglePlayServices.instance.IncreaseEvent("CgkI78r8qZIMEAIQDw", 1);
            GooglePlayServices.instance.UnlockableAchievement("CgkI78r8qZIMEAIQCA", 100.0f);
            GooglePlayServices.instance.IncrementalAchievement("CgkI78r8qZIMEAIQCg", 1);
            GooglePlayServices.instance.IncrementalAchievement("CgkI78r8qZIMEAIQCw", 1);
            GameManager.instance.ChangeState(GameManager.GameState.Victory);
        }
        if (condition == "Defeat")
        {
            GooglePlayServices.instance.IncreaseEvent("CgkI78r8qZIMEAIQDw", 1);
            GooglePlayServices.instance.UnlockableAchievement("CgkI78r8qZIMEAIQCA", 100.0f);
            GooglePlayServices.instance.IncrementalAchievement("CgkI78r8qZIMEAIQCw", 1);

            GameManager.instance.ChangeState(GameManager.GameState.Defeat);
        }

        PhotonNetwork.Disconnect();


    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    void OnDisable()
    {
        spawnedList.ForEach(PhotonNetwork.Destroy);
        spawnedList.Clear();
        currentPointsLeft = 0;
        currentPointsRight = 0;
        PhotonNetwork.Disconnect();


    }

}
