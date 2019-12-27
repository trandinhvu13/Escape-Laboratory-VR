using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class Blaster : MonoBehaviour
{
    
    public AudioSource laserShootSource;
    public AudioSource reload;

    public SceneManagement sceneManagement;
    [Header("Input")]
    public SteamVR_Action_Boolean m_FireAction = null;
    public SteamVR_Action_Boolean m_ReloadAction = null;
    public SteamVR_Action_Boolean m_PauseMenu = null;
    public SteamVR_Action_Boolean m_Exit = null;
    public GameObject pauseMenu;
    public bool pauseMenuOn = false;

    [Header("Settings")]
    public int m_Force = 2000;
    public int m_MaxProjectileCount = 6;
    public float m_ReloadTime = 1f;

    [Header("References")]
    public Transform m_Barrel = null;
    public GameObject m_ProjectilePrefab = null;
    public Text m_AmmoOutput = null;

    private bool m_IsReloading = false;
    private int m_FiredCount = 0;


    private SteamVR_Behaviour_Pose m_Pose = null;
    private Animator m_Animator = null;
    private ProjectilePool m_ProjectilePool = null;

    private void Awake()
    {
        m_Pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        m_Animator = GetComponent<Animator>();

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

        if (m_FireAction.GetStateUp(m_Pose.inputSource))
        {
            m_Animator.SetBool("Fire", false);

        }

        if (m_ReloadAction.GetStateDown(m_Pose.inputSource))
        {
            StartCoroutine(Reload());
        }

        if (m_PauseMenu.GetStateDown(m_Pose.inputSource))
        {
            Debug.Log("I press menu");

            if (pauseMenuOn == false)
            {
                sceneManagement.Pause();
                pauseMenu.SetActive(true);
                pauseMenuOn = true;
            }
            else
            {
                sceneManagement.Resume();
                pauseMenu.SetActive(false);
                pauseMenuOn = false;

            }
        }


        if (m_Exit.GetStateDown(m_Pose.inputSource))
        {
            if (pauseMenuOn == true)
            {
                sceneManagement.ReloadScene();
            }
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
        laserShootSource.Play();
    }

    private IEnumerator Reload()
    {
        if (m_FiredCount == 0)
        {
            yield break;
        }

        m_AmmoOutput.text = "-";

        m_ProjectilePool.SetAllProjectiles();
        reload.Play();
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
