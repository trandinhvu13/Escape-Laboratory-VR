using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Color m_FlashDamageColor = Color.white;

    protected MeshRenderer m_MeshRenderer = null;

    protected Color m_OriginalColor = Color.white;

   
    public int m_MaxHealth;
    
    public int m_Health;

    public GameObject mainObject;

    protected void Awake()
    {
        m_MeshRenderer = mainObject.GetComponent<MeshRenderer>();
        m_OriginalColor = m_MeshRenderer.material.color;
        
    }

    protected void OnEnable()
    {
        m_Health = m_MaxHealth;
        ResetHealth();
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Damage();
        }
    }

    protected void Damage()
    {
        StopAllCoroutines();
        StartCoroutine(Flash());
        RemoveHealth();

    }

    protected virtual IEnumerator Flash()
    {
        m_MeshRenderer.material.color = m_FlashDamageColor;

        WaitForSeconds wait = new WaitForSeconds(0.1f);
        yield return wait;

        m_MeshRenderer.material.color = m_OriginalColor;
    }

    protected void RemoveHealth()
    {
        m_Health--;
        CheckForDeath();
    }

    protected void ResetHealth()
    {
        m_Health = m_MaxHealth;

    }

    protected void CheckForDeath()
    {
        if (m_Health <= 0)
        {
            Kill();
        }
    }

    protected void Kill()
    {
        gameObject.SetActive(false);
    }
}
