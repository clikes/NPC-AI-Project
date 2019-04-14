using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    // Use this for initialization
    public static volatile float time = 0;
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        time += Time.fixedDeltaTime;

	}
}
