using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_player : MonoBehaviour
{
    public float targetDirectionAngle;
    private Transform tr;
    private Animator ani;
    public GameObject[] bullet;
    private int bulletNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        ani = GetComponent<Animator>();
    }

    public void FireShoot()
    {
        if (!ani.GetBool("shoot"))
        {
            StartCoroutine("Shooting");
        }
    }

    IEnumerator Shooting()
    {
        ani.SetBool("shoot", true);
        yield return new WaitForSeconds(0.35f);
        bullet[bulletNum].transform.position = tr.position;
        bullet[bulletNum].transform.eulerAngles = new Vector3(0, 0, targetDirectionAngle);
        bullet[bulletNum].SetActive(true);
        bullet[bulletNum].GetComponent<Rigidbody2D>().velocity = bullet[bulletNum].transform.rotation * Vector2.right * 10;
        bulletNum++;
        if (bulletNum == 6)
            bulletNum = 0;
    }


}
