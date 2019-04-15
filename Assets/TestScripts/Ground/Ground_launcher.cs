using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_launcher : MonoBehaviour
{
    public GameObject[] point;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        point = new GameObject[transform.childCount];
        for(int i = 0; i < point.Length; i++)
        {
            point[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            speed = 5f;
            int rand = Random.Range(0, point.Length);
            ShootPoint(point[rand], 0);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            speed = 2.5f;
            StartCoroutine(Pattern1());
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            speed = 2.5f;
            StartCoroutine(Pattern2());
        }
    }

    IEnumerator Pattern1()
    {
        for(int i = 0; i < 5; i++)
        {
            ShootPoint(point[i], 1);
            ShootPoint(point[i + 5], 1);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator Pattern2()
    {
        for(int i = 0; i < 5; i++)
        {
            if (i == 0)
            {
                ShootPoint(point[i], 1);
                ShootPoint(point[i + 4], 1);
            }
            else if (i == 4)
            {
                ShootPoint(point[i + 1], 1);
                ShootPoint(point[i + 5], 1);
            }
            else
            {
                ShootPoint(point[i + 9], 1);
                ShootPoint(point[i + 12], 1);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void ShootPoint(GameObject point, int shootPattern)
    {
        point.SetActive(true);
        point.GetComponent<Ground_point>().speed = speed;
        point.GetComponent<Ground_point>().Shoot(shootPattern);
    }
}
