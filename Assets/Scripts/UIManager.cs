using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject shining;
    public TMPro.TMP_Text titleText;
    private float normalAlpha = 1f;  
    private float hoverAlpha = 0.5f;  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (titleText == null)
            titleText = GetComponent<TMP_Text>();
        SetAlpha(hoverAlpha);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        shining.SetActive(true);
        SetAlpha(normalAlpha);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetAlpha(hoverAlpha);
        shining.SetActive(false);
    }

    void SetAlpha(float a)
    {
        Color c = titleText.color;
        c.a = a;
        titleText.color = c;
    }

}
