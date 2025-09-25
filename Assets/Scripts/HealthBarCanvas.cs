using UnityEngine;
using UnityEngine.UI;

public class HealthBarCanvas : MonoBehaviour
{
    public Slider Health;
    public Slider Mana;
    public Transform player;
    void LateUpdate()
    {
        transform.position = player.position + new Vector3(1.25f, 1.5f, 0); 
        transform.rotation = Quaternion.identity;
        if (PlayerController.Instance.isDead)
        {
            Health.gameObject.SetActive(false);
            Mana.gameObject.SetActive(false) ;
        }
        else
        {
            Health.gameObject.SetActive(true);
            Mana.gameObject.SetActive(true);
        }
    }
    void Start()
    {
        Health.interactable = false;
        Mana.interactable = false;

    }
}
