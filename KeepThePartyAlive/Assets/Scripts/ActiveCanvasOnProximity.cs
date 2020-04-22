using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCanvasOnProximity : MonoBehaviour
{
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"){
            canvas.enabled = true;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Player"){
            canvas.enabled = false;
        }
    }
}
