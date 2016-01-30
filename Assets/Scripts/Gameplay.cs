using UnityEngine;
using System.Collections;
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


	void Start () {
        instance = this;
        gamePlayPhotonView = PhotonView.Get(this);

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
        timer = 3;
        currentPointsLeft = currentPointsRight = 0;
        maxPoints = 1;
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
                rightPlayerText.text = "You lost :(";
                SoundManager.instance.audioSource.PlayOneShot(defeatClip);
            }
            if (!PhotonNetwork.isMasterClient) {
                leftPlayerText.text = "You win!";
                SoundManager.instance.audioSource.PlayOneShot(victoryClip);
            }
            
        }
       if (currentPointsRight == maxPoints)
        {
            gameOver = true;

            if (PhotonNetwork.isMasterClient)
            {
                rightPlayerText.text = "Victory!";
                SoundManager.instance.audioSource.PlayOneShot(victoryClip);
            }
            if (!PhotonNetwork.isMasterClient)
            {
                leftPlayerText.text = "You got Schooled :(";
                SoundManager.instance.audioSource.PlayOneShot(defeatClip);
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
    public void sendMaxPoints(int data)
    {
       maxPoints = data;
        Debug.Log(maxPoints);
    }

    //If a Player Connects
    
    void OnPhotonPlayerConnected()
    {
        if (PhotonNetwork.isMasterClient)
        {
            sendMaxPoints(maxPoints);
        }
    }

}
