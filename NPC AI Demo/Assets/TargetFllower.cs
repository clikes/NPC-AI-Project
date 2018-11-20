using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFllower : MonoBehaviour {

	public GameObject target;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 newPos = target.transform.position;
		newPos.y +=3;
		transform.position = newPos;
	}
}