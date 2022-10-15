using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    int direction = 1;

    public Transform rightPosCheck;
    public Transform leftPosCheck;

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime * direction);
        if (Physics2D.Raycast(rightPosCheck.position, Vector2.down, 2) ==  false || Physics2D.Raycast(leftPosCheck.position, Vector2.down, 2) == false)
            direction *= -1;
        /*if (Physics2D.Raycast(leftPosCheck.position, Vector2.down, 2) ==  false)
            direction = 1;*/

    }
}
