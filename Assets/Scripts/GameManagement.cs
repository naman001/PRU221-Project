using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameoverScene;
    public GameObject pauseScene;
    public GameObject pauseBtn;
    private void Awake()
    {
        isGameOver = false;
        Time.timeScale = 1;
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

    public void Restart()
    {
        SoundManager.instance.GameMusic(true);
        gameoverScene.SetActive(false);
        pauseScene.SetActive(false);
        pauseBtn.SetActive(true);
        Character.lastCheckPointPos = new Vector2(-11, 1);
        Character.CoinNums = 0;
        PlayerPrefs.SetInt("CoinsCount", Character.CoinNums);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void NewGame()
    {
        SoundManager.instance.MenuMusic(false);
        SoundManager.instance.GameMusic(true);
        Character.lastCheckPointPos = new Vector2(-11, 1);
        Character.CoinNums = 0;
        PlayerPrefs.SetInt("CoinsCount", Character.CoinNums);
    }

    public void Replay()
    {
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

    //co the them gameobject giong pauseScreen de setactive trong menu scene
    public void ReturnHome()
    {
        SoundManager.instance.GameMusic(false);
        SoundManager.instance.MenuMusic(true);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
