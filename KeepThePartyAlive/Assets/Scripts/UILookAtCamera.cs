using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    public Canvas UICanvas;

    // Update is called once per frame
    void Update()
    {
        UICanvas.transform.LookAt(Camera.main.transform);
    }
}
