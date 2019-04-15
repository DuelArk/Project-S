using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow_target : MonoBehaviour
{
    private Transform tr;
    private float moveSpeed = 5;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        direction = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        if(tr.position.x > 8)
        {
            direction = -Vector3.right;
            SpeedChange();
        }else if (tr.position.x < -4)
        {
            direction = Vector3.right;
            SpeedChange();
        }
        tr.Translate(direction * Time.deltaTime * moveSpeed);
    }

    private void SpeedChange()
    {
        int rand = Random.Range(3, 10);
        moveSpeed = rand;
    }
}
