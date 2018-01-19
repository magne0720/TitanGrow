using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ki : MonoBehaviour {
    public GameObject kii0;
    public GameObject kii1;
    public GameObject kii2;
    public GameObject rock;
    bool flag = false;
    int kim;
    int elia = 90;
    bool[] kiflag;
    int count=1;
    int[] kih;
    int AElia = 0;
    int BELIA = 0;
    // Use this for initialization
    void Start() {
        int range = 0;
        count = 1;
        kih = new int[elia];
        for(int i = 0; i < elia; i++)
        {
            kih[i] = -1;
        }
        kiflag = new bool[elia];
        for (int y = 0; y < elia; y++)
        {
            for (int x = 0; x < elia/2; x++)
            {
                if (count < 300)
                {
                    if (flag == false && kiflag[x] == false)
                    {


                        range = Random.Range(0, 100);
                        if (range == 1)
                        {
                            int i = Random.Range(0, 3);

                            GameObject gg=null;
                            switch (i)
                            {
                                case 0:
                                    gg= kii0;
                                    kih[x] = 0;
                                    kim = 0;
                                    break;
                                case 1:
                                    gg = kii1;
                                    kih[x] = 1;
                                    kim = 1;
                                    break;
                                case 2:
                                    gg = kii2;
                                    kih[x] = 2;
                                    kim = 2;
                                    break;
                            }

                            int rand = Random.Range(0, 10);
                            gg.transform.position = new Vector3((x-(elia/2))*2 , 0.5f, (y-(elia/2))*2 );
                            Instantiate(gg);
                            
                            gg.gameObject.name = count.ToString();
                            count++;
                            
                            flag = true;
                            kiflag[x] = true;
                        }
                        else
                        {
                            flag = false;
                            kiflag[x] = false;
                            kih[x] = -1;
                            kim = -1;
                        }


                    }
                    else
                    {
                        if (flag || (flag && kiflag[x]))
                        {
                            range = Random.Range(0, 3);
                        }
                        else
                        {
                            range = Random.Range(0, 3);
                        }
                        if (range != 1)
                        {
                            GameObject gg = null;
                            switch (kih[x])
                            {
                                case 0:
                                    gg = kii0;
                                    kih[x] = 0;
                                    kim = 0;
                                    break;
                                case 1:
                                    gg = kii1;
                                    kih[x] = 1;
                                    kim = 1;
                                    break;
                                case 2:
                                    gg = kii2;
                                    kih[x] = 2;
                                    kim = 2;
                                    break;
                            }
                            switch (kim)
                            {
                                case 0:
                                    gg = kii0;
                                    kih[x] = 0;
                                    kim = 0;
                                    break;
                                case 1:
                                    gg = kii1;
                                    kih[x] = 1;
                                    kim = 1;
                                    break;
                                case 2:
                                    gg = kii2;
                                    kih[x] = 2;
                                    kim = 2;
                                    break;
                            }
                            Instantiate(gg);
                            gg.gameObject.name = count.ToString();
                            count++;
                            int rand = Random.Range(0, 10);
                            gg.transform.position = new Vector3((x-(elia/2))*2, 0.5f, (y-(elia/2))*2 );
                            flag = true;
                            kiflag[x] = true;
                        }
                        else
                        {
                            flag = false;
                            kiflag[x] = false;
                            kih[x] = -1;
                            kim = -1;
                        }

                    }
                }

            }
        
        }

    }


	
	// Update is called once per frame
	void Update () {
        
	}
    void create()
    {
        
    }
}
