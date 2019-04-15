using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_create : MonoBehaviour
{
    public GameObject wall;
    private float prevY;
    private GameObject[] wallObj;
    private int lastWall = 0;

    // Start is called before the first frame update
    void Start()
    {
        wallObj = new GameObject[34];
        prevY = 0;
        wall.GetComponent<Wind_wallmove>().createSetting = GetComponent<Wind_create>();
        for (int i = 0; i < 16; i++)
        {
            float rand = 0;
            float distance = 2;
            while(distance > 1.5f)
            {
                rand = Random.Range(0, 11);
                rand = (rand - 5) * 0.4f;
                distance = Mathf.Abs(prevY - rand);
            }
            prevY = rand;
            wall.transform.position = new Vector3(i * 0.6f, rand, 0);
            wallObj[lastWall] = Instantiate(wall);
            lastWall++;
        }
    }

    public void NextCreate(float posX)
    {
        float rand = 0;
        float distance = 2;
        while (distance > 1.5f)
        {
            rand = Random.Range(0, 11);
            rand = (rand - 5) * 0.4f;
            distance = Mathf.Abs(prevY - rand);
        }
        prevY = rand;
        if(wallObj[lastWall] == null)
        {
            wall.transform.position = new Vector3(posX + 0.6f, rand, 0);
            wallObj[lastWall] = Instantiate(wall);
            wallObj[lastWall].GetComponent<Wind_wallmove>().last = true;
            lastWall++;
            if (lastWall == 34)
            {
                lastWall = 0;
            }
        }
        else
        {
            wallObj[lastWall].transform.position = new Vector3(posX + 0.6f, rand, 0);
            wallObj[lastWall].SetActive(true);
            wallObj[lastWall].GetComponent<Wind_wallmove>().last = true;
            lastWall++;
            if (lastWall == 34)
            {
                lastWall = 0;
            }
        }
    }
}
