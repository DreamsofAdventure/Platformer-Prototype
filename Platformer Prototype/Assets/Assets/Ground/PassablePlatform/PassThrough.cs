using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThrough : MonoBehaviour
{
    private PlatformEffector2D effector2D;
    public float waitTime;
    private bool isPlayerOn = false;

    void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        //TODO Change keycode for something generic for controller use
        if (Input.GetKeyDown(KeyCode.S) && isPlayerOn){
            waitTime = 0.5f;
            this.gameObject.layer = 0;
        }

        if (waitTime <= 0 && effector2D.rotationalOffset == 180f){
            effector2D.rotationalOffset = 0f;
            this.gameObject.layer = 6;
        }
        if (waitTime > 0)
        {
            effector2D.rotationalOffset = 180f;
            waitTime -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "Player"){
            isPlayerOn = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if (collider.gameObject.tag == "Player"){
            isPlayerOn = false;
        }
    }
}