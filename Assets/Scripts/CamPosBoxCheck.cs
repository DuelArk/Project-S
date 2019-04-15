using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosBoxCheck : MonoBehaviour
{
    public CameraMove cameraMove;
    private BoxCollider2D boundBox;
    // Start is called before the first frame update
    void Start()
    {
        cameraMove = Camera.main.GetComponent<CameraMove>();
        boundBox = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            cameraMove.boundBox = boundBox;
            cameraMove.maxbounds = boundBox.bounds.max;
            cameraMove.minbounds = boundBox.bounds.min;
        }
    }
}
