using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BWCharaInitialize : MonoBehaviour
{
    //振袖部分の布表現
    public GameObject[] rightArmCloth;
    public GameObject[] leftArmCloth;
    public GameObject rightUpArm;
    public GameObject leftUpArm;

    //裾部分の布表現
    public GameObject[] frontHem;
    public GameObject[] backHem;
    // Start is called before the first frame update
    void Start()
    {
        for(int armCount = 0; armCount < rightArmCloth.Length; armCount++)
        {
            if(armCount == rightArmCloth.Length - 1)
            {
                Rigidbody2D rigidbody = rightArmCloth[armCount].AddComponent<Rigidbody2D>();
                for(int i = rightArmCloth.Length - 2; i >= 0; i--)
                {

                }
                break;
            }
            FixedJoint2D fixedjoint = rightArmCloth[armCount].AddComponent<FixedJoint2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
