using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Target
{
    public Transform scoutPoint;

    public Transform player;

    public float range = 15f;

    public Transform partToRotate;

    //Meshrenderer
    public MeshRenderer ren;
    public GameObject turretBody;
    public Material[] mat;

    // Start is called before the first frame update
    void Start()
    {
        ResetValue();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Standby();
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

    private new void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Damage();

        }

    }

    void UpdateTarget()
    {

    }

    void Standby()
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

}
