using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_bullet : MonoBehaviour
{
    public Water_launcher launcher;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        launcher.coolTime();
        Destroy(this.gameObject);
    }
}
