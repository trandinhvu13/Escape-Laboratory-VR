using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour
{

    [Header("Settings")]
    public int m_Force = 20;
    public int m_MaxProjectileCount = 20;
    public float m_ReloadTime = 1.5f;

    [Header("References")]
    public Transform m_Barrel = null;
    public GameObject m_ProjectilePrefab = null;

    private bool m_IsReloading = false;
    private int m_FiredCount = 0;

    private ProjectilePool m_ProjectilePool = null;

    private void Awake()
    {
        m_ProjectilePool = new ProjectilePool(m_ProjectilePrefab, m_MaxProjectileCount);
    }

    private void Start()
    {
        UpdateFiredCount(0);
    }

    private void Update()
    {
        if (m_IsReloading)
        {
            return;
        }

        if (m_FireAction.GetStateDown(m_Pose.inputSource))
        {
            m_Animator.SetBool("Fire", true);
            Fire();
        }

        if (m_ReloadAction.GetStateDown(m_Pose.inputSource))
        {
            StartCoroutine(Reload());
        }

    }

    private void Fire()
    {
        if (m_FiredCount >= m_MaxProjectileCount)
        {
            return;
        }
        Projectile targetProjectile = m_ProjectilePool.m_Projectiles[m_FiredCount];
        targetProjectile.Launch(this);

        UpdateFiredCount(m_FiredCount + 1);
    }

    private IEnumerator Reload()
    {
        if (m_FiredCount == 0)
        {
            yield break;
        }

        m_AmmoOutput.text = "-";

        m_ProjectilePool.SetAllProjectiles();

        yield return new WaitForSeconds(m_ReloadTime);

        UpdateFiredCount(0);
        m_IsReloading = false;
    }

    private void UpdateFiredCount(int newValue)
    {
        m_FiredCount = newValue;
        m_AmmoOutput.text = (m_MaxProjectileCount - m_FiredCount).ToString();
    }
}
