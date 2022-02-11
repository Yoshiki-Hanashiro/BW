using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHcontroller : MonoBehaviour
{
    //�u���b�N�z�[���̏d�́@g=M/r^2 (�f���̏d����BH����̋�����2��Ŋ���)
    private float MASS = 10;
    float collision_radius;
    float gravityCoef = 10;
    /*
     �~�`�̓����蔻������B�i�͈͂́A�d�͂̉e�����ق�0�ɂȂ�ꏊ�܂ŁB�j
    �͈͂ɓ��������I�I�u�W�F�N�g�����X�g�����A�S�ĂɌW��d�͂��v�Z
    �d�͂�X�����AY�����ɕ����B
    Addforce����B
     */
    // Start is called before the first frame update
    void Start()
    {
        float radius_break = MASS / 0.01f;//�~�`�̓����蔻��̔��a�����肷��B�d�͂̉e�����\���ɏ������Ȃ�܂�
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
