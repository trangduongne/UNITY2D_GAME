using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene1 : MonoBehaviour
{
    public static ManagerScene1 Instance;

    public int killCount = 0;
    public int killTarget = 20;

    public TextMeshProUGUI killnumber;
    public TextMeshProUGUI numberoflives;
    public GameObject gameOverPanel;
    public GameObject pauseMenuUI;
    public GameObject gameWinPanel;
    public bool isPaused = true;
    float maxLives;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        MusicManager.Instance.PlayMusic("BackgroundGameMusic");
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "GameStart":
                return;
            case "Map1":
                PlayerController.Instance.life = 3;
                break;
            case "Map2":
                PlayerController.Instance.life = 6;
                break;
            case "Map3":
                PlayerController.Instance.life = 9;
                break;
            default:
                PlayerController.Instance.life = 3; 
                break;
        }

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (gameWinPanel != null) gameWinPanel.SetActive(false);
        maxLives = PlayerController.Instance.life;
        UpdateLives();
    }
    
    public void AddKill()
    {
        killCount++;
        Debug.Log("Kill Count: " + killCount);

        if (killCount >= killTarget)
        {
            NextLevel();
        }
        if (killnumber != null)
        {
            killnumber.text = "Sống lượng quái vật đã tiêu diệt: " + killCount.ToString() + "/" + killTarget.ToString();
        }
    }
    public void UpdateLives()
    {
        if (numberoflives != null)
        {
            numberoflives.text = "Số mạng sống còn lại: " + PlayerController.Instance.life.ToString();
        }

        if (PlayerController.Instance.life <= 0)
        {
            TriggerGameOver();
        }
    }
    public void PlayNew()
    {
        SceneManager.LoadScene("CutScene1");

    }

    void NextLevel()
    {
        SceneManager.LoadScene("CutScene2");
    }
    public void NextLevel_BossFight()
    {
        SceneManager.LoadScene("CutScene3");
    }
    public void TriggerGameOver()
    {
        MusicManager.Instance.playSFX("gameover");
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayAgain()
    {
        Time.timeScale = 1; 
        killCount = 0;      
        SceneManager.LoadScene("Map1");
    }
    public void PauseGame()
            {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
    }
    public void GameWin()
    {
        gameWinPanel.SetActive(true);
        StartCoroutine(waitForVictory());
    }
    IEnumerator waitForVictory()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("WinScene");
    }
}
