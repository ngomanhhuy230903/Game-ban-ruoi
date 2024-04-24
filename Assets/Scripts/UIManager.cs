using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Scoretext;
    public GameObject Gameoverpanel;
    public void SetScoreText(string txt)
    {
        if (Scoretext)
        {
            Scoretext.text = txt;
        }
    }
    public void Showgameoverpanel(bool state)
    {
        if (Gameoverpanel)
        {
            Gameoverpanel.SetActive(state);
        }
    }
}
