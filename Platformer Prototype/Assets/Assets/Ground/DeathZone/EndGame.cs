using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EndGame : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponentInParent<PlayerStats>().TakeDamage(100);
        }
        else{
            Destroy(collision.gameObject);
        }
    }
}
