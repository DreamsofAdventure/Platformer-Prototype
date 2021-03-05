using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionJump : MonoBehaviour
{
    public GameObject playerGO;
    PlayerMovement playerMov;

    //Colliders
    public Collider2D playerBaseColl;
    public Collider2D playerFeetColl;
    public Collider2D thisFeetColl;

    public bool collisionTrue = true;


    void Start()
    {
        playerMov = playerGO.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(collisionTrue){
            Physics2D.IgnoreCollision(playerBaseColl, thisFeetColl, false);
            Physics2D.IgnoreCollision(playerFeetColl, thisFeetColl, false);
        }
        else{
            Physics2D.IgnoreCollision(playerBaseColl, thisFeetColl);
            Physics2D.IgnoreCollision(playerFeetColl, thisFeetColl);
        }
    }


    //To be used in animation
    void IgnoreCollisionFeetTrue(){
        collisionTrue = false;
    }
    void IgnoreCollisionFeetFalse(){
        collisionTrue = true;
    }
}
