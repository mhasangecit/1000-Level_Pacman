using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class npcDead : MonoBehaviour
{
    Vector2 startingPos;
    Animator animator;
    public bool navMeshState=true;
    [SerializeField] GameObject canvas;
    button buttonOption;

    void Start()
    {
        startingPos = transform.position;
        animator = GetComponent<Animator>();
        buttonOption = canvas.GetComponent<button>();
    }

    void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!animator.GetBool("Run") && collision.CompareTag("Player"))
        {
            buttonOption.GameOver();
        }
        else if (animator.GetBool("Run") && collision.CompareTag("Player"))
        {
            StartCoroutine(deadAnim());
        } 
    }
    
    IEnumerator deadAnim()
    {
        NavMeshAgent nvm=GetComponent<NavMeshAgent>();
        npcAnimControl npa = GetComponent<npcAnimControl>();
        navMeshState = false;
        nvm.enabled = false;
        npa.enabled = false;
        animator.SetTrigger("dead");
        yield return new WaitForSeconds(3f);
        transform.position = startingPos;
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("born");
        yield return new WaitForSeconds(npcFollow.ghostTime);
        animator.SetBool("Run", false);
        nvm.enabled = true;
        npa.enabled = true;
        navMeshState = true;
    }
}
