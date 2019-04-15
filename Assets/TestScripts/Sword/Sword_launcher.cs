using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_launcher : MonoBehaviour
{
    private float[] posX = new float[36];
    private float[] posY = new float[36];
    public GameObject bullet;
    public int count = 0;
    private bool check = false;
    public BoxCollider2D[] wallCollider;
    public GameObject Player;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 36; i++)
        {
            posX[i] = 10 * Mathf.Cos(i * 10);
            posY[i] = 10 * Mathf.Sin(i * 10);
        }
        StartCoroutine("Shot");
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            StartCoroutine("Shot");
            check = false;
        }
    }

    IEnumerator Shot()
    {
        while (count < 10)
        {
            bullet.GetComponent<TestBullet>().wallCollider = wallCollider;
            bullet.GetComponent<TestBullet>().controller = GetComponent<Sword_launcher>();
            int posNum = Random.Range(0, 36);
            bullet.transform.position = new Vector3(posX[posNum], posY[posNum], 0);
            Vector3 vector = bullet.transform.position - Player.transform.position;
            float angle = 180 + Mathf.Rad2Deg * Mathf.Atan2(vector.y, vector.x);
            bullet.transform.eulerAngles = new Vector3(0, 0, angle);
            bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
            GameObject obj = Instantiate(bullet);
            obj.GetComponent<Rigidbody2D>().velocity = obj.transform.rotation * new Vector2(bulletSpeed, 0);
            count++;
            yield return new WaitForSeconds(1.0f);
        }

        check = true;
    }
}
