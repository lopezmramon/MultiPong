﻿using UnityEngine;
using System.Collections;
using CnControls;

public class LeftPlayer : MonoBehaviour {

    Rigidbody2D r2d;
    [SerializeField]
    float speed;
	void Start () {
        r2d = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
	if (CnInputManager.GetButton("LeftPaddleUp"))
        {
            r2d.velocity = new Vector2(0, speed);

        }
        if (CnInputManager.GetButton("LeftPaddleDown"))
        {

            r2d.velocity = new Vector2(0, -speed);

        }
    }
}
