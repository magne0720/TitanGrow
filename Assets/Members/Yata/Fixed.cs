using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixed : MonoBehaviour {
    public float Yaxis = 136;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Y軸固定
        Vector3 pos = transform.position;
        pos.y = Yaxis;
        transform.position = pos;
    }
}
