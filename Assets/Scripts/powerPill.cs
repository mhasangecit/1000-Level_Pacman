using UnityEngine;

public class powerPill : MonoBehaviour
{
    //GameObject[] enemies;

    void Start()
    {
        //enemies = GameObject.FindGameObjectsWithTag("enemy");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //gameObject.GetComponent<SpriteRenderer>().sprite = null;
            foreach (GameObject enemy in disabledEnemy.enemies)
            {
                enemy.GetComponent<npcFollow>().isGhost = true;
            }

            if (npcFollow.ghostTime < 10)
                npcFollow.ghostTime += 5;
            Destroy(gameObject);
        }
    }
}
