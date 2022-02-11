using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHcontroller : MonoBehaviour
{
    //ブラックホールの重力　g=M/r^2 (惑星の重さをBHからの距離の2乗で割る)
    private float MASS = 10;
    float collision_radius;
    float newtonian = 10;
    //物質転送
    public GameObject loopHole;
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
            GameObject go = collision.gameObject;
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            float r = Vector2.Distance(go.transform.position, transform.position);
            float gravity = MASS / (r * r);
            Vector2 XYgravity = new Vector2(Mathf.Cos(GetAngle(go.transform.position,transform.position))*gravity, Mathf.Sin(GetAngle(go.transform.position, transform.position)) * gravity);
            rb.AddForce(XYgravity* newtonian);
            if (gravity > 10)
            {
                go.transform.position = loopHole.transform.position;
            }
        }
    }

    private float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);

        return rad;
    }
}
