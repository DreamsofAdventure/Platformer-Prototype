using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndMsg : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Win(){
        text.text = "YOU WIN!";
    }

    public void Lose(){
        text.text = "YOU LOSE!";
    }
}
