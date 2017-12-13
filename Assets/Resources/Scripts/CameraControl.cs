using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    const float CAM_Y_MAX = 260.0f;
    const float CAM_Y_MIN = 180.0f;
    const float CAM_DISTANCE = 2.0f;

    public GameObject player; //player
    public Vector3 direction;
    public Camera controlCamera;
    
    public float distance;
    public float speedX;
    public float speedY;

    // Use this for initialization
    void Start()
    {
        Initialize();
        //タグ("Player")を検出
        player = GameObject.FindGameObjectWithTag("Player");
        if (controlCamera == null)
        {
            controlCamera = GetComponent<Camera>();
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
            SetCameraFar(player.transform.localScale.z * 100);

            //playerの移動量分、カメラも移動
            transform.position = new Vector3(player.transform.position.z,0,player.transform.position.z);
            //transform.position = new Vector3(0, 0,playerPos.z-10);
        }
        else
        {
            distance = 7.0f;
            direction.x += speedX*0.01f;
            direction.y += speedY*0.01f;
            if (direction.x >= 360.0f) direction.x = 0.0f;
            if (direction.x < 0.0f) direction.x = 360.0f;
            if (direction.y >= CAM_Y_MAX) direction.y = CAM_Y_MAX;
            if (direction.y < CAM_Y_MIN) direction.y = CAM_Y_MIN;

            float angX = Math.Rotate(Vector3.up, direction.x, CAM_DISTANCE * distance).x;
            float angY = Math.Rotate(Vector3.left, direction.y, CAM_DISTANCE * distance).y;
            float angZ = Math.Rotate(Vector3.left, direction.x, CAM_DISTANCE * distance).x;
            transform.position = new Vector3(angX, angY, angZ);

            transform.LookAt(new Vector3(0,5,0));
        }
    }

    public void Initialize()
    {  
        //初期化
        speedX = 3.0f;
        speedY = 3.0f;

        distance = 4.0f;

        direction.x = 0;
        direction.y = 210.0f;
    }

    public void InputJoystick(float x, float y)
    {
        if (player != null)
        {
            direction.x += x * speedX;
            direction.y += y * speedY;
            if (direction.x >= 360.0f) direction.x = 0.0f;
            if (direction.x < 0.0f) direction.x = 360.0f;
            if (direction.y >= CAM_Y_MAX) direction.y = CAM_Y_MAX;
            if (direction.y < CAM_Y_MIN) direction.y = CAM_Y_MIN;

            float angX = Math.Rotate(Vector3.up, direction.x, CAM_DISTANCE * distance * player.transform.localScale.x).x;
            float angY = Math.Rotate(Vector3.left, direction.y, CAM_DISTANCE * distance * player.transform.localScale.y).y;
            float angZ = Math.Rotate(Vector3.left, direction.x, CAM_DISTANCE * distance * player.transform.localScale.z).x;
            transform.position = new Vector3(angX, angY, angZ) + new Vector3(player.transform.position.x,0,player.transform.position.z);

            transform.LookAt(new Vector3(player.transform.position.x,player.transform.localScale.y,player.transform.position.z));
        }
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
