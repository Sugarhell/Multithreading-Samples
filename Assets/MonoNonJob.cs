using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoNonJob : MonoBehaviour {
    public Vector3 acceleration = new Vector3(0.0004f, 0.0003f, 0.0004f);
    public Vector3 accelerationM = new Vector3(0.0002f, 0.0002f, 0.0002f);
    Vector3 Velocity;
	
	// Update is called once per frame
	void Update () {
        //Velocity = transform.position;
        Velocity += acceleration + Random.Range(10, 50) * accelerationM;
        transform.position += Velocity * Time.deltaTime;
	}
}
