using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArrow : MonoBehaviour
{
    private Rigidbody2D rig;
    private Transform tr;
    private Vector3 mousePos;
    private Vector3 direction;
    private float angle;
    private Vector3 prevPos;
    public Bow_launcher luncher;
    private bool colliderCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        prevPos = tr.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaPos = tr.position - prevPos;
        float deltaAngle = Mathf.Rad2Deg * Mathf.Atan2(deltaPos.y, deltaPos.x);
        tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.Euler(new Vector3(0, 0, deltaAngle)), 10 * Time.deltaTime);
        prevPos = tr.position;

        /*if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePos - tr.position).normalized;
            angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
            tr.rotation = Quaternion.AngleAxis(angle, transform.forward);
            rig.velocity = tr.rotation * new Vector2(10, 0);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!colliderCheck)
        {
            if (collision.gameObject.tag == "10 Point")
            {
                luncher.score += 10;
            }
            else if (collision.gameObject.tag == "5 Point")
            {
                luncher.score += 5;
            }
            else if (collision.gameObject.tag == "1 Point")
            {
                luncher.score += 1;
            }
            colliderCheck = true;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!colliderCheck)
        {
            if (collision.gameObject.tag == "10 Point")
            {
                luncher.score += 10;
            }
            else if (collision.gameObject.tag == "5 Point")
            {
                luncher.score += 5;
            }
            else if (collision.gameObject.tag == "1 Point")
            {
                luncher.score += 1;
            }
            colliderCheck = true;
        }
        Destroy(this.gameObject);
    }
}
