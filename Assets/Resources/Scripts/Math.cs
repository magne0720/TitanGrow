using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//計算用メソッド群

public class Math
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    /*
    直円錐の位置・向きが書かれていませんのでそこは適当に決めます。
...円錐の頂点： 位置ベクトルpos
...円錐の底面の中心： 位置ベクトルdir（但し、height=|dir-pos|）
...円錐の底面の半径： range
...テストする点： 位置ベクトルtarget=(x,y,z)
とするとき、
... 0≦(target-pos)･(dir-pos)≦height^2 （･はベクトルの内積）
... |(target-pos)×(dir-pos)|≦|target-pos|range*height/√(range^2+height^2) （×はベクトルの外積）
の両方を満たせばよい。
*/
    //視覚方向から円錐上に見る
    public static float SearchCone(Vector3 pos, Vector3 dir, float height, float range, Vector3 target)
    {
        float dis = float.MaxValue;
        Vector3 moving = dir;
        float dot = Dot(target - pos, moving - pos);
        // 0≦(p-a)･(d-a)≦h^2 (･はベクトルの内積)
        if (0 <= dot && dot <= height * height)
            //|(target - pos)×(dir - pos) |≦| target - pos | range * height /√(range ^ 2 + height ^ 2)(×はベクトルの外積)
            if (Length(Cross(target - pos, moving - pos)) <= Length(target - pos) * range * height / Mathf.Sqrt(range * range + height * height))
            {
                return Length(target - pos);
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
    public static float Dot(Vector3 a, Vector3 b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }


    //外積
    public static Vector3 Cross(Vector3 a, Vector3 b)
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
    //中心から指定角回転させる
    public static Vector3 RotateX(Vector3 target, float deg, float range = 1.0f)
    {
        Vector3 vector = (target).normalized;
        //ラジアンに変換
        float rag = DegToRag(deg);

        float ay = vector.y * Mathf.Cos(rag) - vector.z * Mathf.Sin(rag);
        float az = vector.y * Mathf.Sin(rag) + vector.z * Mathf.Cos(rag);

        vector.y = ay * range;
        vector.z = az * range;

        return vector;
    }
    public static Vector3 RotateY(Vector3 target, float deg, float range = 1.0f)
    {
        Vector3 vector = (target).normalized;
        //ラジアンに変換
        float rag = DegToRag(deg);

        float az = vector.z * Mathf.Cos(rag) - vector.x * Mathf.Sin(rag);
        float ax = vector.z * Mathf.Sin(rag) + vector.x * Mathf.Cos(rag);

        vector.z = az * range;
        vector.x = ax * range;

        return vector;
    }
    public static Vector3 RotateZ(Vector3 target, float deg, float range = 1.0f)
    {
        Vector3 vector = (target).normalized;
        //ラジアンに変換
        float rag = DegToRag(deg);

        float ax = vector.x * Mathf.Cos(rag) - vector.y * Mathf.Sin(rag);
        float ay = vector.x * Mathf.Sin(rag) + vector.y * Mathf.Cos(rag);

        vector.x = ax * range;
        vector.y = ay * range;

        return vector;
    }
    public static Vector3 Rotate(Vector3 target, float deg, float range = 1.0f)
    {
        Vector3 vector = (target).normalized;
        //ラジアンに変換
        float rag = DegToRag(deg);

        float ax = vector.x * Mathf.Cos(rag) - vector.y * Mathf.Sin(rag);
        float ay = vector.x * Mathf.Sin(rag) + vector.y * Mathf.Cos(rag);

        vector.x = ax * range;
        vector.y = ay * range;

        return vector;
    }

    //方向ベクトルから右方向に固有角度で自身の視認範囲のベクトルを取得する(XZ平面上)
    public static Vector3 getDirectionDegree(Vector3 target, float deg, float range = 1.0f)
    {
        Vector3 vector = target.normalized;
        //ラジアンに変換
        float rag = DegToRag(deg);

        float ax = vector.x * Mathf.Cos(rag) - vector.z * Mathf.Sin(rag);
        float az = vector.x * Mathf.Sin(rag) + vector.z * Mathf.Cos(rag);

        vector.x = ax * range;
        vector.z = az * range;

        return vector;
    }
    //右側から見て内側にあるか
    public static bool OnDirectionRight(Vector3 pos,Vector3 dir,Vector3 target, float rightDeg)
    {
        //自身の向いている方向から右に視認範囲分回転
        Vector3 to = getDirectionDegree(dir, rightDeg);
        //敵の位置
        Vector3 t = target-pos;

        if (to.x * t.z - t.x * to.z < 0)
        {
            return true;
        }
        return false;
    }
    //右側から見て内側にあるか
    public static bool OnDirectionLeft(Vector3 pos,Vector3 dir,Vector3 target, float leftDeg)
    {
        //自身の向いている方向から右に視認範囲分回転
        Vector3 to = getDirectionDegree(dir, -leftDeg);
        //敵の位置
        Vector3 t = target-pos;

        if (to.x * t.z - t.x * to.z > 0)
        {
            return true;
        }
        return false;
    }

    //扇状に見る
    public static bool OnDirectionFan(Vector3 pos, Vector3 dir, Vector3 target, float openDeg)
    {
        if (OnDirectionLeft(pos,dir, target, openDeg) && OnDirectionRight(pos,dir, target, openDeg))
            return true;
        return false;
    }
}