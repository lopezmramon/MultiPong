﻿using UnityEngine;
using System.Collections;

public class LeftLimit : MonoBehaviour {

    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {

            Gameplay.instance.UpdatePoints("Right");
            BallScript.instance.GameRestarted();


        }


    }
}
