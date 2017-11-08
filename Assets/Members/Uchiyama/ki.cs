using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ki : MonoBehaviour {
    public GameObject kii;
    bool flag = false;
    int elia = 300;
    bool[] kiflag;
    
	// Use this for initialization
	void Start () {
        int range = 0;
        kiflag = new bool[elia];
		for (int y = 0; y <elia/2; y++)
        {
            for (int x = 0; x <elia; x++)
            {

                if (flag == false && kiflag[x] == false)
                {
                    range = Random.Range(0, 900);
                    if (range == 1)
                    {
                        Instantiate(kii);
                        kii.transform.position = new Vector3(x - (elia/2), 1.0f, y);
                        flag = true;
                        kiflag[x] = true;
                    }
                    else
                    {
                        flag = false;
                        kiflag[x] = false;
                    }
                }
                else
                {
                    if (flag == true || (flag == true && kiflag[x]))
                    {
                        range = Random.Range(0, 3);
                    }
                    else
                    {
                        range = Random.Range(0,3);
                    }
                    if (range != 1)
                    {
                        Instantiate(kii);
                        kii.transform.position = new Vector3(x - (elia / 2), 1.0f, y );
                        flag = true;
                        kiflag[x] = true;
                    }
                    else
                    {
                        flag = false;
                        kiflag[x] = false;
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
