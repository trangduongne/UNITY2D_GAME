﻿using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WarningZone : MonoBehaviour
{
    public float warningDuration = 1.5f;
    void OnEnable()
    {
        Invoke("TriggerAttack", warningDuration);
    }

    void TriggerAttack()
    {
        GameObject BeastGhostBullet = BulletPool.Instance.GetBeastGhostBullet();
        BeastGhostBullet.transform.position = transform.position;
        BeastGhostBullet.GetComponent<BeastGhostBullet>().SetDirection(Vector3.up);
        gameObject.SetActive(false); 
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
