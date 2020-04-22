using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeMove : MonoBehaviour
{

    public HingeJoint hj;
    public Transform transform;
    public bool inverted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JointSpring js = hj.spring;
        js.targetPosition = transform.localEulerAngles.x;
        if(js.targetPosition > 180){
            js.targetPosition = js.targetPosition - 360;
        }
        js.targetPosition = Mathf.Clamp(js.targetPosition, hj.limits.min + 5, hj.limits.max - 5);
        if(inverted){
            js.targetPosition = js.targetPosition *= -1;
        }
        hj.spring = js;
    }
}
