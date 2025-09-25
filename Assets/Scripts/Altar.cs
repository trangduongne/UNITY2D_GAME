using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class Altar : MonoBehaviour
{
    [Header("Thời gian cần thiết")]
    public float requiredTime = 30f;
    private float timer = 0f;
    private bool playerInside = false;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("Hiệu ứng màu")]
    public SpriteRenderer altarRenderer;
    private Color startColor = new Color32(0x39, 0x0B, 0x3B, 255); // #390B3B
    private Color targetColor = new Color32(0xF5, 0x59, 0xFF, 255); // #F559FF
    public float colorLerpSpeed = 2f;

    [Header("Ánh sáng")]
    public Light2D altarLight;
    public float defaultIntensity = 0.5f;
    public float activeIntensity = 1.5f;

    void Start()
    {
        altarRenderer.color = startColor;
        if (altarLight != null)
            altarLight.intensity = defaultIntensity;

        timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        Color target = playerInside ? targetColor : startColor;
        altarRenderer.color = Color.Lerp(altarRenderer.color, target, Time.deltaTime * colorLerpSpeed);

        if (altarLight != null)
        {
            float targetIntensity = playerInside ? activeIntensity : defaultIntensity;
            altarLight.intensity = Mathf.Lerp(altarLight.intensity, targetIntensity, Time.deltaTime * colorLerpSpeed);
        }

        if (playerInside)
        {
            timer += Time.deltaTime;
            if (timer >= requiredTime)
            {
                ManagerScene1.Instance.NextLevel_BossFight();
            }
        }
        else
        {
            timer = 0f;
        }

        float remaining = Mathf.Clamp(requiredTime - timer, 0f, requiredTime);
        timerText.text = "Thời gian còn lại: " + remaining.ToString("F2") + " giây";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            timerText.gameObject.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            timerText.gameObject.SetActive(false);

        }
    }
}
