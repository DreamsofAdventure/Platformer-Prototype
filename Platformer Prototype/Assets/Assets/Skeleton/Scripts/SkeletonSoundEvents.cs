using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSoundEvents : MonoBehaviour
{
    void SoundHit(){
        FindObjectOfType<AudioManager>().Play("SkeletonHit");
    }

    void SoundDeath(){
        FindObjectOfType<AudioManager>().Play("SkeletonDeath");
    }

    void SoundAttack(){
        FindObjectOfType<AudioManager>().Play("SkeletonAttack");
    }

    void SoundWhoosh(){
        FindObjectOfType<AudioManager>().Play("SkeletonWoosh");
    }
}
