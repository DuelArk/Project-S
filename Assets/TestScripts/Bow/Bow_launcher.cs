using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow_launcher : MonoBehaviour
{
    private Transform tr;
    public Transform directionTr;
    private Vector3 mousePos;
    private Vector3 vector;
    private float directionAngle;
    public GameObject arrow;
    public float score;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ArrowDir();
        if (Input.GetMouseButtonDown(0))
        {
            arrow.GetComponent<TestArrow>().luncher = GetComponent<Bow_launcher>();
            arrow.transform.position = directionTr.position;
            arrow.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, directionAngle + 90);
            GameObject obj = Instantiate(arrow);
            obj.GetComponent<Rigidbody2D>().velocity = obj.transform.rotation * new Vector2(10, 0);

        }
    }

    private void ArrowDir()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vector = directionTr.position - mousePos;
        directionAngle = Mathf.Rad2Deg * Mathf.Atan2(vector.y, vector.x) + 90;
        if (directionAngle > 0 && directionAngle < 90)
            directionAngle = 0;
        else if (directionAngle > 90 && directionAngle < 270)
            directionAngle = -90;
        directionTr.eulerAngles = new Vector3(0, 0, directionAngle);
    }
}
