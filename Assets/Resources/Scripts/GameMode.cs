using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    const int TS_MAX = 2;
    const int TS_MIN = 0;


    public enum MODE
    {
        TITLE=0,GAME, GALLERY, OPTION,NUM
    };
    MODE mode;
    int titleselect, galleryselect;
    float selectGravity;
    bool isArrowed;

    public CameraControl cam;
    public Player player;
    public Controller controller;
    public CanvasControl canvas;

    const float playerspeed = 4.5f;
    // Use this for initialization
    void Start ()
    {
        isArrowed = false;

        mode = MODE.TITLE;
        titleselect = 0;

        if (cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
        }

        if (controller == null)
        {
            GameObject CO = new GameObject();
            CO.name = "Controller";
            controller = CO.gameObject.AddComponent<Controller>();
            controller.SetCamera(cam.GetComponent<CameraControl>());
        }

        ObjectManager.StartUpData();
        DataBaseManager.SetUpObjectData();
        StageCreator.StartUp();
        //GrowPlant.CreateGrowPlant("nat_001",new Vector3(),5);
    }

    // Update is called once per frame
    void Update () {
        Game();
	}

    void Game()
    {
        switch (mode)
        {
            case MODE.TITLE:
                TitleSelectInput();
                DispTitle();
                break;
            case MODE.GAME:
                if (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("circle"))
                {
                        GameEnd();
                        canvas.FadeOut();
                }
                break;
            case MODE.GALLERY:
                GallerySelectInput();
                DispGallery();
                break;
            case MODE.OPTION:
                break;
            case MODE.NUM:
                break;
            default:
                break;
        }
    }

    void DispCanvas(bool visible)
    {
        if (canvas != null)
        {
        }
    }

    //タイトルの表示
    void DispTitle()
    {
        if (canvas == null)
        {
            canvas = CanvasControl.CreateCanvas(cam.controlCamera).GetComponent<CanvasControl>();
        }
        else
        {
            canvas.ChangeText(titleselect);
        }
        //DispCanvas(true);
    }

    void DispGallery()
    {
        canvas.ChangeGallery(galleryselect);
    }

    void TitleSelectInput()
    {
        int axis =(int)Input.GetAxis("LRArrow")+(int)Input.GetAxis("Horizontal");
        //左右移動
        if (axis != 0)
        {
            selectGravity += Time.deltaTime;
            if (!isArrowed)
            {
                Debug.Log("select=" + selectGravity);
                //-1で左、1で右
                titleselect += (int)axis;
                isArrowed = true;
            }
            //一定秒後にめっちゃ早く選ぶ
            if (selectGravity > 0.5f)
            {
                Debug.Log("gravity");
                titleselect += (int)axis;
            }
            if (titleselect < TS_MIN) titleselect = TS_MIN;
            if (titleselect > TS_MAX) titleselect = TS_MAX;
        }
        else
        {
            isArrowed = false;
            selectGravity = 0;
        }
        //選択
        if (Input.GetButtonDown("circle") || Input.GetKeyDown(KeyCode.Space))
        {
            if (mode == MODE.TITLE)
            {
                switch (titleselect)
                {
                    case 0:
                        canvas.FadeIn();
                        StartUp();
                        break;
                    case 1:
                        canvas.HideImage();
                        GalleryStart();
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }
        }
    }
    void GallerySelectInput()
    {
        int size = DataBaseManager.GetObjectLength();
        float axis = (int)Input.GetAxis("LRArrow") + (int)Input.GetAxis("Horizontal");
        //左右移動
        if (axis != 0)
        {
            selectGravity += Time.deltaTime;
            if (!isArrowed)
            {
                //-1で左、1で右
                galleryselect += (int)axis;
                isArrowed = true;
            }
            //1秒後にめっちゃ早く選ぶ
            if (selectGravity > 0.5f)
            {
                galleryselect += (int)axis;
            }
            if (galleryselect < 0) galleryselect = 0;
            if (galleryselect >= size) galleryselect = size - 1;
        }
        else
        {
            isArrowed = false;
            selectGravity = 0;
        }
        if (Input.GetButtonDown("cross"))
        {
            mode = MODE.TITLE;
            canvas.HideImage();
        }
    }

    void StartUp()
    {
        mode = MODE.GAME;
        //ゲームの立ち上げ(ゲームプレイ時)
        //カメラの設定
        cam.Initialize();

        //３．ゲーム設定
        //playerの生成
        DataBaseManager.SetUpStartData();
        player = DataBaseManager.GetPlayer();

        controller.SetPlayer(player);

        //４．オブジェクト初期化
        ObjectManager.AllClear();

        //５．オブジェクト追加
        //CastleInstance();
        ObjectInstance();

        //６．ゲーム開始
        GameStart();
    }

    void GameStart()
    {
        mode = MODE.GAME;
        controller.SetControll(true);
    }

    void GameEnd()
    {
        mode = MODE.TITLE;
        controller.player = null;
        player = null;
        controller.SetControll(false);
        ObjectManager.AllClear();
        StageCreator.StartUp();
    }

    void GameStop()
    {

    }

    void GalleryStart()
    {
        mode = MODE.GALLERY;
    }

    void CastleInstance()
    {
        GameObject g = Instantiate(Resources.Load("Models/cat_001", typeof(GameObject)) as GameObject);
        g.name = "castle";
        g.transform.position = new Vector3(100, 0, 0);
        g.transform.localScale *= 0.05f;
        g.AddComponent<Castle>();
    }

    void ObjectInstance()
    {
        StageCreator.StartUp();
        //DataBaseManager.SpawnEnemyWave("EnemyPos");
    }
}
