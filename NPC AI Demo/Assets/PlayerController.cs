using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject[] EmemiesObject;
	Animator animator;
	public EmemyController[] Ememies;

	public EmemyController Target;

	public TargetFllower tf;

	System.Random random = new System.Random ();

	CharacterController cc;

	public float speed;
	Vector3 lookAtPos;

	void Start () {
		animator = GetComponent<Animator> ();
		EmemiesObject = GameObject.FindGameObjectsWithTag ("Enemy");
		Ememies = new EmemyController[EmemiesObject.Length];
		for (int i = 0; i < EmemiesObject.Length; i++) {
			Ememies[i] = EmemiesObject[i].GetComponent<EmemyController> ();
		}
		cc = GetComponent<CharacterController> ();
		Target = null;
		speed = 8;
	}

	void UpdateTarget () {
		if (Target != null && Target.isAlive) return;
		do {
			Target = Ememies[random.Next (Ememies.Length - 1)];
			tf.target = Target.gameObject;
		} while (!Target.isAlive);

	}
	/// <summary>
	/// OnControllerColliderHit is called when the controller hits a
	/// collider while performing a Move.
	/// </summary>
	/// <param name="hit">The ControllerColliderHit data associated with this collision.</param>
	void OnControllerColliderHit (ControllerColliderHit hit) {
		if (hit.gameObject == Target.gameObject) {
			Target.Kill ();
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		UpdateTarget ();
		Vector3 deltaTarget = Target.transform.position - transform.position;
		lookAtPos = transform.position + deltaTarget.normalized * 2.0f;
		transform.LookAt (lookAtPos);
		cc.SimpleMove (speed * deltaTarget.normalized);
		if (Vector3.Distance (Target.transform.position, transform.position) > 0.5f) {
			animator.SetBool ("Idling", false);

		} else {
			animator.SetBool ("Idling", true);
		}

	}
}