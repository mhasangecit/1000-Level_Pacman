using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuStart : MonoBehaviour
{
    public GameObject blackPanel;
    Animator bpAnim;

    void Start()
    {
        bpAnim = blackPanel.GetComponent<Animator>();
        StartCoroutine(bpanel());
    }

    void Update()
    {
        
    }

    IEnumerator bpanel()
    {
        bpAnim.SetInteger("blackPanel", 0);
        yield return new WaitForSeconds(1f);
        blackPanel.SetActive(false);
    }

    public void Play()
    {
        StartCoroutine(openBp(1));
    }

    public void HighScores()
    {

    }

    public void Credits()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator openBp(int sceneIndex)
    {
        blackPanel.SetActive(true);
        Time.timeScale = 1f;
        bpAnim.SetInteger("blackPanel", -1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }
}
