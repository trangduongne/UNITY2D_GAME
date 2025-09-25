using UnityEngine;
using UnityEngine.UI;

public class BatHealthCanvas : MonoBehaviour
{
    public Slider Health;
    public Transform Bat;
    void Awake()
    {

    }
    void LateUpdate()
    {
        transform.position = Bat.position + new Vector3(1.2f, 0f, 0);
        transform.rotation = Quaternion.identity;
    }
    void Start()
    {
        Health.interactable = false;

    }


}
