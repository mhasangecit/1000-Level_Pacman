using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class food : MonoBehaviour
{
    Text scoreText; 
    public static int score=0;
    public static int eatenFood=0;
    button buttonScript;

    void Start()
    {
        scoreText = GameObject.Find("score").GetComponent<Text>();
        buttonScript= GameObject.Find("Canvas").GetComponent<button>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            eatenFood++;
            score += 1;
            scoreText.text = "Score:" + score;

            if (eatenFood >= 25)
            {
                buttonScript.Win();
            }

            Destroy(gameObject);
        }
    }
}
