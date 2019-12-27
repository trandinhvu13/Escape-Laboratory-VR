using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitYesButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Gamequit");
            Application.Quit();
        }
        else
        {
            return;
        }
    }
}
