using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "character")
        {
            Character.CoinNums++;
            PlayerPrefs.SetInt("CoinsCount", Character.CoinNums);
            Destroy(gameObject);
        }
    }
}
