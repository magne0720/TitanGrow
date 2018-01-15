using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ptarget : MonoBehaviour {

    public Transform target;
    public float _height = 0f;

    void Start()
    {
    }

    void Update()
    {

        transform.position = new Vector3(target.position.x, _height, target.position.z);

    }
}
