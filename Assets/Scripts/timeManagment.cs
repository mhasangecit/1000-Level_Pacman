using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeManagment : MonoBehaviour
{
    public Text timeWriting;
    public float time=5f;
    button buttonScript;


    private void Start()
    {
        buttonScript = GameObject.Find("Canvas").GetComponent<button>();
        InvokeRepeating("coutDown", 0f, 1f);
    }

    void coutDown()
    {
        timeWriting.text = "";
        time -= 1;

        int minute = Mathf.FloorToInt(time / 60);
        int second = Mathf.FloorToInt(time % 60);

        string formattedTime = string.Format("{0:00}:{1:00}", minute, second);

        timeWriting.text = formattedTime;

        if (time <= 0)
        {
            CancelInvoke("coutDown");
            buttonScript.Win();
        }
    }
}
