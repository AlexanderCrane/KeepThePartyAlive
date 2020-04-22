using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpCamera : MonoBehaviour
{

    //NOTE: This script is not used. It was replaced with CameraFollow

    public GameObject player;
    // public Vector3 defaultDistance = new Vector3(0f, 2f, -10f);
    private Vector3 cameraOffset;
    // public float distanceDamp = 10f;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool lookAtPlayer = false;

    public bool rotateAroundPlayer = true;

    public float rotateSpeed = 0.5f;

    // Use this for initialization
    void Awake()
    {
        cameraOffset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if(rotateAroundPlayer){
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up);
            Quaternion camTurnAngleVertical = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotateSpeed, Vector3.up);

            cameraOffset = camTurnAngle * cameraOffset ;
            cameraOffset = camTurnAngleVertical * cameraOffset ;
        }
        // Transform targetTransform = player.transform;
        // Vector3 toPos = targetTransform.position + (defaultDistance);
        // Vector3 curPos = Vector3.Lerp(transform.position, toPos, distanceDamp * Time.deltaTime);
        //transform.position = camTurnAngle * cameraOffset;
        Vector3 newPos = player.transform.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, newPos, SmoothFactor);
        if(lookAtPlayer || rotateAroundPlayer){
            transform.LookAt(player.transform);
            // Quaternion newPlayerRot = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.z, this.transform.rotation.z);
            // player.transform.rotation = newPlayerRot;

        }
    }
}