using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float movementX;
    [SerializeField]
    private float moveForce = 3f;
    [SerializeField]
    private float jumpForce = 11f;
    private Rigidbody2D mybody;
    private Animator anim;
    private SpriteRenderer sr;
    private readonly string running = "IsRunning";
    private readonly string jumping = "IsJumping";
    private readonly string groundTag = "Ground";
    private bool isGround;
    public static Vector3 lastCheckPointPos = new Vector3(-11, 1, 0);
    public static int CoinNums;
    public TextMeshProUGUI coinsText;
    public int map;
    public int health;
    public GameObject gameObj;
    private void Awake()
    {
        CoinNums = PlayerPrefs.GetInt("CoinsCount", 0);
        PlayerPrefs.SetInt("HealthCount", 3);
        mybody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        GameObject.FindGameObjectWithTag("character").transform.position = lastCheckPointPos;
        map = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
    }

    private void Update()
    {
        coinsText.text = CoinNums.ToString();
        health = HealthManagement.health;
    }

    void FixedUpdate()
    {
        CharacterMovementKeyBoard();
        AnimateCharacter();
        Jump();
    }

    private void AnimateCharacter()
    {
        if (isGround)
        {
            anim.SetBool(jumping, false);
            if (movementX > 0)
            {
                SetMover(false);
                if (Input.GetButtonDown("Jump"))
                {
                    anim.SetBool(running, false);
                    SetJumper(false);
                }
            }
            else if (movementX < 0)
            {
                SetMover(true);
                if (Input.GetButtonDown("Jump"))
                {
                    anim.SetBool(running, false);
                    SetJumper(true);
                }
            }
            else
            {
                anim.SetBool(running, false);
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
        else
        {
            anim.SetBool(jumping, true);
            if (movementX > 0)
                SetJumper(false);
            else if (movementX < 0)
                SetJumper(true);
        }
    }

    private void CharacterMovementKeyBoard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.fixedDeltaTime * moveForce;
        if (!Mathf.Approximately(0, movementX))
            transform.rotation = movementX > 0 ? Quaternion.Euler(0, 100, 0) : Quaternion.identity;
    }

    private void SetMover(bool flip)
    {
        anim.SetBool(running, true);
        sr.flipX = flip;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void SetJumper(bool flip)
    {
        anim.SetBool(jumping, true);
        sr.flipX = flip;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void Jump()
    {
        //Mathf.Abs(mybody.velocity.y) < 0.001f
        if (Input.GetButtonDown("Jump") && isGround)
        {
            isGround = false;
            mybody.velocity = new Vector2(mybody.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "enemy")
        {
            HealthManagement.health--;
            if(HealthManagement.health <= 0)
            {
                GameManagement.isGameOver = true;
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(GetHurt());
            }
        }

        if (collision.transform.tag == "water")
        {
            HealthManagement.health--;
            if (HealthManagement.health <= 0)
            {
                GameManagement.isGameOver = true;
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.transform.position = lastCheckPointPos;
                StartCoroutine(GetHurt());
            }
        }

        if (collision.gameObject.CompareTag(groundTag))
        {
            isGround = true;
        }
    }
    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
    
    /// <summary>
    /// this method is used to save game info into file then return home menu
    /// </summary>
    public void SaveCharacterInfo()
    {
        SaveSystem.SaveCharacter(this);
        var game = gameObj.GetComponent<GameManagement>();
        game.ReturnHome();
    }

    /// <summary>
    /// this method is for load saved game in main menu
    /// </summary>
    public void LoadCharacterInfo()
    {
        SaveData data = SaveSystem.LoadData();
        SceneManager.LoadScene(data.map);
        PlayerPrefs.SetInt("HealthCount", data.health);
        CoinNums = data.coins;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
    
    /// <summary>
    /// this method is for load new game in main menu
    /// </summary>
    public void CharacterNewGame()
    {
        health = 3;
        CoinNums = 0;
        PlayerPrefs.SetInt("CoinsCount", CoinNums);
        PlayerPrefs.SetInt("HealthCount", health);
        transform.position = new Vector3(-11, 1, 0);
    }
}
