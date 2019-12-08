using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BomberController : Target
{
    public float lookRadius = 7f;
    private Renderer rend = null;
    // Navmesh
    Transform target;
    NavMeshAgent agent;
    private float originalSpeed;
    private float distance;

    // Raycast
    public GameObject raycastOrigin;
    public float sightLength = 30f;
    public bool isDetected = false;


    void Start()
    {
        ResetValue();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();

        distance = Vector3.Distance(target.position, transform.position);
        originalSpeed = agent.speed;
        Debug.Log("Agent speed is " + originalSpeed);
    }


    void Update()
    {
        Looking();
    }

    public void ResetValue()
    {
        m_FlashDamageColor = Color.white;
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_OriginalColor = m_MeshRenderer.material.color;

        m_MaxHealth = 5;
        m_Health = 5;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void Looking()
    {
        RaycastHit hit;
        Vector3 fwd = raycastOrigin.transform.TransformDirection(Vector3.forward);
        Ray eyeRay = new Ray(raycastOrigin.transform.position, fwd);
        Debug.DrawRay(raycastOrigin.transform.position, fwd * sightLength);
        Wander();
        if (Physics.Raycast(eyeRay, out hit, sightLength))
        {

            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Toi da thay" + hit.collider.tag);
                Chase();
            }

        }
        else
        {
            isDetected = false;
            Wander();
        }

    }

    private void Chase()
    {
        isDetected = true;

        agent.SetDestination(target.position);
        agent.speed = 5f;
        Debug.Log("I have changed my speed");
        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
        }
    }

    public void Wander()
    {
        agent.speed = originalSpeed;
        float dist = GetComponent<NavMeshAgent>().remainingDistance;
        if (dist != Mathf.Infinity && GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete)//Arrived.
        {
            agent.SetDestination(RandomNavmeshLocation(30f));
        }

        Vector3 RandomNavmeshLocation(float radius)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                finalPosition = hit.position;
            }

            return finalPosition;
        }
    }

    private new void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("I collide player");
            StartCoroutine("Explode");
        } else if (collision.gameObject.CompareTag("Projectile"))
        {
                Damage();
           
        }
        
    }
    protected IEnumerator Explode()
    {
        InvokeRepeating("Blink", 0f, 0.2f);
        WaitForSeconds wait = new WaitForSeconds(2.0f);
        yield return wait;
        Destroy(gameObject);
    }

    private void Blink()
    {
        StartCoroutine("Blink1");
    }

    protected IEnumerator Blink1()
    {
        
        rend.enabled = false;
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        yield return wait;
        rend.enabled = true;
    }
}
