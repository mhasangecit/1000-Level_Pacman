using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pacmanControl : MonoBehaviour
{
    public float speed = 5f;
    bool up, down, right, left;

    private Vector2 velocity = Vector2.zero;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        yon_ata(false, false, true, false);
    }


    private void Update()
    {
        keyControl();
        move();

        velocity = Vector2.ClampMagnitude(velocity, speed);

        transform.Translate(velocity * Time.deltaTime);
    }

    void keyControl()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            yon_ata(true, false, false, false);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            yon_ata(false, true, false, false);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            yon_ata(false, false, true, false);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            yon_ata(false, false, false, true);
        }
    }

    public void yon_ata(bool y1, bool y2, bool y3, bool y4)
    {
        up = y1;
        down = y2;
        right = y3;
        left = y4;
    }

    void move()
    {
        if (up)
        {
            velocity.y = speed;
            anim.SetInteger("direction", 2);
        }
        else if (down)
        {
            velocity.y = -speed;
            anim.SetInteger("direction", 3);
        }
        else if (right)
        {
            velocity.x = speed;
            anim.SetInteger("direction", 0);
        }
        else if (left)
        {
            velocity.x = -speed;
            anim.SetInteger("direction", 1);
        }
    }
}

