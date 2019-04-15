using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_player : MonoBehaviour
{
    private Transform tr;
    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * 10);
        tr.position = new Vector3(Mathf.Clamp(tr.position.x, -4.5f, 4.5f), Mathf.Clamp(tr.position.y, -4.5f, 4.5f), 0);
    }
}
