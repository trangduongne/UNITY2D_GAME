using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownSkill : MonoBehaviour
{    
    public Button skillButton1;
    private float cooldownTimer;
    public float cooldownTime = 5f;
    public Image cooldownOverlay;
    public TextMeshProUGUI cooldownText;

    public Button skillButton2;
    private float cooldownTimer2;
    public float cooldownTime2 = 8f;
    public Image cooldownOverlay2;
    public TextMeshProUGUI cooldownText2;

    public Button skillButton3;
    private float cooldownTimer3;
    public float cooldownTime3 = 15f;
    public Image cooldownOverlay3;
    public TextMeshProUGUI cooldownText3;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        skillButton1.onClick.AddListener(ActivateSkill);
        cooldownOverlay.fillAmount = 0f;
        cooldownText.text = "";

        skillButton2.onClick.AddListener(ActivateSkill2);
        cooldownOverlay2.fillAmount = 0f;
        cooldownText2.text = "";

        skillButton3.onClick.AddListener(ActivateSkill3);
        cooldownOverlay3.fillAmount = 0f;
        cooldownText3.text = "";
    }
    void ActivateSkill()
    {
        Debug.Log("Skill 1 được kích hoạt!");

        skillButton1.interactable = false;
        cooldownTimer = cooldownTime;
    }
    void ActivateSkill2()
    {
        Debug.Log("Skill 2 được kích hoạt!");

        skillButton2.interactable = false;
        cooldownTimer2 = cooldownTime2;
    }

    void ActivateSkill3()
    {
        Debug.Log("Skill 3 được kích hoạt!");

        skillButton3.interactable = false;
        cooldownTimer3 = cooldownTime3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!skillButton1.interactable)
        {
            cooldownTimer -= Time.deltaTime;

            cooldownOverlay.fillAmount = cooldownTimer / cooldownTime;
            cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();

            if (cooldownTimer <= 0f)
            {
                skillButton1.interactable = true;
                cooldownOverlay.fillAmount = 0f;
                cooldownText.text = "";
            }
        }
        if (!skillButton2.interactable)
        {
            cooldownTimer2 -= Time.deltaTime;

            cooldownOverlay2.fillAmount = cooldownTimer2 / cooldownTime2;
            cooldownText2.text = Mathf.Ceil(cooldownTimer2).ToString();

            if (cooldownTimer2 <= 0f)
            {
                skillButton2.interactable = true;
                cooldownOverlay2.fillAmount = 0f;
                cooldownText2.text = "";
            }
        }
        if (!skillButton3.interactable)
        {
            cooldownTimer3 -= Time.deltaTime;

            cooldownOverlay3.fillAmount = cooldownTimer3 / cooldownTime2;
            cooldownText3.text = Mathf.Ceil(cooldownTimer3).ToString();

            if (cooldownTimer3 <= 0f)
            {
                skillButton3.interactable = true;
                cooldownOverlay3.fillAmount = 0f;
                cooldownText3.text = "";
            }
        }
    }
}
