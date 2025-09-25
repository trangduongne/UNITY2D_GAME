using UnityEngine;
using UnityEngine.UI;

public class BeastGhostHealthCanvas : MonoBehaviour
{
    public Slider Health;
    public Transform BeastGhost;
    void Awake()
    {

    }
    void LateUpdate()
    {
        transform.position = BeastGhost.position + new Vector3(3.3f, -0.3f, 0);
        transform.rotation = Quaternion.identity;
    }
    void Start()
    {
        Health.interactable = false;

    }


}
