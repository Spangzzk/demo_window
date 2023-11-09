using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class MapGenerate : MonoBehaviour
{
    [Header("外部属性")]
    public Transform playerTransform;
    public GameObject LeftMap;
    public GameObject RightMap;
    public GameObject[] Obstacles;
    [Header("控制属性")]
    public float Y_Position = 20.5f;
    public float controlObstacle = 0;

    // Start is called before the first frame update
    void Start()
    {
        map_Init();
    }

    // Update is called once per frame
    void Update()
    {
        generateMap();
        generateObstacle();
    }

    void generateMap()
    {
        if(playerTransform.position.y + 20 > Y_Position)
        {
            for(float i = 0f; i < 20; i++)
            {
                Vector3 p_Left = new Vector3(-7.5f, Y_Position, 0);
                Vector3 p_Right = new Vector3(7.5f, Y_Position, 0);

                Instantiate(LeftMap, p_Left, Quaternion.Euler(0, 0, 90));
                Instantiate(RightMap, p_Right, Quaternion.Euler(0, 0, -90));
                Y_Position++;
            }
        }
    }

    void map_Init()
    {
        
        for (float i = -9.5f; i < 20.5f; i+=1)
        {
            Vector3 p_Left = new Vector3(-7.5f, i, 0);
            Vector3 p_Right = new Vector3(7.5f, i, 0);
     
            Instantiate(LeftMap, p_Left, Quaternion.Euler(0,0,90));
            Instantiate(RightMap, p_Right, Quaternion.Euler(0, 0, -90));
        }
    }

    void generateObstacle()
    {
        controlObstacle += Time.deltaTime;
        if (controlObstacle > 3)
        {
            Vector3 a = new Vector3(Random.Range(-6.5f,6.5f), playerTransform.position.y + 10, 0);
            Obstacles[2].active = true;
            Instantiate(Obstacles[2], a, Quaternion.identity);
            controlObstacle = 0;
        }
    }

    
}
