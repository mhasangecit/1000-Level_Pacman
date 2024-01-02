using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class disabledEnemy : MonoBehaviour
{

    public Transform mainEnemyObject;
    public static List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        enemiesToArray();
    }

    void Update()
    {
    }

    void enemiesToArray()
    {
        for (int i = 0; i < mainEnemyObject.childCount; i++)
        {
            enemies.Add(mainEnemyObject.GetChild(i).gameObject);
        }
    }

    public void enemiesPassive()
    {
        enemies.Clear();
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<npcFollow>().enabled && enemy.GetComponent<NavMeshAgent>().enabled)
            {
                enemy.GetComponent<npcFollow>().enabled = false;
                enemy.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
        /* chatgpt hata cozum onerisi:
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                npcFollow followComponent = enemy.GetComponent<npcFollow>();
                if (followComponent != null && followComponent.enabled)
                {
                    followComponent.enabled = false;
                }
                NavMeshAgent navAgentComponent = enemy.GetComponent<NavMeshAgent>();
                if (navAgentComponent != null && navAgentComponent.enabled)
                {
                    navAgentComponent.enabled = false;
                }
            }
        }*/
    }

    public int nextLevel(int sceneIndex)
    {
        food.score = 0;
        food.eatenFood = 0;
        return (sceneIndex == 1) ? 2 : 1;
    }
}
