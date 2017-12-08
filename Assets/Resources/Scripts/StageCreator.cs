using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreator : MonoBehaviour {

    //ステージの範囲
    public const int STAGE_AREA = 50;
    //初期繁殖
    public const int BREED_LIMIT = 200;

    static int[,] woodlist;

    public static float Area_A = 0;
    public static float Area_B = 0;
    //上限
    public static int BREED_MAX=10;
    
    //二つの城の位置
    public static Vector3 AreaPos_A;
    public static Vector3 AreaPos_B;
    
    public static string[] ObjNames;

    public static void StartUp()
    {
        int BreedCount = 0;
        Vector3 Vec3 = new Vector3();
        woodlist = new int[STAGE_AREA, STAGE_AREA];

        //生成リストを取得
        ObjNames = DataBaseManager.SetUpStartObjectData();

        //A国の範囲内には生成しない
        for (int xz = 0; xz < STAGE_AREA; xz++)
        {
            for (int yz = 0; yz < STAGE_AREA; yz++)
            {
                woodlist[xz, yz] = -1;
                if (xz < AreaPos_A.x + (Area_A / 2) && xz > AreaPos_A.x - (Area_A / 2))
                {
                    if (yz < AreaPos_A.z + (Area_A / 2) && yz > AreaPos_A.z - (Area_A / 2))
                    {
                        woodlist[xz, yz] = -2;
                    }
                }
                //B国の範囲内には生成しない
                Vec3 = new Vector3(xz, 0, yz);
                if (Math.Length(AreaPos_B - Vec3) < Area_B)
                {
                    woodlist[xz, yz] = -2;
                }
            }
        }

        //第１世代を生成
        for (int z = 0; z < STAGE_AREA; z++)
        {
            for (int x = 0; x < STAGE_AREA; x++)
            {
                if (BreedCount < BREED_LIMIT)//第１世代
                {
                    if (Random.Range(0, 100) >90 && woodlist[z, x] == -1 )
                        if (woodlist[z, x] == -1)
                        {
                            int rInst = Random.Range(0, ObjNames.Length);
                            GameObject g = BaseObject.CreateObject(ObjNames[rInst]);
                            g.transform.position = new Vector3((x-STAGE_AREA/2) * 10, 0, (z - STAGE_AREA / 2) * 10);
                            woodlist[z, x] = rInst;
                            BreedCount++;
                        }
                }
            }
        }
    }

    //次世代を生成
    public static void NextStartUp()
    {
        int count = BREED_MAX;
        string[,] temp = new string[50,50];
        for (int z = 0; z < STAGE_AREA; z++)
        {
            for (int x = 0; x < STAGE_AREA; x++)
            {
                if (woodlist[z, x] > 0)
                {
                    search(z, x, count--, woodlist[z, x]);
                }
            }
        }
    }

    static void search(int z, int x, int move, int random)
    {
        GameObject obj;
        if (move < 0)
        {
            return;
        }
        obj = BaseObject.CreateObject(ObjNames[random]);
        obj.transform.position = new Vector3((x - STAGE_AREA / 2) * 10, 0, (z - STAGE_AREA / 2) * 10);

    }
}
