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
        //Animator animator;
        public TrainingGround trainingGround;
        float rotateSpeed = 20.0f; //used to smooth out turning
        float[] randommove = { 0, 0 };
        public Vector3 movementTargetPosition;
        public Vector3 attackPos;
        public Vector3 lookAtPos;
        public Material alive;
        public Material dead;
        public bool moveable;

        public bool isAlive;

        float deathTime;

        public Vector3 bornPosition;

        static System.Random random = new System.Random();

        float gravity = 5.0f;

        // Use this for initialization
        void Awake()
        {
            movementTargetPosition = transform.position; //initializing our movement target as our current position
            bornPosition = transform.position;
            isAlive = true;
            deathTime = 0;
            trainingGround = GetComponentInParent<TrainingGround>();
            randommove = GetRandomPosition();
        }

        public static float[] GetRandomPosition()
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
            //animator.SetInteger("Death", 2);
            isAlive = false;
            deathTime = Time.time;
            GetComponent<CharacterController>().enabled = false;
            GetComponent<Renderer>().material = dead;
        }

        public void Respawn()
        {
            //animator.SetInteger("Death", 0);
            Vector3 randomPos = new Vector3
            {
                x = ((float)random.NextDouble() * Consts.OutsideGroundLength  / 2 ) - 2,
                z = ((float)random.NextDouble() * Consts.OutsideGroundLength  / 2 ) -2
            };
            if (random.NextDouble() > 0.5)
            {
                randomPos.x *= -1;
            }
            if (random.NextDouble() > 0.5)
            {
                randomPos.z *= -1;
            }
            randomPos.y = 0.5f;
            randomPos += trainingGround.transform.position;
            transform.position = randomPos;//get new position
            isAlive = true;
            GetComponent<CharacterController>().enabled = true;
            GetComponent<Renderer>().material = alive;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("wall"))
            {
                randommove = GetRandomPosition();
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //animator.SetInteger("WeaponState", 0);
            //GetComponent<CharacterController>().SimpleMove(Vector3.zero);
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
            //transform.LookAt(lookAtPos);
            if (moveable)
            {
                GetComponent<CharacterController>().SimpleMove(deltaTarget.normalized * Consts.enemyMoveSpeed);
            }
            
            if (!isAlive)
            {
               Respawn();
                
            }

        }
    }
}