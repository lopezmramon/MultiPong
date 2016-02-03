using UnityEngine;
using System.Collections;
using CnControls;

public class BallScript : MonoBehaviour {
    public static BallScript instance;
    private PhotonView myPhotonView;
    Rigidbody2D r2d;
    [SerializeField]
    float speed;
     bool gameStart;
    int consecutiveWallHits,consecutivePaddleHits;

    public AudioClip[] audios;


    void Start () {
        instance = this;
        r2d = GetComponent<Rigidbody2D>();
        gameStart = false;
        myPhotonView = GetComponent<PhotonView>();
        consecutiveWallHits = 0;
        consecutivePaddleHits = 0;
	}
	
	void Update () {
	if (!gameStart & (CnInputManager.GetButtonDown("LeftPaddleUp")|| CnInputManager.GetButtonDown("LeftPaddleDown") || CnInputManager.GetButtonDown("RightPaddleUp") || CnInputManager.GetButtonDown("RightPaddleDown")))
        {
            Kickoff(speed);

        }

        if (consecutiveWallHits >= 10)
        {
            GameRestarted();
        }
        if (consecutivePaddleHits >= 6)
        {
            GameRestarted();

        }
        r2d.AddForce(new Vector2(0.001f * Mathf.Sign(r2d.velocity.x), 0));

    }


    void Kickoff(float Kickoffspeed)
    {
       int whereToGo = Random.Range(0, 2);
        int leftOrRight = Random.Range(-2, 2);
        float horizontalMultiplier = Random.Range(0.5f, 0.95f);
        switch (whereToGo)
        {
            case 0:
                r2d.AddForce(new Vector2(Mathf.Sign(leftOrRight)* Kickoffspeed *horizontalMultiplier, Random.Range(0, Kickoffspeed)));

                break;
            case 1:
                r2d.AddForce(new Vector2(Mathf.Sign(leftOrRight) * Kickoffspeed * horizontalMultiplier,Random.Range( -Kickoffspeed, 0)));

                break;
        }
        
        gameStart = true;


    }
    public void GameRestarted()
    {
        consecutiveWallHits = 0;
        consecutivePaddleHits = 0;
        gameStart = false;
        transform.position = new Vector3(0, 0, 0);
        r2d.velocity = new Vector2(0, 0);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "RightWall" & !Gameplay.instance.gameOver)
        {
            SoundManager.instance.audioSource.pitch = Random.Range(0.45f, 1f);
            SoundManager.instance.audioSource.PlayOneShot(audios[3]);
            consecutiveWallHits = 0;
            Gameplay.instance.gamePlayPhotonView.RPC("UpdatePoints", PhotonTargets.All,"Right");
            GameRestarted();
        }
        if (col.gameObject.name == "LeftWall" & !Gameplay.instance.gameOver)
        {
            SoundManager.instance.audioSource.pitch = Random.Range(0.45f, 1f);
            SoundManager.instance.audioSource.PlayOneShot(audios[3]);

            consecutiveWallHits = 0;
            Gameplay.instance.gamePlayPhotonView.RPC("UpdatePoints", PhotonTargets.All, "Left");

            // Gameplay.instance.UpdatePoints("Left");

            GameRestarted();
        }

        if (col.gameObject.CompareTag("Wall"))
        {
            consecutivePaddleHits = 0;
            SoundManager.instance.audioSource.pitch = Random.Range(0.45f, 1f);
            SoundManager.instance.audioSource.PlayOneShot(audios[0]);
            consecutiveWallHits++;
            //Speeding the ball up
            if (r2d.velocity.x < 0)
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
            consecutivePaddleHits++;
            SoundManager.instance.audioSource.pitch = Random.Range(0.45f, 1f);
            SoundManager.instance.audioSource.PlayOneShot(audios[Random.Range(1,3)]);
        }
       

    }

   
}

