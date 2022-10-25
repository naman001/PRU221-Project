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
    [SerializeField]
    private float fallMultiplier;
    [SerializeField]
    private float jumpTime;
    [SerializeField]
    private float jumpMultiplier;
    [SerializeField]
    private AudioSource collectSoundEffect;
    [SerializeField]
    private AudioSource hurtSoundEffect;
    [SerializeField]
    private AudioSource deadthSoundEffect;
    
    Vector2 vecGravity;
    private Animator anim;
    public GameObject gameObj;
    private SpriteRenderer sr;
    private Rigidbody2D mybody;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public TextMeshProUGUI coinsText;
    public static Vector3 lastCheckPointPos = new Vector3(-11, 1, 0);
    private readonly string running = "IsRunning";
    private readonly string jumping = "IsJumping";
    private readonly string groundTag = "Ground";
    public int map;
    public int health;
    public bool isGround;
    private bool isJumping;
    private float jumpCounter;
    public static int CoinNums;

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

        vecGravity = new Vector2(0f, -Physics2D.gravity.y);
    }

    private void Update()
    {
        coinsText.text = CoinNums.ToString();
        health = HealthManagement.health;
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        CharacterMovementKeyBoard();
        AnimateCharacter();
        Jump();
    }

    /// <summary>
    /// is method is used to check the condition to activate the animation
    /// </summary>
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

    /// <summary>
    /// this method is used to get the input when player pressed left or right arrow to move the character
    /// </summary>
    private void CharacterMovementKeyBoard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.fixedDeltaTime * moveForce;
        if (!Mathf.Approximately(0, movementX))
            transform.rotation = movementX > 0 ? Quaternion.Euler(0, 100, 0) : Quaternion.identity;
    }

    /// <summary>
    /// this method is used to set animation for character movement
    /// </summary>
    /// <param name="flip">turn the character body to left & right</param>
    private void SetMover(bool flip)
    {
        anim.SetBool(running, true);
        sr.flipX = flip;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    /// <summary>
    /// this method is used to set animation for character jump
    /// </summary>
    /// <param name="flip">turn the character body to left & right</param>
    private void SetJumper(bool flip)
    {
        anim.SetBool(jumping, true);
        sr.flipX = flip;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    /// <summary>
    /// this method is used to get the input when the player have pressed "jump" button
    /// </summary>
    void Jump()
    {
        //Mathf.Abs(mybody.velocity.y) < 0.001f this is used to get the absolute velocity value of vector y. It's the same with isGround check.
        if (Input.GetButtonDown("Jump") && isGround)
        {
            
            isGround = false;
            isJumping = true;
            jumpCounter = 0;
            mybody.velocity = new Vector2(mybody.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        if(mybody.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) 
                isJumping = false;
            mybody.velocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }
        if (mybody.velocity.y < 0)
        {
            mybody.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
    }

    /// <summary>
    /// this method is used to check collision between character with other game objects.
    /// </summary>
    /// <param name="collision"></param>
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

    /// <summary>
    /// this method will be activated when character collision with enemy
    /// </summary>
    /// <returns>the flashing effect and the character will be invisible for 3s</returns>
    IEnumerator GetHurt()
    {
        hurtSoundEffect.Play();
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
