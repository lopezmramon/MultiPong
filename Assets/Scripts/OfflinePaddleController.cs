using UnityEngine;
using System.Collections;
using CnControls;

public class OfflinePaddleController : MonoBehaviour {

    private Rigidbody2D r2d;
    [SerializeField]
    float speed;
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameObject.name == "LeftPaddle")
        {
            if (CnInputManager.GetButton("LeftPaddleUp"))
            {
                r2d.velocity = new Vector2(0, speed);

            }
            if (CnInputManager.GetButton("LeftPaddleDown"))
            {

                r2d.velocity = new Vector2(0, -speed);

            }
        }
        if (gameObject.name == "RightPaddle")
        {
            if (CnInputManager.GetButton("RightPaddleUp"))
            {
                r2d.velocity = new Vector2(0, speed);

            }
            if (CnInputManager.GetButton("RightPaddleDown"))
            {

                r2d.velocity = new Vector2(0, -speed);

            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            


        }









    }
}
