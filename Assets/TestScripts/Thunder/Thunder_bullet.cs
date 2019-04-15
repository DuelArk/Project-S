using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder_bullet : MonoBehaviour
{
    private Transform tr;
    private Vector3 prevPos;
    private Vector3 direction;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        prevPos = tr.position;
    }

    // Update is called once per frame
    void Update()
    {
        direction = tr.position - prevPos;
        angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        if (angle != 0)
        {
            tr.eulerAngles = new Vector3(0, 0, angle);
        }
        prevPos = tr.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (angle == 0 || angle == 180)
        {
            tr.eulerAngles = new Vector3(0, 0, tr.eulerAngles.z + 180);
        }
    }
}
