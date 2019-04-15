using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_wallmove : MonoBehaviour
{
    private Transform tr;
    public Wind_create createSetting;
    public bool last = false;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        if (tr.position.x == 9)
        {
            last = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(Vector3.left * Time.deltaTime * 5);

        if(tr.position.x < -10)
        {
            this.gameObject.SetActive(false);
        }

        if(tr.position.x < 9.6f && last)
        {
            last = false;
            createSetting.NextCreate(tr.position.x);
        }
    }
}
