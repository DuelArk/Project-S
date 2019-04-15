using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_point : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
    public float speed;

    public void Shoot(int patternNum)
    {
        switch (patternNum)
        {
            case 0: StartCoroutine(FiveRepeat());
                break;
            case 1: StartCoroutine(Circle());
                break;
        }
    }

    IEnumerator FiveRepeat()
    {
        for(int i = 0; i < 5; i++)
        {
            int rand = Random.Range(-15, 15);
            bullet.transform.eulerAngles = new Vector3(0, 0, rand);
            bullet.transform.position = transform.position;
            GameObject obj = Instantiate(bullet);
            bullet.GetComponent<Ground_bullet>().speed = speed;
            Vector3 direction = player.transform.position - transform.position;
            direction = direction.normalized;
            obj.GetComponent<Rigidbody2D>().velocity = obj.transform.rotation * direction * speed;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.SetActive(false);
    }

    IEnumerator Circle()
    {
        for(int i = 0; i < 8; i++)
        {
            bullet.transform.eulerAngles = new Vector3(0, 0, 45 * i);
            bullet.transform.position = transform.position;
            bullet.GetComponent<Ground_bullet>().speed = speed;
            GameObject obj = Instantiate(bullet);
            obj.GetComponent<Rigidbody2D>().velocity = obj.transform.rotation * Vector2.right * speed;
        }
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
