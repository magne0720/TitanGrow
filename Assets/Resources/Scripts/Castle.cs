using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 配置される二つの城の基底
/// </summary>
public class Castle : MonoBehaviour {

    //城の体力
    public int CastleHp;
    //生成する間隔
    public float Interval;
    //計測時間
   protected float SpawnTime;
    //出撃場所
    public   Vector3 offsetPosition;
    //出撃範囲
    protected float spawnRange;

    //兵士出撃のトリガー
    public bool GoSortie;

    public  static GameObject CreateCastle(string path, Vector3 pos = new Vector3())
    {
        //Debug.Log(path + "," + pos.z);
        string temp = path.Replace('\r', '\0');
        GameObject g;
        try
        {
            g = Instantiate(Resources.Load("Prefabs/" + temp, typeof(GameObject))) as GameObject;
        }
        catch
        {
            //オブジェクトパスが見つからない場合
            g = Instantiate(Resources.Load("Models/OUT_BOX", typeof(GameObject))) as GameObject;
        }
        g.AddComponent<Castle>();
        g.name = temp;
        g.transform.position = pos;

        return g;
    }
    /// <summary>
    /// 体力、生成時間、生成場所を指定する
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="fInterval"></param>
    /// <param name="spawnOffset"></param>
    public void Initialize(int hp,float fInterval,float fRange)
    {
        //オブジェクトの追加
        //ObjectManager.AddObject(gameObject);
        tag = "Castle";

        CastleHp = hp;
        Interval = fInterval;
        spawnRange = fRange;
        offsetPosition = transform.position+transform.forward*50.0f;
    }

    // Use this for initialization
    void Start () {
        
        GoSortie = false;

    }
   
	
	// Update is called once per frame
	void Update () {

       
       // SpawnRandom = Random.Range(0, 4);

        //兵士沸かせる

        //if (GoSortie == true)
        //{
        //    if (CastleHp > 50)
        //    {
                SpawnTime += Time.deltaTime;

              

        //    }
        //}
        
    }

    //兵士わくわくさん
    public virtual void Spawn()
    {

    }
}
