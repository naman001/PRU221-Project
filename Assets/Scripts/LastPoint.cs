using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPoint : MonoBehaviour
{
    [SerializeField]
    GameObject finalScene;
    GameObject character;
    private Animator anim;
    private readonly string isOpening = "isOpening";
    private float delayTime = 1f;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "character")
        {
            anim.SetBool(isOpening, true);
            Character.lastCheckPointPos = transform.position;
            Invoke("CompleteMap", delayTime);
            character = GameObject.FindWithTag("character");
            character.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
;
        }
    }

    public void CompleteMap()
    {
        SoundManager.instance.GameMusic(false);
        SoundManager.instance.WinningMusic();
        finalScene.SetActive(true);
    }
}
