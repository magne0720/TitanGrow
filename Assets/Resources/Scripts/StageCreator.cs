using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreator : MonoBehaviour {

    //ステージの範囲
    public const int STAGE_AREA = 250;
    //初期繁殖
    public const int BREED_LIMIT = 250; 

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
                woodlist[xz, yz] = 0;
                if (xz < AreaPos_A.x + (Area_A / 2) && xz > AreaPos_A.x - (Area_A / 2))
                {
                    if (yz < AreaPos_A.z + (Area_A / 2) && yz > AreaPos_A.z - (Area_A / 2))
                    {
                        woodlist[xz, yz] = -1;
                    }
                }
                //B国の範囲内には生成しない
                Vec3 = new Vector3(xz, 0, yz);
                if (Math.Length(AreaPos_B - Vec3) < Area_B)
                {
                    woodlist[xz, yz] = -1;
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
                    if (Random.Range(0, 100) == 0 && woodlist[z, x] == 0)
                    {
                        int rInst = Random.Range(0, ObjNames.Length);
                        GameObject g = GrowPlant.CreateGrowPlant(ObjNames[rInst], new Vector3((x - STAGE_AREA / 2) * 5, 0, (z - STAGE_AREA / 2) * 5), 8);
                        woodlist[z, x] = rInst;
                        BreedCount++;
                    }
                }
            }
        }
    }
}
