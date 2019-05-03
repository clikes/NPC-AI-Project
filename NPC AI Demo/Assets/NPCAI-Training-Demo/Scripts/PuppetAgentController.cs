using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NpcAI
{
    public class PuppetAgentController : MonoBehaviour
    {

        TrainingGround ground;
        float lastEpisode = 0;
        public Vector3 movementTargetPosition;
        public Vector3 attackPos;
        public Vector3 lookAtPos;
        public Material alive;
        public Material dead;
        public bool moveable;
        float[] randommove;
        private ObjectPerception rayPer;
        Rigidbody rb;
        // Use this for initialization
        void Start()
        {
            ground = GetComponentInParent<TrainingGround>();
            movementTargetPosition = transform.position; //initializing our movement target as our current position
            rayPer = GetComponent<ObjectPerception>();
            //isAlive = true;
            // deathTime = 0;
            // trainingGround = GetComponentInParent<TrainingGround>();
            randommove = EnemyController.GetRandomPosition();
            rb = GetComponent<Rigidbody>();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Enemy"))
            {

                collision.collider.GetComponent<EnemyAgent>().Kill();
                randomPos();
            }
        }

        void randomPos()
        {
            float[] rpos = EnemyController.GetRandomPosition();
            Vector3 rposvector = new Vector3(rpos[0], 0.0f, rpos[1]);
            transform.position = ground.transform.position + rposvector * Consts.PointGroundLength / 3 + new Vector3(0, 0.5f, 0);
        }
        // Update is called once per frame
        void Update()
        {
            //GetComponent<Rigidbody>().velo
            if (Timer.time - lastEpisode > Consts.episodeTime * 2)
            {
                randomPos();
                lastEpisode = Timer.time;
            }
            float rayDistance = Consts.OutsideGroundLength * 0.8f;
            float[] rayAngles = {90f};
            string[] detectableObjects = {  "obstacle" };
            List<float> buffer = rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f);

            movementTargetPosition = transform.position;
            if (Time.frameCount % 30 == 0)
            {
                randommove = EnemyController.GetRandomPosition();
            }
            //Debug.Log (name + randommove[0] + " " + randommove[1]);
            movementTargetPosition.x += randommove[0] * 10;
            movementTargetPosition.z += randommove[1] * 10;

            Vector3 deltaTarget = movementTargetPosition - transform.position;
            lookAtPos = transform.position + deltaTarget.normalized * 2.0f;
            rb.AddForce(deltaTarget.normalized * Consts.enemyMoveSpeed, ForceMode.VelocityChange);
            if (rb.velocity.magnitude > Consts.enemyMoveSpeed)
            {
                rb.velocity = rb.velocity.normalized * Consts.enemyMoveSpeed;
            }

        }
    }
}