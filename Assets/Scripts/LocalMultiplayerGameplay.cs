using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CnControls;

public class LocalMultiplayerGameplay : MonoBehaviour {

    public static LocalMultiplayerGameplay instance;
    public Text pointsLeft, pointsRight, middleText;

    public int currentPointsLeft, currentPointsRight, maxPoints;

    float timer;
    public bool gameStart, gameStarted = false, gameOver;

    public AudioClip victoryClip;
    [SerializeField]
    GameObject localMPBall, paddle;
    List<GameObject> spawnedList; 


    void Awake()
    {
        instance = this;
        spawnedList = new List<GameObject>();

        //Finding GameObjects
        middleText = GameObject.Find("MiddleText").gameObject.GetComponent<Text>();
        pointsLeft = GameObject.Find("PointsLeft").gameObject.GetComponent<Text>();
        pointsRight = GameObject.Find("PointsRight").gameObject.GetComponent<Text>();
        pointsLeft.text = "Points: 0";
        pointsRight.text = "Points: 0";


        gameStart = false;
        timer = 3;
        currentPointsLeft = currentPointsRight = 0;
        maxPoints = 5;
    }
    void OnEnable()
    {
        gameStarted = false;
        GameObject player1 = Instantiate(paddle, GameObject.Find("RightPaddleLocation").gameObject.transform.position, transform.rotation) as GameObject;
        player1.gameObject.name = "RightPaddle";
        spawnedList.Add(player1);

        GameObject player2 = Instantiate(paddle, GameObject.Find("LeftPaddleLocation").gameObject.transform.position, transform.rotation) as GameObject;
        player2.gameObject.name = "LeftPaddle";
        spawnedList.Add(player2);

    }

    void Update()
    {

        if (!gameStarted)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 3 & timer > 2)
        {
            middleText.text = "3...";
        }

        else if (timer <= 2 & timer > 1)
        {
            middleText.text = "2...";

        }
        else if (timer <= 1 & timer > 0)
        {

            middleText.text = "1...";


        }
        else if (timer <= 0)
        {

            middleText.text = "Tap to Play!";


            gameStart = true;
        }


        if (gameStart)
        {
            if (CnInputManager.GetButtonDown("LeftPaddleUp") || CnInputManager.GetButtonDown("LeftPaddleDown") || CnInputManager.GetButtonDown("RightPaddleUp") || CnInputManager.GetButtonDown("RightPaddleDown"))
            {
                middleText.text = "";
                GameStarted();

            }
        }

        //Handling Player Victory or defeat
        if (currentPointsLeft == maxPoints)
        {
            gameOver = true;

            middleText.text = "Left Player Won!";
            SoundManager.instance.audioSource.PlayOneShot(victoryClip);
            FinishGame("Victory");



        }
        if (currentPointsRight == maxPoints)
        {
            gameOver = true;

            middleText.text = "Right Player Wins!";
            SoundManager.instance.audioSource.PlayOneShot(victoryClip);
            FinishGame("Victory");
        }




    }

    void GameStarted()
    {
        timer = 4;

        GameObject ball = Instantiate(localMPBall, Vector3.zero, transform.rotation) as GameObject;
        spawnedList.Add(ball);

        gameStart = false;
        gameStarted = true;
    }
    public void UpdatePoints(string side)
    {
        if (side == "Right")
        {
            GooglePlayServices.instance.IncreaseEvent("CgkI78r8qZIMEAIQEA", 1);
            currentPointsLeft++;
            pointsLeft.text = "Points: " + currentPointsLeft;

        }
        if (side == "Left")
        {
            GooglePlayServices.instance.IncreaseEvent("CgkI78r8qZIMEAIQEA", 1);

            currentPointsRight++;
            pointsRight.text = "Points: " + currentPointsRight;

        }


    }

   public void FinishGame(string condition)
    {
        if (currentPointsLeft > currentPointsRight)
        {
            GooglePlayServices.instance.LeaderboardScore("CgkI78r8qZIMEAIQAg", currentPointsLeft);
            GooglePlayServices.instance.LeaderboardScore("CgkI78r8qZIMEAIQAw", currentPointsLeft - currentPointsRight);
        }
        else {

            GooglePlayServices.instance.LeaderboardScore("CgkI78r8qZIMEAIQAw", currentPointsRight - currentPointsLeft);
            GooglePlayServices.instance.LeaderboardScore("CgkI78r8qZIMEAIQAg", currentPointsRight);

        }
        spawnedList.ForEach(Destroy);
        spawnedList.Clear();
        currentPointsLeft = 0;
        currentPointsRight = 0;
        middleText.text = "";

        pointsLeft.text = "Points: " + currentPointsLeft;
        pointsRight.text = "Points: " + currentPointsRight;
        if (condition == "Victory")
        {
            GooglePlayServices.instance.IncreaseEvent("CgkI78r8qZIMEAIQDw", 1);
            GooglePlayServices.instance.IncrementalAchievement("CgkI78r8qZIMEAIQBw", 1);
            GooglePlayServices.instance.IncrementalAchievement("CgkI78r8qZIMEAIQCQ", 1);
            GooglePlayServices.instance.IncrementalAchievement("CgkI78r8qZIMEAIQCw", 1);

            GameManager.instance.ChangeState(GameManager.GameState.Victory);
        }
        if (condition == "Defeat")
        {
            GooglePlayServices.instance.IncreaseEvent("CgkI78r8qZIMEAIQDw", 1);
            GooglePlayServices.instance.IncrementalAchievement("CgkI78r8qZIMEAIQCQ", 1);
            GooglePlayServices.instance.IncrementalAchievement("CgkI78r8qZIMEAIQCw", 1);

            GameManager.instance.ChangeState(GameManager.GameState.Defeat);
        }


    }


}
