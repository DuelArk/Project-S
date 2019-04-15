using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_player : MonoBehaviour
{
    private Rigidbody2D rig;
    private Transform tr;
    [SerializeField] private Transform bgTransform;
    private Vector3 bgSize;
    private Vector3 playerSize;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        bgSize = bgTransform.localScale / 2;
        playerSize = transform.localScale / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) //가속
        {
            rig.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * 5);
        }
        else //마찰로 감속
        {
            rig.AddForce(-rig.velocity * 0.5f);
        }
        tr.position = new Vector3(Mathf.Clamp(tr.position.x, -bgSize.x + playerSize.x, bgSize.x - playerSize.x), Mathf.Clamp(tr.position.y, -bgSize.y + playerSize.y, bgSize.y - playerSize.y), 0);

        if (Mathf.Abs(tr.position.x) == bgSize.x - playerSize.x || Mathf.Abs(tr.position.y) == bgSize.y - playerSize.y)
        {
            rig.velocity = -rig.velocity * 0.5f;
        }
    }
}
