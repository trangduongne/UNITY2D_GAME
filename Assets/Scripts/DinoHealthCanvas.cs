using UnityEngine;
using UnityEngine.UI;

public class DinoHealthCanvas : MonoBehaviour
{
    public Slider Health;
    public Transform Dino;
    void Awake()
    {

    }
    void LateUpdate()
    {
        transform.position = Dino.position + new Vector3(3.2f, 1.2f, 0);
        transform.rotation = Quaternion.identity;
    }
    void Start()
    {
        Health.interactable = false;

    }


}
