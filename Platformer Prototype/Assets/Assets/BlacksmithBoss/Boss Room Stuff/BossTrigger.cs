using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public Collider2D leftWall;
    public AudioManager audioManager;
    public CameraFollow cameraFollow;
    //public GameObject bossHP;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            leftWall.enabled = true;

            audioManager.Stop("ForestAmbience");
            audioManager.Play("BossArenaAmbience");

            cameraFollow.BossCamera();

            //bossHP.SetActive(true);

            GetComponent<Collider2D>().enabled = false;
        }
    }
}
