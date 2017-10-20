using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter {

    public float eyeRange;//視覚距離
    //性格
    public enum EMOTION
    {
        NOTHING, JOY, SAD, GLAD, ANGER
    };
    public EMOTION myEmotion;//自身の感情
    public float attackDelay;//攻撃速度
    public Vector3 lastTarget;//敵の城の拠点

    static Enemy Create(string path)
    {
        Enemy e = new Enemy();

        e.MyModel = Resources.Load(path) as GameObject;

        return e;
    }

	// Use this for initialization
	void Start () {
        lastTarget = new Vector3(20, 20, 0);
        TargetPosition = lastTarget;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}
}
