using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsEvents : MonoBehaviour
{
    void SoundFootstep1(){
        FindObjectOfType<AudioManager>().Play("PlayerFootStep1");
    }

    void SoundFootstep2(){
        FindObjectOfType<AudioManager>().Play("PlayerFootStep2");
    }

    //This one isnt used. It is activated from script when damage taken
    //TODO Fix later
    void SoundJump(){
        FindObjectOfType<AudioManager>().Play("PlayerJump");
    }

    void SoundRoll(){
        FindObjectOfType<AudioManager>().Play("PlayerRoll");
    }

    void SoundHit(){
        FindObjectOfType<AudioManager>().Play("PlayerHit");
    }

    void SoundDeath(){
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
    }

    void SoundWoosh1(){
        FindObjectOfType<AudioManager>().Play("PlayerWoosh1");
    }

    void SoundWoosh2(){
        FindObjectOfType<AudioManager>().Play("PlayerWoosh2");
    }

    void SoundAttack1(){
        FindObjectOfType<AudioManager>().Play("PlayerAttack1");
    }

    void SoundAttack2(){
        FindObjectOfType<AudioManager>().Play("PlayerAttack2");
    }
}