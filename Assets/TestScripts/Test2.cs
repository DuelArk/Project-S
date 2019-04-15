using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    private Vector3 location;
    private Vector3 direction;
    private Transform tr;
    private Rigidbody2D rig;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (location - tr.position).normalized;
            rig.AddForce(direction * speed);
        }
    }
}
