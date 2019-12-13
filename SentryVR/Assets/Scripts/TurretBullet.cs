using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public Player player;
    public GameObject Player;
    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.GetComponent<Player>();
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.currentHealth -= 20;
        }
    }
}
