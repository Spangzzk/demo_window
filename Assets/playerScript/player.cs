using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("�ٶ����")]
    public float playerSpeed;
    public float rotateSpeed;
    [Header("�������")]
    public float rotation;
    public float N = 0;
    public float aboveRotation = 0;
    public float stopThere = 0;
    public float startThere = 0;
    [Header("�������")]
    public Rigidbody2D playerRB;
    public Transform playerTransform;
    public GameObject obstaclePrefab;
    

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerSpeed = 1f;
        rotateSpeed = 1.5f;
        playerRB.velocity = new Vector2(playerRB.velocity.x, playerSpeed);
    }

    
    void Update()
    {
        adjustStay();
        playerMove();
    }

    void playerMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        
        if (horizontal != 0f && Mathf.Abs(N) <= Mathf.Abs(horizontal)) 
        {
            N = Mathf.Max(Mathf.Abs(N), Mathf.Abs(horizontal));
            // ���㴬����ת�Ƕ�
            rotation = -horizontal * 180 + aboveRotation;
            
            if(rotation < -70)
            {
                rotation = -70;
            }
            else if(rotation > 70)
            {
                rotation = 70;
            }
            // ���㴬���ٶȺ��ƶ�����
            Vector2 direction = new Vector2(Mathf.Cos((rotation+90) * Mathf.Deg2Rad), Mathf.Sin((rotation + 90) * Mathf.Deg2Rad));

            playerRB.velocity = direction * rotateSpeed;
        }
        if(horizontal == 0f)
        {
            N = 0;
            aboveRotation = rotation;
        }
        
    }
    private void FixedUpdate()
    {
        
        // ���ô�����ת�Ƕ�
        playerTransform.rotation = Quaternion.Euler(0, 0, rotation);
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerRB.velocity = new Vector2(0, 0);
        stopThere = 1;
    }

    void adjustStay()
    {
        if(stopThere != 0)
        {
            startThere += Time.deltaTime;
            playerRB.velocity *= startThere;
            if(startThere > 1)
            {
                stopThere = 0;
                startThere = 0;
            }
        }
        
        
    }
}
