using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHcontroller : MonoBehaviour
{
    //ブラックホールの重力　g=M/r^2 (惑星の重さをBHからの距離の2乗で割る)
    private float MASS = 10;
    float collision_radius;
    float gravityCoef = 10;
    /*
     円形の当たり判定を作る。（範囲は、重力の影響がほぼ0になる場所まで。）
    範囲に入った動的オブジェクトをリスト化し、全てに係る重力を計算
    重力をX方向、Y方向に分解。
    Addforceする。
     */
    // Start is called before the first frame update
    void Start()
    {
        float radius_break = MASS / 0.01f;//円形の当たり判定の半径を決定する。重力の影響が十分に小さくなるまで
        collision_radius = Mathf.Sqrt(radius_break);
        CircleCollider2D col = this.gameObject.GetComponent<CircleCollider2D>();
        col.radius = collision_radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Geffect")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            GameObject go = collision.gameObject;
            float r = Vector2.Distance(go.transform.position, transform.position);
            float gravity = MASS / (r * r);
            Vector2 XYgravity = new Vector2(Mathf.Cos(GetAngle(go.transform.position,transform.position))*gravity, Mathf.Sin(GetAngle(go.transform.position, transform.position)) * gravity);
            rb.AddForce(XYgravity*gravityCoef);
            Debug.Log(go.name);
        }
    }

    private float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);

        return rad;
    }
}
