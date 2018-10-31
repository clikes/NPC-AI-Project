using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Control the Enemy, some code is from the CharaterDemoController which not my originnal code
/// </summary>
public class EmemyController : MonoBehaviour {
    Animator animator;
    public GameObject floorPlane;
    float rotateSpeed = 20.0f; //used to smooth out turning
    float[] randommove = {0, 0};
    public Vector3 movementTargetPosition;
    public Vector3 attackPos;
    public Vector3 lookAtPos;

    float gravity = 5.0f;

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator> ();
        movementTargetPosition = transform.position; //initializing our movement target as our current position
    }

    /// <summary>
    /// OnCollisionStay is called once per frame for every collider/rigidbody
    /// that is touching rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionStay (Collision other) {
        Debug.Log (other);
    }

    private void OnCollisionEnter (Collision other) {
        Debug.Log (other);
    }

    /// <summary>
    /// OnControllerColliderHit is called when the controller hits a
    /// collider while performing a Move.
    /// </summary>
    /// <param name="hit">The ControllerColliderHit data associated with this collision.</param>
    void OnControllerColliderHit (ControllerColliderHit hit) {

    }

    float[] GetRandomPosition(){
        System.Random random  = new System.Random();
        float x = (float)random.NextDouble();
        float y = (float)random.NextDouble();
        if (random.NextDouble() > 0.5){
            x = -x;
        }
        if (random.NextDouble() > 0.5){
            y = -y;
        }
        float[] result = { x, y };
        return result;
    }

    // Update is called once per frame
    void FixedUpdate () {
        animator.SetInteger ("WeaponState", 0);
        GetComponent<CharacterController> ().SimpleMove (Vector3.zero);
        //transform.position = transform.position - transform.up;
        
        movementTargetPosition.y = transform.position.y;
        if (Time.frameCount % 5 == 0){
            randommove = GetRandomPosition();
        }
        Debug.Log(name + randommove[0] + " " + randommove[1]);
        movementTargetPosition.x += randommove[0];
        movementTargetPosition.z += randommove[1];
 
        Vector3 deltaTarget = movementTargetPosition - transform.position;
        lookAtPos = transform.position + deltaTarget.normalized * 2.0f;
        transform.LookAt (lookAtPos);
        if (Vector3.Distance (movementTargetPosition, transform.position) > 0.5f) {
            animator.SetBool ("Idling", false);
        } else {
            animator.SetBool ("Idling", true);
        }
    }
}