using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover_Hazard : MonoBehaviour {

    public float minSpeed;
    public float maxSpeed;


    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * Random.Range(minSpeed, maxSpeed);
    }
}
