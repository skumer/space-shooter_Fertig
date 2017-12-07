using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour
{
    public float scrollSpeed;
    private float z;

    private Vector3 position;

    void Start()
    {
        z = 30;
        position = new Vector3(0, -10, z);
    }

    void Update()
    {
        transform.position = new Vector3(0, -10, z);
        z -= scrollSpeed;
        if (z < 0)
        {
            z = 30;
        }
    }
}