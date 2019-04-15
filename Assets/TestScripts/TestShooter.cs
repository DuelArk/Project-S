using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShooter : MonoBehaviour
{
    public GameObject bullet;
    private Transform tr;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tr.position = new Vector3(mousePos.x, mousePos.y, 0);
        if (Input.GetMouseButtonDown(0))
        {
            bullet.transform.position = tr.position;
            Instantiate(bullet);
        }
    }
}
