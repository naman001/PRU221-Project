using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManagement : MonoBehaviour
{
    public static int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite empHeart;
    private void Awake()
    {
        health= PlayerPrefs.GetInt("HealthCount", health);
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
