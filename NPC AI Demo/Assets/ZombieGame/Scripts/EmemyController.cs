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

    void GetRandomPosition(){
        Math.Random random  = new Random();
        rando
    }

    // Update is called once per frame
    void Update () {
        animator.SetInteger ("WeaponState", 0);
        GetComponent<CharacterController> ().SimpleMove (Vector3.zero);
        //transform.position = transform.position - transform.up;
        
        movementTargetPosition.y = transform.position.y;
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