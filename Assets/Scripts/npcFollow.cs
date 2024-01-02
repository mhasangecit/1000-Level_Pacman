using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcFollow : MonoBehaviour
{
    static float startGhostTime;
    NavMeshAgent nm;
    Animator anim;
    GameObject player;
    Vector3 target;
    public float prisonTime;
    bool inPrison = true;
    public bool isGhost;
    public Transform[] runPoints;
    public static float ghostTime = 5f;
    bool navMeshState;

    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player =GameObject.FindWithTag("Player");
        nm.updateRotation = false;
        nm.updateUpAxis = false;
        target = new Vector3(transform.position.x, transform.position.y, 0f);
        startGhostTime = ghostTime;
        StartCoroutine(waitInPrison());
    }

    void Update()
    {
        navMeshState = GetComponent<npcDead>().navMeshState;

        if (!inPrison && !isGhost && navMeshState)
        {
            playerFollow();
        }
        else if (isGhost && navMeshState)
        {
            StartCoroutine(ghostManage());
        }
    }

    void playerFollow()
    {
        nm.SetDestination(player.transform.position);
    }


    IEnumerator waitInPrison()
    {
        inPrison = true;
        yield return new WaitForSeconds(prisonTime);
        inPrison = false;
    }

    void runGhost()
    {
        anim.SetBool("Run", true);

        int targetIndex = 0;
        float maxRp = 0;

        for (int i = 0; i < runPoints.Length; i++)
        {
            float temp = Vector2.Distance(player.transform.position, runPoints[i].position);

            if (temp > maxRp)
            {
                maxRp = temp;
                targetIndex = i;
            }
        }

        nm.SetDestination(runPoints[targetIndex].position);
    }

    void returnToEnemyCase()
    { 
        anim.SetBool("Run", false);
        isGhost = false;
    }

    IEnumerator ghostManage()
    {
        runGhost();
        float elapsedTime = 0f;
        while (elapsedTime < ghostTime)
        {
            yield return null; 
            elapsedTime += Time.deltaTime;
        }
        returnToEnemyCase();
    }
}