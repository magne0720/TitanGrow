using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//存在してる間、世代を繰り返し作るクラス
public class GrowPlant : BaseObject {
    
    float scale = 0;
    Vector3 OriginScale;
    public bool isBreed;
    public int breedRange = 0;//次世代を作る確率
    string breedName;


    public static GameObject CreateGrowPlant(string path, Vector3 pos = new Vector3(),int breed=0)
    {
        string temp = path.Substring(0,path.Length);
        Debug.Log("create="+path);
        GameObject g;
        try
        {
            g = Instantiate(Resources.Load("Models/" + temp, typeof(GameObject))) as GameObject;
        }
        catch
        {
            //オブジェクトパスが見つからない場合
            g = Instantiate(Resources.Load("Models/OUT_BOX", typeof(GameObject))) as GameObject;
        }
        g.AddComponent<GrowPlant>().breedRange = breed;
        g.name = temp;
        g.transform.position = pos;

        return g;
    }

    // Use this for initialization
    void Start ()
    {
        isBreed=(Random.Range(0, breedRange) != 0&&breedRange>0)? false : true;
        Initialize();
        OriginScale = transform.localScale;
        transform.localScale *= scale;
        breedName = name;

        if (Random.Range(0, 100) >= 30 && !isBreed)
        {
            isBreed = true;
            GameObject g = CreateGrowPlant(breedName, transform.position + Math.RotateY(new Vector3(0, 0, 1), Random.Range(0, 360),5), breedRange - 1);
            g.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        }
        try
        {
            foreach (GameObject g in gameObject.GetComponentsInChildren<GameObject>())
            {
                g.gameObject.layer = 8;
            }
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (scale <= 1.0f)
        {
            // scale += Time.deltaTime * 0.01f;
            scale += Time.deltaTime;
            transform.localScale = OriginScale * (scale + 1);

            if (Random.Range(0, 100) == 100)
            {
                GameObject g = CreateGrowPlant(breedName, transform.position + Math.RotateY(new Vector3(0, 0, 1), Random.Range(0, 360), 5), breedRange - 1);
                g.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            }
        }
    }
    //変化する確率
    void BreedMutation()
    {

    }
}
