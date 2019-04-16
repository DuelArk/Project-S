using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_ball : MonoBehaviour
{
    private Rigidbody2D rig;
    private float result;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        RandomPos();
        transform.position = new Vector3(result, 4.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        rig.mass += Time.deltaTime * 0.1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 21)
            rig.mass -= 0.1f;
    }

    private void RandomPos()
    {
        int rand = Random.Range(-9, 9);
        int rand2 = Random.Range(1, 5);
        result = rand + rand2 * 0.2f;
    }
}
