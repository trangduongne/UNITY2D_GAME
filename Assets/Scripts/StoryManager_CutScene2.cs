using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager_CutScene2 : MonoBehaviour
{
    public GameObject NPC;
    public GameObject Content;
    public GameObject Title;
    public GameObject BG;

    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject TextBox;
    [SerializeField] GameObject NextBttn;
    [SerializeField] int eventPos = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(EventStarter());
        NextBttn.GetComponent<Button>().onClick.AddListener(NextButton);
    }

    // Update is called once per frame
    void Update()
    {
        textLength = TextCreator.charCount;
    }
    IEnumerator EventStarter()
    {
        BG.SetActive(true);
        yield return new WaitForSeconds(3);
        NPC.SetActive(false);
        Content.SetActive(true);
        TextBox.SetActive(true);
        Title.GetComponent<TMPro.TextMeshProUGUI>().text = "Dẫn truyện";
        textToSpeak = "Sân huấn luyện trong cung. Những thí sinh vượt qua vòng 1 đang tập trung. Không khí căng thẳng nhưng đầy háo hức.";
        MusicManager.Instance.PlayMusic("storybackgroundmusic");
        Content.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        NextBttn.SetActive(true);
        eventPos = 1;
    }

    IEnumerator EventOne()
    {
        NextBttn.SetActive(false);
        Title.GetComponent<TMPro.TextMeshProUGUI>().text = "Dẫn truyện";
        textToSpeak = "Một viên quan phụ trách bước lên bục, giọng dõng dạc vang vọng khắp nơi.";
        Content.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        NextBttn.SetActive(true);
        eventPos = 2;
    }
    public void NextButton()
    {
        if (eventPos == 1)
        {
            StartCoroutine(EventOne());
        }
        else if (eventPos == 2)
        {
            StartCoroutine(EventTwo());
        }
        else if (eventPos == 3)
        {
            StartCoroutine(EventThree());
        }
        else if (eventPos == 4)
        {
            StartCoroutine(EventFour());
        }
        else if (eventPos == 5)
        {
            StartCoroutine(EventFive());
        }
        else if (eventPos == 6)
        {
            StartCoroutine(EventSix());
        }
        else if (eventPos == 7)
        {
            StartCoroutine(EventSeven());
        }
        else if (eventPos == 8)
        {
            StartCoroutine(EventEight());
        }
    }
    IEnumerator EventTwo()
    {
        NPC.SetActive(true);
        NextBttn.SetActive(false);
        Title.GetComponent<TMPro.TextMeshProUGUI>().text = "Sĩ quan phụ trách";
        textToSpeak = "Các ngươi đã vượt qua vòng tuyển chọn đầu tiên. Nhưng từ đây trở đi, thử thách sẽ không còn đơn giản chỉ là sức mạnh hay kỹ năng.";
        Content.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        NextBttn.SetActive(true);
        eventPos = 3;
    }
    IEnumerator EventThree()
    {
        NPC.SetActive(true);
        NextBttn.SetActive(false);
        Title.GetComponent<TMPro.TextMeshProUGUI>().text = "Sĩ quan phụ trách";
        textToSpeak = "Vòng 2 mang tên Đột Kích Hang Dơi. Nhiệm vụ của các ngươi là tiến vào hang tối, tìm đến tế đàn cổ xưa, và đứng vững trong đó suốt 30 giây.";
        Content.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        NextBttn.SetActive(true);
        eventPos = 4;
    }
    IEnumerator EventFour()
    {
        NPC.SetActive(true);
        NextBttn.SetActive(false);
        Title.GetComponent<TMPro.TextMeshProUGUI>().text = "Sĩ quan phụ trách";
        textToSpeak = "Hãy nhớ! Trong chiến trường thực sự, chiến đấu không phải lúc nào cũng là cách duy nhất. Đôi khi, quan trọng hơn là hoàn thành mục tiêu.";
        Content.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        NextBttn.SetActive(true);
        eventPos = 5;
    }
    IEnumerator EventFive()
    {
        NPC.SetActive(true);
        NextBttn.SetActive(false);
        Title.GetComponent<TMPro.TextMeshProUGUI>().text = "Sĩ quan phụ trách";
        textToSpeak = "Ngươi có thể tránh né, che giấu, hoặc phối hợp – miễn là sống sót và đạt được yêu cầu.";
        MusicManager.Instance.playSFX("drumbattle");
        Content.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        NextBttn.SetActive(true);
        eventPos = 6;
    }
    IEnumerator EventSix()
    {
        NPC.SetActive(true);
        NextBttn.SetActive(false);
        Title.GetComponent<TMPro.TextMeshProUGUI>().text = "Sĩ quan phụ trách";
        textToSpeak = "Hang dơi kia… sẽ là nơi kiểm chứng sự kiên trì, mưu trí và lòng dũng cảm của các ngươi. ";
        MusicManager.Instance.playSFX("drumbattle");
        Content.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        NextBttn.SetActive(true);
        eventPos = 7;
    }
    IEnumerator EventSeven()
    {
        NPC.SetActive(true);
        NextBttn.SetActive(false);
        Title.GetComponent<TMPro.TextMeshProUGUI>().text = "Sĩ quan phụ trách";
        textToSpeak = "Bước vào đi, và chứng minh rằng mình xứng đáng với danh hiệu Hộ Vệ Hoàng Gia.";
        MusicManager.Instance.playSFX("drumbattle");
        Content.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        NextBttn.SetActive(true);
        eventPos = 8;
    }
    IEnumerator EventEight()
    {
        NextBttn.SetActive(false);
        SceneManager.LoadScene("Map2");
        yield return new WaitForSeconds(2f);
    }


}
