using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BomberController : Target
{
    public AudioSource explodeSound;
    public AudioSource finalSound;
    //Player
    public Player player;
    public GameObject Player;

    public Transform playerPos;
    public float lookRadius = 1f;
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
        Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.GetComponent<Player>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
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
            }else if (isDetected == true && !hit.collider.CompareTag("Player"))
            {
                StartCoroutine("KeppTrackTarget");
            }else
            {
                Wander();
            }

        }
        

    }

    private void Chase()
    {
        isDetected = true;

        agent.SetDestination(target.position);
        agent.speed = 3f;
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
            agent.isStopped = true;
            Debug.Log("Da dung");
            StartCoroutine("Explode");
        } else if (collision.gameObject.CompareTag("Projectile"))
        {
                Damage();
           
        }
        
    }
    protected IEnumerator KeppTrackTarget()
    {
        agent.SetDestination(target.position);
        Debug.Log("Still chase");
        Debug.Log("phat hien" + isDetected);
        WaitForSeconds wait = new WaitForSeconds(2.5f);
        yield return wait;
        isDetected = false;
        Debug.Log("phat hien2" + isDetected);
        Wander();
    }

    protected IEnumerator Explode()
    {
        explodeSound.Play();
        InvokeRepeating("Blink", 0f, 0.2f);
        WaitForSeconds wait = new WaitForSeconds(1.5f);
        yield return wait;
        
        float distanceToEnemy = Vector3.Distance(transform.position, playerPos.position);
        if (distanceToEnemy <= lookRadius)
        {
            Debug.Log("Damage Player");
            player.currentHealth  = player.currentHealth - 20;
            rend.enabled = false;
            finalSound.Play();
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Ko damage player");
            rend.enabled = false;
            finalSound.Play();
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }

        
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
