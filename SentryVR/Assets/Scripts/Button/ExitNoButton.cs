using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitNoButton : MonoBehaviour
{
    public GameObject panelExit;
    public GameObject panelMain;
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
            panelMain.SetActive(true);
            panelExit.SetActive(false);
        }
        else
        {
            return;
        }
    }
}
