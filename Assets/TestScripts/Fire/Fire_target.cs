using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_target : MonoBehaviour
{
    [HideInInspector] public float count;
    public KeyCode keyCode;
    public bool on = false;
    public Fire_controller controller;
    private Animator ani;
    public Fire_player player;
    private Transform tr;
    private float angle;
    private Vector3 direction;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        ani = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        direction = tr.position - player.gameObject.transform.position;
        angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            player.targetDirectionAngle = angle;
            player.FireShoot();
        }
    }

    public void TargetOn()
    {
        StartCoroutine("TargetCount");
    }

    IEnumerator TargetCount()
    {
        on = true;
        ani.SetBool("on", true);
        yield return new WaitForSeconds(count);
        on = false;
        ani.SetBool("on", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (on)
        {
            ani.SetBool("on", false);
            controller.score++;
            on = false;
        }
        collision.gameObject.SetActive(false);
    }
}
