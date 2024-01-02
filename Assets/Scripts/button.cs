using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject blackPanel;
    public GameObject gameOverPanel;
    public GameObject menuButon;
    public GameObject player;
    public GameObject LevelManager;

    Text gameOverText;
    Animator panelAnim;
    Animator blackPanelAnim;
    int sceneIndex;
    bool win = false;

    void Start()
    {
        LevelManager = GameObject.Find("levelManager");
        panelAnim = pausePanel.GetComponent<Animator>();
        blackPanelAnim = blackPanel.GetComponent<Animator>();
        gameOverText = gameOverPanel.transform.GetChild(0).GetComponent<Text>();
        gameOverText.text = "";
        sceneIndex = SceneManager.GetActiveScene().buildIndex; 
        blackPanelAnim.SetInteger("blackPanel",0);
        StartCoroutine(removeBp());
    }

    void Update()
    {
        
    }

    

    void menuButton()
    {
        panelAnim.SetTrigger("panel");
        StartCoroutine(TimeScale(0));
    }

    IEnumerator TimeScale(int i)
    {
        yield return new WaitForSeconds(0.35f);
        Time.timeScale = i;
    }

    public void ResumeGame()
    {
        panelAnim.SetTrigger("panel");
        Time.timeScale = 1; //ilk StartCoroutine(TimeScale(1)); yazdim ama bu kod niye calismadi, anlamadim
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void turnMainMenu()
    {
        StartCoroutine(openBp(0));
    }

    public void Restart()
    {
        food.score = 0;
        food.eatenFood = 0;

        if (!win)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Time.timeScale = 1f;
            LevelManager.GetComponent<levelManager>().variableSettings(sceneIndex);
        }
    }

    public void GameOver()
    {
        menuButon.SetActive(false);
        player.GetComponent<Animator>().SetBool("death", true);
        player.GetComponent<pacmanControl>().speed=0;
        StartCoroutine(gameOver());

        if(food.score!=0)
            gameOverText.text = "Game Over\n\nScore:"+food.score;
        else gameOverText.text = "Game Over";
    }

    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(1.50f);
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(2.1f);
        Time.timeScale = 0f;
    }

    IEnumerator removeBp()
    {
        yield return new WaitForSeconds(1f);
        blackPanel.SetActive(false);
    }

    IEnumerator openBp(int sceneIndex)
    {
        blackPanel.SetActive(true);
        Time.timeScale = 1f;
        blackPanelAnim.SetInteger("blackPanel",-1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }

    public void Win()
    {
        menuButon.SetActive(false);
        player.GetComponent<pacmanControl>().speed=0;
        StartCoroutine(gameWin());

        if (food.score != 0)
            gameOverText.text = "You  Won\n\nScore:" + food.score;
        else gameOverText.text = "You Won";

        gameOverPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Next Level";
        win = true;
    }

    IEnumerator gameWin()
    {
        GetComponent<disabledEnemy>().enemiesPassive();
        yield return new WaitForSeconds(1.50f);
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(2.1f);
        Time.timeScale = 0f;
    }

    
}