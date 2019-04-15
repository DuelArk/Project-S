using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_launcher : MonoBehaviour
{
    public GameObject bullet;
    private Water_bullet bulletSetting;
    public bool shootCheck = false;
    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        bulletSetting = bullet.GetComponent<Water_bullet>();
        bulletSetting.launcher = GetComponent<Water_launcher>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !shootCheck)
        {
            shootCheck = true;
            bullet.transform.position = transform.position;
            GameObject obj = Instantiate(bullet);
            obj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
        }

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            ani.SetBool("moving", true);
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * 10);
        }
        else
        {
            ani.SetBool("moving", false);
        }
    }

    public void coolTime()
    {
        StartCoroutine(coolTimeCoroutine());
    }

    IEnumerator coolTimeCoroutine()
    {
        yield return new WaitForSeconds(1f);
        shootCheck = false;
    }
}
