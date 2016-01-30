using UnityEngine;
using System.Collections;
using CnControls;
using Photon;

public class PaddleController : UnityEngine.MonoBehaviour {
   private Rigidbody2D r2d;
    [SerializeField]
    float speed;
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (PhotonNetwork.isMasterClient)
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
        if (!PhotonNetwork.isMasterClient)
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
}
