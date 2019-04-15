using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayer : MonoBehaviour
{
    private SpriteRenderer render;
    public CameraMove cameraMove;
    private GameObject collisionParent;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponentInParent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionParent = collision.gameObject.transform.parent.gameObject;
        if (collisionParent.layer > 8 && collisionParent.layer < 12)
            render.sortingOrder = 10;
        else
            render.sortingOrder = 3;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collisionParent.layer == 9)
            render.sortingOrder = 3;
    }
}
