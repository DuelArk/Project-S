using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_controller : MonoBehaviour
{
    [SerializeField] private GameObject[] target;
    [SerializeField] private float onTime;
    [SerializeField] private float targetUpSpeed;
    [SerializeField] private float targetUpNum;
    private int count = 0;
    public float score = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("TargetOn");
    }

    IEnumerator TargetOn()
    {
        new WaitForSeconds(targetUpSpeed);
        while (count < targetUpNum)
        {
            int rand = Random.Range(0, 6);
            if (!target[rand].GetComponent<Fire_target>().on)
            {
                target[rand].GetComponent<Fire_target>().count = onTime;
                target[rand].GetComponent<Fire_target>().TargetOn();
                count++;
                yield return new WaitForSeconds(targetUpSpeed);
            }
        }
    }
}
