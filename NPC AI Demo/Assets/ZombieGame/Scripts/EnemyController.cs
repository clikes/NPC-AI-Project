using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NpcAI
{
    /// <summary>
    /// Control the Enemy, some code is from the CharaterDemoController which not my originnal code
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        Animator animator;
        public GameObject floorPlane;
        float rotateSpeed = 20.0f; //used to smooth out turning
        float[] randommove = { 0, 0 };
        public Vector3 movementTargetPosition;
        public Vector3 attackPos;
        public Vector3 lookAtPos;

        public bool isAlive;

        float deathTime;

        public Vector3 bornPosition;

        static System.Random random = new System.Random();

        float gravity = 5.0f;

        // Use this for initialization
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            movementTargetPosition = transform.position; //initializing our movement target as our current position
            bornPosition = transform.position;
            isAlive = true;
            deathTime = 0;
        }

        static float[] GetRandomPosition()
        {

            float x = (float)random.NextDouble();
            float y = (float)random.NextDouble();
            if (random.NextDouble() > 0.5)
            {
                x = -x;
            }
            if (random.NextDouble() > 0.5)
            {
                y = -y;
            }
            float[] result = { x, y };
            return result;
        }

        public void Kill()
        {
            animator.SetInteger("Death", 2);
            isAlive = false;
            deathTime = Time.time;
            GetComponent<CharacterController>().enabled = false;
        }

        void Respawn()
        {
            animator.SetInteger("Death", 0);
            Vector3 randomPos = new Vector3
            {
                x = (float)random.NextDouble() * Consts.OutsideGroundLength  / 2,
                z = (float)random.NextDouble() * Consts.OutsideGroundLength  / 2
            };
            if (random.NextDouble() > 0.5)
            {
                randomPos.x *= -1;
            }
            if (random.NextDouble() > 0.5)
            {
                randomPos.z *= -1;
            }
            transform.position = randomPos;
            isAlive = true;
            GetComponent<CharacterController>().enabled = true;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            animator.SetInteger("WeaponState", 0);
            GetComponent<CharacterController>().SimpleMove(Vector3.zero);
            //transform.position = transform.position - transform.up;

            movementTargetPosition = transform.position;
            if (Time.frameCount % 30 == 0)
            {
                randommove = GetRandomPosition();
            }
            //Debug.Log (name + randommove[0] + " " + randommove[1]);
            movementTargetPosition.x += randommove[0] * 10;
            movementTargetPosition.z += randommove[1] * 10;

            Vector3 deltaTarget = movementTargetPosition - transform.position;
            lookAtPos = transform.position + deltaTarget.normalized * 2.0f;
            transform.LookAt(lookAtPos);
            if (Vector3.Distance(movementTargetPosition, transform.position) > 0.5f)
            {
                animator.SetBool("Idling", false);
            }
            else
            {
                animator.SetBool("Idling", true);
            }
            animator.SetBool("Idling", true);
            if (!isAlive)
            {
                if (Time.time - deathTime >= 3)
                {
                    Respawn();
                }
            }

        }
    }
}