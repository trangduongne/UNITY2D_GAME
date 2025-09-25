using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutScript : MonoBehaviour
{
    public GameObject panel1;
    public Button OK1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Map1")
        {
            panel1.SetActive(true);
            ManagerScene1.Instance.PauseGame();

            OK1.onClick.RemoveAllListeners();
            OK1.onClick.AddListener(() =>
            {
                ClosePanel(panel1);
            });
        }
        if (SceneManager.GetActiveScene().name == "Map2" || SceneManager.GetActiveScene().name == "Map3")
        {
            if (panel1 != null && panel1.activeSelf)
            {
                ClosePanel(panel1);
            }
            else
            {
                if (Time.timeScale == 0)
                    ManagerScene1.Instance.ResumeGame();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ClosePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
        ManagerScene1.Instance.ResumeGame();
    }
    public void ReviewGuideline()
    {
            panel1.SetActive(true);
        ManagerScene1.Instance.PauseGame();
        OK1.onClick.RemoveAllListeners();
        OK1.onClick.AddListener(() => ClosePanel(panel1));
    }
}
