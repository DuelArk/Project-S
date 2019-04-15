using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_player : MonoBehaviour
{
    private Transform tr;
    public GameObject attackCollider;
    public float moveSpeed;
    public bool front, back, left, right;
    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        front = true;
        back = false;
        left = false;
        right = false;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ani.GetBool("attack"))
        {
            Move();

            if (Input.GetKeyDown(KeyCode.Z))
                StartCoroutine("Attack");
        }
    }

    private void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if(direction.x != 0 || direction.y != 0)
        {
            ani.SetBool("moving", true);
        }
        else
        {
            ani.SetBool("moving", false);
        }

        if(Input.anyKeyDown)
        {
            Direction();
        }
        //GetComponent<Rigidbody2D>().MovePosition(tr.position + direction * Time.deltaTime * moveSpeed);
        tr.Translate(direction * Time.deltaTime * moveSpeed);
        tr.position = new Vector3(Mathf.Clamp(tr.position.x, -2.05f, 2.05f), Mathf.Clamp(tr.position.y, -1.6f, 2.5f), 0);
        
    }

    private void Direction()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            front = false;
            back = true;
            left = false;
            right = false;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            front = true;
            back = false;
            left = false;
            right = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            front = false;
            back = false;
            left = true;
            right = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            front = false;
            back = false;
            left = false;
            right = true;
        }

        if (front)
        {
            ani.SetFloat("PosX", 0);
            ani.SetFloat("PosY", -1);
            tr.localScale = Vector3.one;
            attackCollider.transform.eulerAngles = Vector3.forward * 180;
        }else if (back)
        {
            ani.SetFloat("PosX", 0);
            ani.SetFloat("PosY", 1);
            tr.localScale = Vector3.one;
            attackCollider.transform.eulerAngles = Vector3.zero;
        }else if (left)
        {
            ani.SetFloat("PosX", -1);
            ani.SetFloat("PosY", 0);
            tr.localScale = Vector3.one - Vector3.right * 2;
            attackCollider.transform.eulerAngles = Vector3.forward * 90;
        }
        else if (right)
        {
            ani.SetFloat("PosX", 1);
            ani.SetFloat("PosY", 0);
            tr.localScale = Vector3.one;
            attackCollider.transform.eulerAngles = Vector3.forward * -90;
        }
    }

    IEnumerator Attack()
    {
        ani.SetBool("attack", true);
        yield return new WaitForSeconds(1f);
    }
}
