using UnityEngine;
using System.Collections;

public class EnemyPaddle : MonoBehaviour {

    public float speed;
    GameObject ball;


	void Start () {
        ball = GameObject.Find("SinglePlayerBall(Clone)");

    }
	
	void FixedUpdate () {
        if (ball)
        {
            if (transform.position.y < GameObject.Find("SinglePlayerBall(Clone)").gameObject.transform.position.y)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * speed;
            }
            else if (transform.position.y > GameObject.Find("SinglePlayerBall(Clone)").gameObject.transform.position.y)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * speed;
            }
            else {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0) * speed;
            }

        }else if (!ball)
        {
           ball =  GameObject.Find("SinglePlayerBall(Clone)");
        }

	}



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            


        }









    }
}
