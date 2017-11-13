using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの進行
///１．ゲームの立ち上げ
///２．マネージャー
///３．ゲーム設定
///４．オブジェクト情報
///５．オブジェクト追加
///６．ゲーム開始
/// </summary>
public class GameMode : MonoBehaviour {

    public GameObject camera;
    public CollisionManager CM;
    public Player player;
    public Controller controller;

    const float playerspeed = 4.5f;

    // Use this for initialization
    void Start ()
    {
        StartUp();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartUp()
    {
        //ゲームの立ち上げ(ゲームプレイ時)
        //カメラの設定
        if (camera == null)
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        //CollisionManagerの生成
        if (CM == null)
        {
            //CM = this.gameObject.AddComponent<CollisionManager>();
        }
        //３．ゲーム設定
        //playerの生成
        // if (player == null)
        if(player==null)
        {
            player = Player.Create("a").AddComponent<Player>();
            player.tag = "Player";
         }
        if (controller == null)
        {
            controller= this.gameObject.AddComponent<Controller>();
            controller.player = player;
            controller.camera = camera.GetComponent<CameraControl>();
            controller.camera.player = player.gameObject;
        }

        //４．オブジェクト情報

        //５．オブジェクト追加

        //６．ゲーム開始
        GameStart();
    }

    void GameStart()
    {

    }

    void GameEnd()
    {

    }

    void GameStop()
    {

    }

    public static void debugPoint(GameObject g)
    {
        for (int x = -2; x <= 2; x++)
            for (int y = -2; y <= 2; y++)
                for (int z = -2; z <= 2; z++)
                {
                    GameObject go = Instantiate(g);
                    go.transform.position = new Vector3(x * 10, z * 10, y * 10);
                }
    }
}
