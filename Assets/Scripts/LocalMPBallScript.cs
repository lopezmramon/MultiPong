using UnityEngine;
using System.Collections;
using CnControls;

public class LocalMPBallScript : MonoBehaviour {

    public static LocalMPBallScript instance;
    Rigidbody2D r2d;
    [SerializeField]
    float speed;
    bool gameStart;
    int consecutiveWallHits;
    void Start()
    {
        instance = this;
        gameStart = false;
        r2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!gameStart & (CnInputManager.GetButtonDown("LeftPaddleUp") || CnInputManager.GetButtonDown("LeftPaddleDown") || CnInputManager.GetButtonDown("RightPaddleUp") || CnInputManager.GetButtonDown("RightPaddleDown")))
        {
            Kickoff(speed);

        }
        if (consecutiveWallHits >= 8)
        {
            GameRestarted();
        }

        Debug.Log(r2d.velocity);
    }


    void Kickoff(float Kickoffspeed)
    {
        r2d.AddForce(new Vector2(Random.Range(-Kickoffspeed, Kickoffspeed), Random.Range(-Kickoffspeed, Kickoffspeed)));
        gameStart = true;


    }
    public void GameRestarted()
    {
        consecutiveWallHits = 0;
        gameStart = false;
        transform.position = new Vector3(0, 0, 0);
        r2d.velocity = new Vector2(0, 0);


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "RightWall")
        {
            LocalMultiplayerScript.instance.UpdatePoints("Right");
            GameRestarted();
        }
        if (col.gameObject.name == "LeftWall")
        {
            LocalMultiplayerScript.instance.UpdatePoints("Left");


            GameRestarted();
        }

        if (col.gameObject.CompareTag("Wall"))
        {
            consecutiveWallHits++;
            //Speeding the ball up
           if(r2d.velocity.x < 0)
            {
                r2d.AddForce(new Vector2(-5, 0));
            }
           if (r2d.velocity.x > 0)
            {
                r2d.AddForce(new Vector2(5, 0));
            }
        }
        if (col.gameObject.name.Contains("ddle"))
        {
            consecutiveWallHits = 0;


         
        }

    }
}
