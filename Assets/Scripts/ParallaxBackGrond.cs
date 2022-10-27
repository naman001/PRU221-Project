using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGrond : MonoBehaviour
{
    public Transform mainCam;
    public Transform middleBg;
    public Transform asideBg;
    public float bgLength = 20.47f;
    // Update is called once per frame
    void Update()
    {
        if (mainCam.position.x > middleBg.position.x)
            asideBg.position = middleBg.position + Vector3.right * bgLength;
        if (mainCam.position.x < middleBg.position.x)
            asideBg.position = middleBg.position + Vector3.left * bgLength;
        if (mainCam.position.x > asideBg.position.x)
        {
            Vector3 x = middleBg.position;
            middleBg.position = asideBg.position;
            asideBg.position = x;
        }

    }
}
