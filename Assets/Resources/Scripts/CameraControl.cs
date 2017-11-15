using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    const float CAM_Y_MAX = 260.0f;
    const float CAM_Y_MIN = 160.0f;
    const float CAM_DISTANCE = 2.0f;

    public GameObject player; //player
    public Vector3 playerPos;
    public Vector3 direction;
    
    public float distance;
    public float speedX;
    public float speedY;

    // Use this for initialization
    void Start()
    {
        //初期化
        speedX = 7.0f;
        speedY = 7.0f;
        distance = 1.0f;

        //タグ("Player")を検出
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerPos = player.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (distance < 1) distance = 1;
            if (Input.GetKey(KeyCode.J))
            {
                SetDistance(distance + 0.2f);
            }
            if (Input.GetKey(KeyCode.K))
            {
                SetDistance(distance - 0.2f);
            }
            if (Input.GetKey(KeyCode.L))
            {
                SetCameraFar(5.0f);
            }
            SetCameraFar(player.transform.localScale.z * 20);


            //playerの移動量分、カメラも移動
            transform.position = playerPos;
            playerPos = player.transform.position;
            //transform.position = new Vector3(0, 0,playerPos.z-10);
        }
    }

    public void InputJoystick(float x,float y)
    {
        direction.x += x * speedX;
        direction.y += y * speedY;
        if (direction.x >= 360.0f) direction.x = 0.0f;
        if (direction.x < 0.0f) direction.x = 360.0f;
        if (direction.y >= CAM_Y_MAX) direction.y = CAM_Y_MAX;
        if (direction.y < CAM_Y_MIN) direction.y = CAM_Y_MIN;
        
        float angX = Math.Rotate(Vector3.up, direction.x, CAM_DISTANCE *distance* player.transform.localScale.x).x;
        float angY = Math.Rotate(Vector3.left, direction.y, CAM_DISTANCE *distance* player.transform.localScale.y).y;
        float angZ = Math.Rotate(Vector3.left, direction.x, CAM_DISTANCE *distance* player.transform.localScale.z).x;
        transform.position = new Vector3(angX, angY, angZ)+transform.position;

        transform.LookAt(playerPos);

        //transform.RotateAround(playerPos, Vector3.up, x * speedX*Time.deltaTime);
        //transform.RotateAround(playerPos, Vector3.right, y * speedY*Time.deltaTime);
    }

    void SetDistance(float s)
    {
        distance = s;
        if (s > 0)
        {
            distance *= -1;
        }
    }
    void SetCameraFar(float dis)
    {
        GetComponent<Camera>().farClipPlane = dis;
    }
}
