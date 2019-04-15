using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_player : MonoBehaviour
{
    private Transform tr;
    private Vector3 prevPos;
    private Rigidbody2D rig;
    private float angle;
    

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        prevPos = tr.position;
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rig.velocity = Quaternion.Euler(0, 0, 0) * Vector2.up * 4;
            tr.eulerAngles = new Vector3(0, 0, -30);
        }
        angle = tr.eulerAngles.z - Time.deltaTime * 100;
        tr.eulerAngles = new Vector3(0, 0, angle);
    }
}
