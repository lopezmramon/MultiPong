using UnityEngine;
using System.Collections;

public class EnemyPaddle : MonoBehaviour {

    public float speed;
    GameObject ball;


	void Start () {
        ball = GameObject.Find("SinglePlayerBall(Clone)");

    }
	
	void Update () {
        if (ball)
        {
            if (transform.position.y < GameObject.Find("SinglePlayerBall(Clone)").gameObject.transform.position.y)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * speed*Time.deltaTime;
            }
            else if (transform.position.y > GameObject.Find("SinglePlayerBall(Clone)").gameObject.transform.position.y)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * speed* Time.deltaTime;
            }
            else {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0) * speed* Time.deltaTime;
            }

        }else if (!ball)
        {
           ball =  GameObject.Find("SinglePlayerBall(Clone)");
        }

	}
}
