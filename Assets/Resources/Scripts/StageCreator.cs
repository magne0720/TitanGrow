using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreator : MonoBehaviour {
    bool flag = false;
    int kim;
    public int elia = 50;
    int count = 1;
    int[] kih;
    int[,] woodlist;
    public int AElia = 0;
    public int BElia = 0;
    public int MAX=0;
    public int limit = 15;
    public Vector3 AEliaPos;
    public Vector3 BEliaPos;
    int[,] ground;
    //NOZU
    public List<GameObject> woods;
    int ki=0;
    int iwa = 0;
    // Use this for initialization

    void search(int z, int x, int move, int random)
    {

        if (move < woodlist[z, x])
        {
            return;
        }
        int rand = Random.Range(0, move);

        if (woodlist[z, x] == -1 && rand != 1)
        {
            Instantiate(woods[random], new Vector3(x*4, 1, z*4), transform.rotation);
            search4(z, x, move - 1, random);
            ki++;
        }
    }
    void search4(int z, int x,int move,int random)
    {
        woodlist[z, x] = move;
        if (move <= 0) return;
        if (z + 1 < elia)
        {
            search(z + 1, x, move,random);
        }
        if (x - 1 > -1)
        {

            search(z, x - 1, move,random);
        }
        if (z - 1 > -1)
        {

            search(z - 1, x, move,random);
        }
        if (x + 1 < elia)
        {

            search(z, x + 1, move,random);
        }


    }
    void Start()
    {
        Vector3 Vec3=new Vector3();
        woodlist = new int[elia, elia];
        ground = new int[elia / 5, elia / 5];
        for(int xz = 0; xz < elia; xz++)
        {
            for (int yz = 0; yz < elia; yz++)
            {
                woodlist[xz, yz] = -1;
                ground[xz, yz] = -1;
                if (xz < AEliaPos.x + (AElia / 2) && xz > AEliaPos.x - (AElia / 2))
                {
                    if (yz < AEliaPos.z + (AElia / 2) && yz > AEliaPos.z - (AElia / 2))
                    {
                        woodlist[xz, yz] = 5;
                    }
                }
                Vec3 = new Vector3(xz, 0, yz);
                if (Math.Length(BEliaPos-Vec3)<BElia)
                {
                    woodlist[xz, yz] = 5;
                }
                /*if (xz < BEliaPos.x + (BElia / 2) && xz > BEliaPos.x - (BElia / 2))
                {
                    if (yz < BEliaPos.z + (BElia / 2) && yz > BEliaPos.z - (BElia / 2))
                    {
                        woodlist[xz, yz] = 5;
                    }
                }
                */
            }
        }
        for (int z =0; z < elia; z++)
        {
            for (int x =0; x < elia; x++)
            {
                if (count < limit)
                {
                    if (Random.Range(0, 100) == 1 && woodlist[z, x] == -1&&z<elia/2)
                    {

                        int rInst = Random.Range(0, woods.Count-1);
                        Instantiate(woods[rInst], new Vector3(x*4, 1, z*4 ), transform.rotation);
                        search(z, x, MAX, rInst);
                        count++;
                        ki++;

                    }
                  
                }
                if (Random.Range(0, 100) == 1 && woodlist[z, x] == -1)
                {
                    Instantiate(woods[3], new Vector3(x * 4, 1, z * 4), transform.rotation);
                    iwa++;

                }
                
            }
            Debug.Log(ki + "," + iwa);
        }
        /*
        //        }
        //int range = 0;
        //count = 1;
        //kih = new int[elia];
        //for (int i = 0; i < elia; i++)
        //{
        //    kih[i] = -1;
        //}
        //for (int y = 0; y < elia; y++)
        //{
        //    for (int x = 0; x < elia / 2; x++)
        //    {
        //        if (count < 300)
        //        {
        //            if (flag == false && kih[x] == -1)
        //            {
        //
        //
        //                range = Random.Range(0, 100);
        //                if (range == 1)
        //                {
        //                    int i = Random.Range(0, 4);
        //
        //                    GameObject gg = null;
        //                    switch (i)
        //                    {
        //                        case 0:
        //                            gg = kii0;
        //                            kih[x] = 0;
        //                            kim = 0;
        //                            break;
        //                        case 1:
        //                            gg = kii1;
        //                            kih[x] = 1;
        //                            kim = 1;
        //                            break;
        //                        case 2:
        //                            gg = kii2;
        //                            kih[x] = 2;
        //                            kim = 2;
        //                            break;
        //                        case 3:
        //
        //                            break;
        //                    }
        //
        //                    gg.transform.position = new Vector3((x - (elia / 2)) * 2, 0.5f, (y - (elia / 2)) * 2);
        //                    Instantiate(gg);
        //
        //                    gg.gameObject.name = count.ToString();
        //                    count++;
        //
        //                    flag = true;
        //                    kiflag[x] = true;
        //                }
        //                else
        //                {
        //                    flag = false;
        //                    kiflag[x] = false;
        //                    kih[x] = -1;
        //                    kim = -1;
        //                }
        //
        //
        //            }
        //            else
        //            {
        //                if (flag || (flag && kiflag[x]))
        //                {
        //                    range = Random.Range(0, 3);
        //                }
        //                else
        //                {
        //                    range = Random.Range(0, 3);
        //                }
        //                if (range != 1)
        //                {
        //                    GameObject gg = null;
        //                    switch (kih[x])
        //                    {
        //                        case 0:
        //                            gg = kii0;
        //                            kih[x] = 0;
        //                            kim = 0;
        //                            break;
        //                        case 1:
        //                            gg = kii1;
        //                            kih[x] = 1;
        //                            kim = 1;
        //                            break;
        //                        case 2:
        //                            gg = kii2;
        //                            kih[x] = 2;
        //                            kim = 2;
        //                            break;
        //                    }
        //                    switch (kim)
        //                    {
        //                        case 0:
        //                            gg = kii0;
        //                            kih[x] = 0;
        //                            kim = 0;
        //                            break;
        //                        case 1:
        //                            gg = kii1;
        //                            kih[x] = 1;
        //                            kim = 1;
        //                            break;
        //                        case 2:
        //                            gg = kii2;
        //                            kih[x] = 2;
        //                            kim = 2;
        //                            break;
        //                    }
        //                    Instantiate(gg);
        //                    gg.gameObject.name = count.ToString();
        //                    count++;
        //                    gg.transform.position = new Vector3((x - (elia / 2)) * 2, 0.5f, (y - (elia / 2)) * 2);
        //                    flag = true;
        //                    kiflag[x] = true;
        //                }
        //                else
        //                {
        //                    flag = false;
        //                    kiflag[x] = false;
        //                    kih[x] = -1;
        //                    kim = -1;
        //                }
        //            }
        //        }
        //    }
        //}
        */

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
