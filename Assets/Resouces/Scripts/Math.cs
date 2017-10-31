using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//計算用メソッド群

public class Math : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    /*
    直円錐の位置・向きが書かれていませんのでそこは適当に決めます。
... 円錐の頂点： 位置ベクトルpos
...円錐の底面の中心： 位置ベクトルdir（但し、height=|dir-pos|）
... 円錐の底面の半径： range
...テストする点： 位置ベクトルtarget=(x,y,z)
とするとき、
... 0≦(target-pos)･(dir-pos)≦height^2 （･はベクトルの内積）
... |(target-pos)×(dir-pos)|≦|target-pos|range*height/√(range^2+height^2) （×はベクトルの外積）
の両方を満たせばよい。
*/
    //視覚方向から円錐上に見る
    public static float SerchCone(Vector3 pos,Vector3 dir,float height,float range,Vector3 target)
    {
        float dis = float.MaxValue;
        Vector3 moving = (dir - pos).normalized*height;
        float dot = Dot(target - pos, moving - pos);
        // 0≦(p-a)･(d-a)≦h^2 （･はベクトルの内積）
        if (0 <= dot && dot <=height*height)
            //| (target - pos)×(dir - pos) |≦| target - pos | range * height /√(range ^ 2 + height ^ 2) （×はベクトルの外積）
            if (Length(Cross(target - pos, moving - pos)) <= Length(target - pos) * range * height / Mathf.Sqrt(range * range + height * height)){
                return Length(target-pos);
            }
        return dis;
    }



    //ベクトル上に指定したものが衝突しているか

    //球状の判定(球同士)

    //球状の判定(球と点)

    //カプセル衝突

    //面と球

    //面とカプセル

    //内積
    public static float Dot(Vector3 a,Vector3 b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }


    //外積
    public static Vector3 Cross(Vector3 a,Vector3 b)
    {
        Vector3 c;

        c.x = a.y * b.z - a.z * b.y;
        c.y = a.z * b.x - a.x * b.z;
        c.z = a.x * b.y - a.y * b.x;

        return c;
    }

    public static Vector3 Abs(Vector3 s)
    {
        if (s.x < 0) s.x *= -1;
        if (s.y < 0) s.y *= -1;
        if (s.z < 0) s.z *= -1;

        return s;
    }

    public static float Length(Vector3 p)
    {
        return Mathf.Sqrt(p.x * p.x + p.y * p.y + p.z * p.z);
    }

    //デグリをラジアンに変換する(単位はΠ)
    public static float DegToRag(float deg)
    {
        //ラジアン = 度 × 円周率 ÷ 180
        return deg * Mathf.PI / 180;
    }

    //ラジアンをデグリに変換する(単位は度)
    public static float RagToDeg(float rag)
    {
        //度 = ラジアン × 180 ÷ 円周率
        return rag * 180 / Mathf.PI;
    }

}
