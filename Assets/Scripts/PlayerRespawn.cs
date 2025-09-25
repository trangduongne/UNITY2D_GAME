using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public static PlayerRespawn instance;
    private Vector3 currentCheckpoint;

    void Awake()
    {
        instance = this;
        currentCheckpoint = transform.position; 
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint;
        gameObject.SetActive(true);
        PlayerHealth.instance.ResetHealth();
    }
    public Vector3 GetCheckpoint()
    {
        return currentCheckpoint;
    }
}
