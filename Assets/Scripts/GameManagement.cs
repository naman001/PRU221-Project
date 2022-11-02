using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    public static bool isGameOver;
    [SerializeField]
    GameObject gameoverScene;
    [SerializeField]
    GameObject pauseScene;
    [SerializeField]
    GameObject pauseBtn;
    [SerializeField]
    GameObject finalScene;
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject selectMap;

    [SerializeField]
    private Sprite soundOnImg;
    [SerializeField]
    private Sprite soundOffImg;
    public Button muteButton;
    private bool isOn = true;

    private void Awake()
    {
        isGameOver = false;
        Time.timeScale = 1;
    }

    private void Start()
    {
        soundOnImg = muteButton.image.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameoverScene.SetActive(true);
            pauseBtn.SetActive(false);
        }
    }

    public void SelectMap()
    {
        SoundManager.instance.GameMusic(false);
        SoundManager.instance.MenuMusic(true);
        SceneManager.LoadScene(0);
        mainMenu.SetActive(false);
        selectMap.SetActive(true);
    }

    

    public void Restart()
    {
        SoundManager.instance.GameOverMusic(false);
        SoundManager.instance.GameMusic(true);
        gameoverScene.SetActive(false);
        pauseScene.SetActive(false);
        finalScene.SetActive(false);
        pauseBtn.SetActive(true);
        Character.lastCheckPointPos = new Vector2(-11, 1);
        Character.CoinNums = 0;
        PlayerPrefs.SetInt("CoinsCount", Character.CoinNums);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void NewGame()
    {
        SoundManager.instance.GameOverMusic(false);
        SoundManager.instance.MenuMusic(false);
        SoundManager.instance.GameMusic(true);
        Character.lastCheckPointPos = new Vector2(-11, 1);
        Character.CoinNums = 0;
        PlayerPrefs.SetInt("CoinsCount", Character.CoinNums);
    }

    public void Replay()
    {
        SoundManager.instance.GameOverMusic(false);
        SoundManager.instance.GameMusic(true);
        gameoverScene.SetActive(false);
        pauseScene.SetActive(false);
        pauseBtn.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScene.SetActive(true);
        pauseBtn.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScene.SetActive(false);
        pauseBtn.SetActive(true);
    }

    public void ReturnHome()
    {
        SoundManager.instance.GameOverMusic(false);
        SoundManager.instance.GameMusic(false);
        SoundManager.instance.MenuMusic(true);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ButtonClicked()
    {
        if (isOn)
        {
            muteButton.image.sprite = soundOffImg;
            isOn = false;
            SoundManager.instance.gameSource.mute = true;
            SoundManager.instance.jumpSource.mute = true;
            SoundManager.instance.hitSource.mute = true;
            SoundManager.instance.coinSource.mute = true;
            SoundManager.instance.deathSource.mute = true;
            SoundManager.instance.menuSource.mute = true;
            SoundManager.instance.gameOverSource.mute = true;
            SoundManager.instance.winningSource.mute = true;
            SoundManager.instance.winningSourceLoop.mute = true;
        }
        else
        {
            muteButton.image.sprite = soundOnImg;
            isOn = true;
            SoundManager.instance.gameSource.mute = false;
            SoundManager.instance.jumpSource.mute = false;
            SoundManager.instance.hitSource.mute = false;
            SoundManager.instance.coinSource.mute = false;
            SoundManager.instance.deathSource.mute = false;
            SoundManager.instance.menuSource.mute = false;
            SoundManager.instance.gameOverSource.mute = false;
            SoundManager.instance.winningSource.mute = false;
            SoundManager.instance.winningSourceLoop.mute = false;
        }
    }
}
