using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHcontroller : MonoBehaviour
{
    //�W�����K���z�Ńz���C�g�z�[���̔��d�͂����߂�
    public float sigma = 0.5f;//������Ɛ��
    public float newtonian = 500f;//�グ��Ƌ����Ȃ�
    // Start is called before the first frame update
    void Start()
    {

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
            float gravity = -(newtonian / (Mathf.Sqrt(2 * Mathf.PI) * sigma)) * Mathf.Exp(-Mathf.Pow(r, 2) / (2 * sigma * sigma));
            Vector2 XYgravity = new Vector2(Mathf.Cos(GetAngle(go.transform.position, transform.position)) * gravity, Mathf.Sin(GetAngle(go.transform.position, transform.position)) * gravity);
            rb.AddForce(XYgravity);
        }
    }

    private float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);

        return rad;
    }
}
