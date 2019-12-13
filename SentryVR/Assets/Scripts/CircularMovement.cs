using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    float timeCounter = 0;
    float speed;
    float width;
    float height;
    public Transform turretOrigin;
    

    // Start is called before the first frame update
    private void OnEnable()
    {
        speed = 0.5f;
        width = 9f;
        height = 9f;

    }
    void Start()
    {
        speed = 0.5f;
        width = 9f;
        height = 9f;
    }

    // Update is called once per frame
    void Update()
    {
        
            Circle();
        
    }
    void Circle()
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * width;
        float z = Mathf.Sin(timeCounter) * height;
        float y = 1;

        transform.position = new Vector3(x, y, z) + turretOrigin.position;
    }
}
