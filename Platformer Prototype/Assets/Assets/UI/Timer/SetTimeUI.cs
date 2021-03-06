using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetTimeUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float timer;

    void Update()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60F);
	    int seconds = Mathf.FloorToInt(timer % 60F);
	    int milliseconds = Mathf.FloorToInt((timer * 100F) % 100F);
	    text.text = "TIME: " + minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString("00");
        //text.text = "TIME: " + timer.ToString("F0");
    }
}
