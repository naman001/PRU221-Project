using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform character;
    private Vector3 tempPos;

    [SerializeField]
    private float minX, maxX;
    [SerializeField]
    private float minY, maxY;
    void Start()
    {
        character = GameObject.FindWithTag("character").transform;
    }

    void LateUpdate()
    {
        tempPos = transform.position;
        tempPos.x = character.position.x;
        if (tempPos.x < minX)
            tempPos.x = minX;
        if (tempPos.x > maxX)
            tempPos.x = maxX;

        tempPos.y = character.position.y;
        if (tempPos.y < minY)
            tempPos.y = minY;
        if (tempPos.y > maxY)
            tempPos.y = maxY;
        transform.position = tempPos;

        if (character.position == null || tempPos == null)
        {
            Application.Quit();
        }
    }
}