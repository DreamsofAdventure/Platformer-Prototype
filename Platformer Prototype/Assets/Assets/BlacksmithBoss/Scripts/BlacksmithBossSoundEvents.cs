using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithBossSoundEvents : MonoBehaviour
{
    void SoundFS1(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossFS1");
    }
    void SoundFS2(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossFS2");
    }

    void SoundJump(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossJump");
    }
    public void SoundHit(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossHit");
    }
    void SoundDeath(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossDeath");
    }
    public void SoundTaunt(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossTaunt");
    }

    void SoundAttack1(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossAttack1");
    }
    void SoundAttack2(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossAttack2");
    }
    void SoundAttack3(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossAttack3");
    }
    void SoundAttack4(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossAttack4");
    }
    void SoundAttack5(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossAttack5");
    }
    void SoundAttack6(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossAttack6");
    }

    void SoundWhoosh1(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossWoosh1");
    }
    void SoundWhoosh2(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossWoosh2");
    }
    void SoundWhoosh3(){
        FindObjectOfType<AudioManager>().Play("BlacksmithBossWoosh3");
    }
}