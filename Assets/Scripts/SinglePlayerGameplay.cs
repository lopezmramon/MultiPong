using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CnControls;

public class SinglePlayerGameplay : MonoBehaviour {
    public static SinglePlayerGameplay instance;
    public Text pointsLeft, pointsRight, middleText;

    public int currentPointsLeft, currentPointsRight, maxPoints;

    float timer;
    public bool gameStart, gameStarted = false, gameOver;

    public AudioClip victoryClip, defeatClip;
    [SerializeField]
    GameObject singlePlayerBall,paddle,enemyPaddle;

   public  List<GameObject> spawnedList;

    
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
        GameObject player = Instantiate(paddle, GameObject.Find("RightPaddleLocation").gameObject.transform.position, transform.rotation) as GameObject;
        player.gameObject.name = "RightPaddle";
        spawnedList.Add(player);
        GameObject enemy = Instantiate(enemyPaddle, GameObject.Find("LeftPaddleLocation").gameObject.transform.position, transform.rotation) as GameObject;
        enemy.gameObject.name = "LeftPaddle";
        spawnedList.Add(enemy);
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
           
                middleText.text = "You lost :(";
                SoundManager.instance.audioSource.PlayOneShot(defeatClip);
            FinishGame("Defeat");
           


        }
        if (currentPointsRight == maxPoints)
        {
            gameOver = true;

            middleText.text = "You win!";
            SoundManager.instance.audioSource.PlayOneShot(victoryClip);
            FinishGame("Victory");
            

        }




    }

    void GameStarted()
    {
        timer = 4;
       
            GameObject ball = Instantiate(singlePlayerBall,Vector3.zero,transform.rotation) as GameObject;
        spawnedList.Add(ball);
        gameStart = false;
        gameStarted = true;
    }
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
   public void FinishGame(string condition)
    {
        GooglePlayServices.instance.LeaderboardScore("CgkI78r8qZIMEAIQAQ",currentPointsRight);

        spawnedList.ForEach(Destroy);
        spawnedList.Clear();
        currentPointsLeft = 0;
        currentPointsRight = 0;
        middleText.text = "";
        pointsLeft.text = "Points: " + currentPointsLeft;
        pointsRight.text = "Points: " + currentPointsRight;
        if (condition == "Victory")
        {
            GooglePlayServices.instance.IncreaseEvent("CgkI78r8qZIMEAIQDA",1);
            GooglePlayServices.instance.IncreaseEvent("CgkI78r8qZIMEAIQDw", 1);
            GooglePlayServices.instance.IncrementalAchievement("CgkI78r8qZIMEAIQBg", 1);
            GooglePlayServices.instance.IncrementalAchievement("CgkI78r8qZIMEAIQCw", 1);

            GameManager.instance.ChangeState(GameManager.GameState.Victory);
        }

        if (condition == "Defeat")
        {
            GooglePlayServices.instance.IncreaseEvent("CgkI78r8qZIMEAIQDw", 1);
            GooglePlayServices.instance.IncrementalAchievement("CgkI78r8qZIMEAIQCw", 1);

            GameManager.instance.ChangeState(GameManager.GameState.Defeat);
        }

       

    }
    void OnDisable()
    {
        spawnedList.ForEach(Destroy);
        spawnedList.Clear();
      
        currentPointsLeft = 0;
        currentPointsRight = 0;
        middleText.text = "";
        pointsLeft.text = "Points: " + currentPointsLeft;
        pointsRight.text = "Points: " + currentPointsRight;
    }

}
