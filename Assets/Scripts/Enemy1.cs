using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float range = 1.5f;
    int direction = 1;
    float startYpoint;
    // Start is called before the first frame update
    void Start()
    {
        startYpoint = transform.position.y;
        speed = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.fixedDeltaTime * Vector2.down) ;
        if (transform.position.y > startYpoint || transform.position.y < startYpoint - range)
            direction *= -1;
    }
}
