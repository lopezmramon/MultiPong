using UnityEngine;
using System.Collections;
using CnControls;

public class BallScript : MonoBehaviour {
    public static BallScript instance;
    Rigidbody2D r2d;
    [SerializeField]
    float speed;
     bool gameStart;

	void Start () {
        instance = this;
        r2d = GetComponent<Rigidbody2D>();
        gameStart = false;
	}
	
	void Update () {
	if (!gameStart & (CnInputManager.GetButtonDown("LeftPaddleUp")|| CnInputManager.GetButtonDown("LeftPaddleDown") || CnInputManager.GetButtonDown("RightPaddleUp") || CnInputManager.GetButtonDown("RightPaddleDown")))
        {
            Kickoff(speed);

        }
	}


    void Kickoff(float Kickoffspeed)
    {
        r2d.AddForce(new Vector2(Random.Range(-Kickoffspeed, 0.75f*Kickoffspeed), Random.Range(-Kickoffspeed,0.75f* Kickoffspeed)));
        gameStart = true;


    }
    public void GameRestarted()
    {
        gameStart = false;
        transform.position = new Vector3(0, 0, 0);
        r2d.velocity = new Vector2(0, 0);


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "RightWall")
        {
            Gameplay.instance.UpdatePoints("Right");
            GameRestarted();
        }
        if (col.gameObject.name == "LeftWall")
        {
            Gameplay.instance.UpdatePoints("Left");
            GameRestarted();
        }

        if (col.gameObject.CompareTag("Wall"))
        {
            if (Mathf.Abs(r2d.velocity.y) <= 0.5f)
            {
                r2d.AddForce(new Vector2(-Mathf.Sign(r2d.velocity.x) * 1.5f, -Mathf.Sign(r2d.velocity.y) * 5f));
            }
            else {
                r2d.AddForce(new Vector2(-Mathf.Sign(r2d.velocity.x) * 1.5f, -Mathf.Sign(r2d.velocity.y) * 1.5f));
            }
        }

    }
}
