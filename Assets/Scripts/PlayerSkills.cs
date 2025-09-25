using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public static PlayerSkills instance;
    public Transform firePoint;
    public Transform healPlayer;
    public void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Skill1( System.Action updateAniSkill1)
    {
        if (PlayerHealth.instance.UseMana(10))
        {
            updateAniSkill1();
            Shoot1();
            MusicManager.Instance.playSFX("shoot1");
        }
        Debug.Log("Skill 1 activated");        
    }
    void Shoot1()
    {
        GameObject bullet = BulletPool.Instance.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        Vector3 shootDirection = transform.right * Mathf.Sign(transform.localScale.x);
        bullet.GetComponent<BulletScript>().SetDirection(shootDirection);
    }
    public void Skill2 (System.Action updateAniSkill2)
    {
        if (PlayerHealth.instance.UseMana2(20))
        {
            updateAniSkill2();
            StartCoroutine(Shoot2());
            MusicManager.Instance.playSFX("shoot2");
        }
        Debug.Log("Skill 2 activated");
    }    
    IEnumerator Shoot2()
    {
        yield return new WaitForSeconds(0.35f);
        GameObject bullet2 = BulletPool.Instance.GetBullet2();
        bullet2.transform.position = firePoint.position;
        bullet2.transform.rotation = firePoint.rotation;
        Vector3 shootDirection = transform.right * Mathf.Sign(transform.localScale.x);
        bullet2.GetComponent<BulletScript2>().SetDirection(shootDirection);
    }    
    public void SkillHeal()
    {
        if (PlayerHealth.instance.UseMana3AndIncreaseBlood(50))
        {
            MusicManager.Instance.playSFX("heal");
            GameObject heal = BulletPool.Instance.GetHeal();
            heal.transform.SetParent(healPlayer);
            heal.transform.localPosition = Vector3.zero;
            heal.SetActive(true);
            Debug.Log("Skill heal activated");
        }

    }    
}
