using System.Collections;
using UnityEngine;

public class CheckpointGlow : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color dimColor = new Color(1f, 1f, 1f, 0.3f); 
    public Color glowColor = new Color(1f, 1f, 0.5f, 1f); 

    public float glowDuration = 1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = dimColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn.instance.SetCheckpoint(transform.position);
            StartCoroutine(GlowEffect());
        }
    }

    IEnumerator GlowEffect()
    {
        spriteRenderer.color = glowColor;
        yield return new WaitForSeconds(glowDuration);
        spriteRenderer.color = dimColor;
    }
}
