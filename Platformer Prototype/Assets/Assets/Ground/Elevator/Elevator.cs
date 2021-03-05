using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    bool isUp = false;
    bool playerTouch = false;

    public Transform downPos;
    public Transform upPos;
    public Rigidbody2D rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerTouch && isUp == false){
            rb.velocity = Vector2.up * 4f;
        }
        else if (playerTouch && isUp){
            rb.velocity = Vector2.down * 4f;
        }
        else{
            rb.velocity = Vector2.zero;
        }

        if (this.transform.position.y > upPos.position.y){
            isUp = true;
            playerTouch = false;
        }
        else if (this.transform.position.y < downPos.position.y){
            isUp = false;
            playerTouch = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerTouch = true;
        }
    }
}
