using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder_launcher : MonoBehaviour
{
    public GameObject bullet;
    private GameObject bulletObj;
    //private Thunder_bullet bulletSetting;
    private Vector3 direction;
    private Vector3 mousePos;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        //bulletSetting = bullet.GetComponent<Thunder_bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = mousePos.normalized;
            angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
            if(bulletObj == null)
            {
                bullet.transform.eulerAngles = new Vector3(0, 0, angle);
                bulletObj = Instantiate(bullet);
            }
            else
            {
                bulletObj.transform.eulerAngles = new Vector3(0, 0, angle);
                bulletObj.transform.position = Vector3.zero;
                bulletObj.SetActive(true);
            }
            bulletObj.GetComponent<Rigidbody2D>().velocity = bulletObj.transform.rotation * new Vector2(10, 0);
        }
    }
}
