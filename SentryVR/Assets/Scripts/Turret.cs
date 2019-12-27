using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Target
{
    public AudioSource shootSound;
    // Basic Movement
    [Header("Rotate")]
    public CircularMovement followpoint;
    public Transform scoutPoint;
    public Transform player;
    public float range = 15f;
    public Transform partToRotate;
    public bool isDetected = false;

    // Shoot manager
    [Header("Shoot")]
    public float moveForce;
    public GameObject bullet;
    public Transform gun;
    public float shootRate;
    public float shootForce;
    private float m_shootRateTimeStamp;

    [Header("Raycast")]
    //Raycast
    public GameObject raycastOrigin;
    private float sightLength = 15f;

    //Meshrenderer
    [Header("Renderer")]
    public MeshRenderer ren;
    public GameObject turretBody;
    public Material[] mat;

    // Start is called before the first frame update
    void Start()
    {
        ResetValue();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }
    public void ResetValue()
    {
        m_MaxHealth = 20;
        m_Health = 20;

        m_FlashDamageColor = Color.white;
        m_MeshRenderer = ren;
        ren = mainObject.GetComponent<MeshRenderer>();
        mat = ren.materials;
        m_OriginalColor = mat[1].color;


    }
    protected override IEnumerator Flash()
    {
        mat[1].color = m_FlashDamageColor;

        WaitForSeconds wait = new WaitForSeconds(0.1f);
        yield return wait;

        mat[1].color = m_OriginalColor;
    }

    


    public void Standby()
    {
        
        Vector3 dir = scoutPoint.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 turretRotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * 13f).eulerAngles;

        partToRotate.rotation = Quaternion.Euler(0f, turretRotation.y, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void DetectPlayer()
    {
        RaycastHit hit;
        Vector3 fwd = raycastOrigin.transform.TransformDirection(Vector3.forward);
        Ray eyeRay = new Ray(raycastOrigin.transform.position, fwd);
        Debug.DrawRay(raycastOrigin.transform.position, fwd * sightLength);
        
        if (Physics.Raycast(eyeRay, out hit, sightLength)&& hit.collider.CompareTag("Player"))
        {
            followpoint.gameObject.SetActive(false);
            scoutPoint = hit.collider.gameObject.transform;
            Shoot();
        }
        else
        {
            followpoint.gameObject.SetActive(true);
            scoutPoint = gameObject.transform.Find("StandbyPoint");
            Standby();
        }
    }

    private void Shoot()
    {
        
        Standby();
        if (Time.time > m_shootRateTimeStamp)
        {
            shootSound.Play();
            GameObject go = (GameObject)Instantiate(bullet, gun.position, gun.rotation);

            go.GetComponent<Rigidbody>().AddForce(gun.forward * shootForce);
            m_shootRateTimeStamp = Time.time + shootRate;
        }
    }
}
