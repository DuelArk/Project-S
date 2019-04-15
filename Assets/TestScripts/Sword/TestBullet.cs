using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public Sword_launcher controller;
    private CircleCollider2D collider;
    public BoxCollider2D[] wallCollider;
    private Transform tr;
    private Rigidbody2D rig;
    private bool atkCheck = false;

    private void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        for(int i = 0; i < 4; i++)
        {
            Physics2D.IgnoreCollision(collider, wallCollider[i], true);
        }
        tr = GetComponent<Transform>();
        rig = GetComponent<Rigidbody2D>();
        StartCoroutine("ColliderCheck");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            delete();
        }
    }

    IEnumerator ColliderCheck()
    {
        while (true)
        {
            if (tr.position.x < 2.25f && tr.position.x > -2.25f && tr.position.y < 2.25f && tr.position.y > -2.25f && !atkCheck)
            {
                for(int i = 0; i < 4; i++)
                {
                    Physics2D.IgnoreCollision(collider, wallCollider[i], false);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rig.velocity = -rig.velocity;
        tr.eulerAngles = -tr.eulerAngles;
        atkCheck = true;
        for (int i = 0; i < 4; i++)
        {
            Physics2D.IgnoreCollision(collider, wallCollider[i], true);
        }

        Invoke("delete", 3);
    }

    private void delete()
    {
        controller.count--;
        Destroy(this.gameObject);
    }
}
