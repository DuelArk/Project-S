using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    private Camera camera;
    private float cameraSpeed;
    private float halfHeight;
    private float halfWidth;
    public Collider2D boundBox;
    public Vector3 maxbounds;
    public Vector3 minbounds;
    new Transform transform;
	
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        halfHeight = camera.orthographicSize;
        halfWidth = halfHeight * camera.aspect;
        transform = GetComponent<Transform>();
        maxbounds = Vector3.zero;
        minbounds = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        /*Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed * Time.deltaTime);*/
        float clampedX = Mathf.Clamp(transform.position.x, minbounds.x + halfWidth, maxbounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minbounds.y + halfHeight, maxbounds.y - halfHeight);
        if (maxbounds.x - halfWidth < minbounds.x + halfWidth)
            clampedX = (maxbounds.x + minbounds.x) / 2;
        if (maxbounds.y - halfHeight < minbounds.y + halfHeight)
            clampedY = (maxbounds.y + minbounds.y) / 2;
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
