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
    public GameObject[] Bloods;
    [Header("控制属性")]
    public float Y_Position = 20.5f;
    public float controlObstacle = 0;
    public float controlBlood = 0;
    public float controlWall = 0;
    public float controlStart = 0;

    // Start is called before the first frame update
    void Start()
    {
        map_Init();
    }

    // Update is called once per frame
    void Update()
    {
        generateMap();
        startGenerate();
    }

    void startGenerate()
    {
        controlStart += Time.deltaTime;
        if(controlStart > 3 )
        {
            generateObstacle();
            generateBlood();
            generateWall();
        }
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
        for (int i = 0; i < 7; i++) Obstacles[i].active = false;
        for (int i = 0; i < 3; i++) Bloods[i].active = false;

        for (float i = -9.5f; i < 20.5f; i+=1)
        {
            Vector3 p_Left = new Vector3(-7.5f, i, 0);
            Vector3 p_Right = new Vector3(7.5f, i, 0);
     
            Instantiate(LeftMap, p_Left, Quaternion.Euler(0,0,90));
            Instantiate(RightMap, p_Right, Quaternion.Euler(0, 0, -90));
        }
    }

    int GetIndex(int x)
    {
        for(int i = 0; i < 6; i++)
        {
            x %= 6;
            if (Obstacles[x].active)
            {
                return x;
            }
        }

        for (int i = 0; i < 6; i++)
        {
            Obstacles[i].active = true;
        }

        return x;
    }

    void generateObstacle()
    {
        controlObstacle += Time.deltaTime;
        if (controlObstacle > 5)
        {
            Vector3 a = new Vector3(Random.Range(-6.5f,6.5f), playerTransform.position.y + 10, 0);
            Vector3 b = new Vector3(Random.Range(-6.5f, 6.5f), playerTransform.position.y + 6, 0);
            int indexa = GetIndex(Random.Range(0, 6));
            int indexb = GetIndex(Random.Range(0, 6));
            Obstacles[indexa].active = true;
            Instantiate(Obstacles[indexa], a, Quaternion.identity);
            Obstacles[indexb].active = true;
            Instantiate(Obstacles[indexb], b, Quaternion.identity);
            controlObstacle = 0;
        }
    }

    void generateBlood()
    {
        controlBlood += Time.deltaTime;
        if (controlBlood > 8)
        {
            Vector3 a = new Vector3(Random.Range(-6.5f, 6.5f), playerTransform.position.y + 10, 0);
            int indexa = Random.Range(0, 3);
            Bloods[indexa].active = true;
            Instantiate(Bloods[indexa], a, Quaternion.identity);
            controlBlood= 0;
        }
    }
    
    void generateWall()
    {
        controlWall += Time.deltaTime;
        if(controlWall > 10)
        {
            int t = Random.Range(0, 4);
            if(t % 2 == 0)
            {
                float x = Random.Range(-6.5f, 5.5f);
                Vector3 a = new Vector3(x, playerTransform.position.y + 10, 0);
                Vector3 b = new Vector3(x+1, playerTransform.position.y + 10, 0);
                Obstacles[6].active = true;
                Instantiate(Obstacles[6], a, Quaternion.identity);
                Instantiate(Obstacles[6], b, Quaternion.identity);
            }
            else
            {
                float x = Random.Range(-6.5f, 5.5f);
                Vector3 a = new Vector3(x, playerTransform.position.y + 10, 0);
                Vector3 b = new Vector3(x, playerTransform.position.y + 11, 0);
                Obstacles[6].active = true;
                Instantiate(Obstacles[6], a, Quaternion.identity);
                Instantiate(Obstacles[6], b, Quaternion.identity);
            }
            controlWall = 0;
        }
    }
}
