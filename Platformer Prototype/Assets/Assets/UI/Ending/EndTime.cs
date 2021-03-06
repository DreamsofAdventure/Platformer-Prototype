using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndTime : MonoBehaviour
{
    public float time;
    public SetTimeUI timerUI;
    public TextMeshProUGUI text;
    public GameObject timerGO;
    public GameObject player;


    public void SetEndTime(){
        time = timerUI.timer;
        int minutes = Mathf.FloorToInt(time / 60F);
	    int seconds = Mathf.FloorToInt(time % 60F);
	    int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);
	    text.text = "TIME: " + minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString("00");

        timerGO.SetActive(false);

        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerCombat>().enabled = false;
    }
}