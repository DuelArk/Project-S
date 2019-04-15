using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_bullet : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(6f);
        Destroy(this.gameObject);
    }
}
