using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
    public float speedDoublingAmount=1.15f;
    public static int level=1;
    GameObject player;
    public static List<GameObject> pills = new List<GameObject>();
    Transform mainPillsObject;
    int enemyIndex=0;
    public static bool createdLManagerObject=false;

    public Text levelText;
    GameObject timeManager;

    private void Awake()
    {
        print("awake");
        if (!createdLManagerObject)
        {
            DontDestroyOnLoad(this.gameObject);
            createdLManagerObject = true;
        }
        //else
        //{
        //    Destroy(this.gameObject);
        //}
    }

    void Start()
    {
        print("start");
        //levelText = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        //player = transform.GetChild(1).gameObject;
        //mainPillsObject = transform.GetChild(2);

        levelText = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        player = GameObject.Find("pacman_0");
        player.GetComponent<pacmanControl>().speed /=2;
        mainPillsObject = GameObject.FindWithTag("powerPill").transform;

        pillsToArray();
    }

    public void variableSettings(int scenIndex)
    {
        StartCoroutine(variableSetting(scenIndex));
    }

    IEnumerator variableSetting(int sceneIndex)
    {
        SceneManager.LoadScene(nextLevel(sceneIndex));
        yield return new WaitForSeconds(1f);

        level++;
        levelText.text = "Level " + level;

        if (level % 3 == 0)
        {
            pillSettings();
            timeSettings();
        }

        if (level % 4 == 0)
        {
            npcSettings();
            playerSettings();
        }

        if (level % 5 == 0)
            increaseNumberOfEnemies();
    }

    void npcSettings()
    {
        foreach (GameObject enemy in disabledEnemy.enemies)
        {
            enemy.GetComponent<NavMeshAgent>().speed *= speedDoublingAmount;
        }
    }

    void increaseNumberOfEnemies()
    {
        GameObject newEnemy=Instantiate(disabledEnemy.enemies[enemyIndex], disabledEnemy.enemies[enemyIndex].transform);
        npcFollow newEnScript= newEnemy.GetComponent<npcFollow>();
        newEnScript.prisonTime = 0;
        newEnScript.prisonTime = disabledEnemy.enemies[enemyIndex - 1].GetComponent<npcFollow>().prisonTime + 3f;
        disabledEnemy.enemies.Add(newEnemy);
        enemyIndex++;

        if (enemyIndex >= 4)
            enemyIndex = 0;
    }

    void playerSettings()
    {
        player.GetComponent<pacmanControl>().speed *= speedDoublingAmount;
        player.GetComponent<Animator>().SetBool("death", false);
    }

    void timeSettings()
    {
        timeManager = GameObject.FindWithTag("timeManager");

        if(timeManager!=null)
            timeManager.GetComponent<timeManagment>().time += 2.5f;
    }

    void pillSettings()
    {
        Destroy(pills[0]);
        pills.RemoveAt(0);
    }

    void pillsToArray()
    {
        for (int i=0; i < mainPillsObject.childCount; i++)
        {
            pills.Add(mainPillsObject.GetChild(i).gameObject);
        }
    }

    public int nextLevel(int sceneIndex)
    {
        food.score = 0;
        food.eatenFood = 0;
        return (sceneIndex == 1) ? 2 : 1;
    }

}
