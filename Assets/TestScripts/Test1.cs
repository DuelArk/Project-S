using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    private float angle;
    private bool move;
    private Rigidbody2D rig;
    private Vector3 location;
    private Vector3 direction;
    private Vector3 mousePos;
    private Vector2 target;
    public float speed;
    private Transform tr;
    private Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        move = false;
        rig = GetComponent<Rigidbody2D>();
        prevPos = tr.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!move)
        {
            if (Input.GetMouseButton(0))
            {
                move = true;
                location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                location = new Vector3(location.x, location.y, 0);
                direction = (location - tr.position).normalized;
                rig.AddForce(direction * speed);
            }
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.x = mousePos.x - tr.position.x;
            target.y = mousePos.y - tr.position.y;
            angle = -1 * Mathf.Rad2Deg * Mathf.Atan2(target.x, target.y) + 90; // or Mathf.Rad2Deg*Mathf.Atan2(taget.y, target.x)
            tr.eulerAngles = new Vector3(0, 0, angle);
        }
        if (move)
        {
            Vector3 deltaPos = tr.position - prevPos;
            float deltaAngle = Mathf.Rad2Deg * Mathf.Atan2(deltaPos.y, deltaPos.x);
            if (deltaAngle != 0)
            {
                tr.eulerAngles = new Vector3(0, 0, deltaAngle);
                prevPos = tr.position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 reflect = Vector3.Reflect(direction, collision.contacts[0].normal);
        float rot = 90 - Mathf.Rad2Deg * Mathf.Atan2(reflect.x, reflect.y);
        tr.eulerAngles = new Vector3(0, 0, rot);
        if (rot == 0 && reflect.x < 0)
        {
            rot = 180;
        }
        direction = reflect;
    }
}
