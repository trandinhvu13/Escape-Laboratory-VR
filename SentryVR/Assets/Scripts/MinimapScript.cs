using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public Transform player;
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
