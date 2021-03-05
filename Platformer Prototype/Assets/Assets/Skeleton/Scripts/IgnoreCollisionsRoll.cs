using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionsRoll : MonoBehaviour
{
    public GameObject playerGO;
    PlayerMovement playerMov;

    //Colliders
    public Collider2D playerBaseColl;
    public Collider2D playerFeetColl;
    public Collider2D thisBaseColl;
    public Collider2D thisFeetColl;

    bool isJumping = false;


    void Start()
    {
        playerMov = playerGO.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerMov.isRolling == true || isJumping){
            Physics2D.IgnoreCollision(playerBaseColl, thisBaseColl);
            Physics2D.IgnoreCollision(playerBaseColl, thisFeetColl);
            Physics2D.IgnoreCollision(playerFeetColl, thisBaseColl);
            Physics2D.IgnoreCollision(playerFeetColl, thisFeetColl);
        }
        else{
            Physics2D.IgnoreCollision(playerBaseColl, thisBaseColl, false);
            Physics2D.IgnoreCollision(playerBaseColl, thisFeetColl, false);
            Physics2D.IgnoreCollision(playerFeetColl, thisBaseColl, false);
            Physics2D.IgnoreCollision(playerFeetColl, thisFeetColl, false);
        }
    }


    //To be used in animation
    void IgnoreCollisionFeetTrue(){
        isJumping = true;
    }
    void IgnoreCollisionFeetFalse(){
        isJumping = false;
    }
}
