using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maptest : MonoBehaviour {

    public GameObject miniMap;

	// Use this for initialization
	void Start () {
        miniMap.SetActive(true);

    }

    // Update is called once per frame
    void Update ()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            miniMap.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            miniMap.SetActive(false);

        }

    }
}
