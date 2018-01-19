using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ミニマップの表示をする
public class MiniMap : MonoBehaviour {

    public Camera mapCam;
    public Player player;

    public Transform target;
    public float _height = 0f;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //transform.rotation = player.transform.rotation;

        //foreach(GameObject g in ObjectManager.GameObjects)
        //{
        //    if (Math.OnSricleAndPoint(player.transform.position, 200.0f, g.transform.position))
        //    {
        //        //g.  q

        //    }
            
        //}

    }
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, _height, target.position.z);
        transform.eulerAngles = new Vector3(90f, 0f, -target.eulerAngles.y);
    }

    /// <summary>
    /// ミニマップを使う際の初期設定
    /// </summary>
    /// <param name="p">中央に置かれるプレイヤーのクラス</param>
    /// <param name="c">ミニマップに使うカメラのクラス</param>
    public void Initialize(Player p,Camera c)
    {
        _height =200;
        player = p;
        mapCam = c;
        target = player.transform;

        mapCam.clearFlags = CameraClearFlags.Depth;
        //表示レイヤーを８(MAP)に設定
        mapCam.cullingMask = 1 << 8;
        //表示場所を右上に設定
        mapCam.rect = new Rect(0.5f, 0.5f, 1.0f, 1.0f);

    }
}
