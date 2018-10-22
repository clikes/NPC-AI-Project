using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Control the Enemy, some code is from the CharaterDemoController which not my originnal code
/// </summary>
public class EmemyController : MonoBehaviour {
    Animator animator;
    public GameObject floorPlane;
    float rotateSpeed = 20.0f; //used to smooth out turning

    public Vector3 movementTargetPosition;
    public Vector3 attackPos;
    public Vector3 lookAtPos;
    float gravity = 5.0f;

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
        movementTargetPosition = transform.position;//initializing our movement target as our current position
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
