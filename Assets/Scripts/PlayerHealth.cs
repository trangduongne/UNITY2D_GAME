using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Slider Settings")]
    public float health = 100;
    private float maxHealth = 100;

    public float Mana = 100;
    private float maxMana = 100;

    public Slider HealthSlider;
    public Slider ManaSlider;

    public static PlayerHealth instance;
    public GameObject Player;

    public TextMeshProUGUI manatext;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (HealthSlider != null)
        {
            health = maxHealth;
            HealthSlider.maxValue = maxHealth;
            HealthSlider.value = health;
        }
        if( ManaSlider != null)
        {
            Mana = maxMana;
            ManaSlider.maxValue = maxMana;
            ManaSlider.value = Mana;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMana();
    }
    public void UpdateMana()
    {
        if (Mana < maxMana)
        {
            Mana += 5 * Time.deltaTime;
            ManaSlider.value = Mana;
        }
    }
    public bool UseMana(float number)
    {
        if (Mana >=10)
        {
            Mana -= 10;
            ManaSlider.value = Mana;
            return true;
        }
        else
        {
           Debug.Log("Not enough mana");
            manatext.text = "Không đủ Mana";
            StartCoroutine(ClearManaText());
            return false;
        }
    }
    IEnumerator ClearManaText()
    {
        yield return new WaitForSeconds(2f);
        manatext.text = "";
    }
    public bool UseMana2(float number)
    {
        if (Mana >=20)
        {
            Mana -= 20;
            ManaSlider.value = Mana;
            return true;
        }
        else
        {
            Debug.Log("Not enough mana");
            manatext.text = "Không đủ Mana";
            StartCoroutine(ClearManaText());
            return false;
        }    
    }    
    public bool UseMana3AndIncreaseBlood(float number)
    {
        if(Mana >=50)
        {
            Mana -=50;
            ManaSlider.value = Mana;
            health += 30;
            HealthSlider.value = health;
            return true;
        }
        else
        {
            Debug.Log("Not enough mana");
            manatext.text = "Không đủ Mana";
            StartCoroutine(ClearManaText());
            return false;
        }
    }  
    public void TakeDamage(float number)
    {
        if(health > 0)
        {
            health -= number;
            MusicManager.Instance.playSFX("hurt");

            HealthSlider.value = health;
        }
        if(health <= 0)
        {
            Player.GetComponent<PlayerController>()?.Die();
        }    
    }
    public void ResetHealth()
    {
        health = maxHealth;
        HealthSlider.value = health;
        Mana = maxMana;
        ManaSlider.value = maxMana;
    }
}
