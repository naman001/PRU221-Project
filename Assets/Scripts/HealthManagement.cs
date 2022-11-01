using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManagement : MonoBehaviour
{
    public static HealthManagement instance;
    public static int health;
    [SerializeField]
    Image[] hearts;
    [SerializeField]
    Sprite fullHeart;
    [SerializeField]
    Sprite empHeart;
    private void Awake()
    {
        health = PlayerPrefs.GetInt("HealthCount", health);
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = empHeart;
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }
}
