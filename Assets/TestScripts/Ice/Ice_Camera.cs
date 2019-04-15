using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Camera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tr.transform.position = player.transform.position - Vector3.forward * 10;
    }
}
