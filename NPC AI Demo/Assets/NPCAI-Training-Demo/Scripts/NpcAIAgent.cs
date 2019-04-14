using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
namespace NpcAI
{
    public class NpcAIAgent : Agent
    {
        NpcaiAcademy academy;
        //Animator animator;
        private ObjectPercepton rayPer;//for enemy detect
        TrainingGround trainingGround;
        TreasureController target;
        CharacterController cc;
        Rigidbody agentRb;
        int killcount = 0;
        System.DateTime currentTime = new System.DateTime();
        EnemyController CloestEnemy = null;
        float CloestEnemyDistance = float.MaxValue;
        
        float lastEpisode = 0;
        // Use this for initialization
        void Start()
        {
            cc = GetComponent<CharacterController>();
            academy = GameObject.Find("Academy").GetComponent<NpcaiAcademy>();
            rayPer = GetComponent<ObjectPercepton>();
            rayPer.player = this;
            lastEpisode = currentTime.Second;
            trainingGround = GetComponentInParent<TrainingGround>();
            target = trainingGround.GetComponentInChildren<TreasureController>();
            target.gameObject.SetActive(false);
            agentRb = GetComponent<Rigidbody>();
        }

        public override void AgentReset()
        {
            killcount = 0;
            CloestEnemyDistance = float.MaxValue;
            transform.position = trainingGround.transform.position;
            transform.position += Consts.HeightOffset;
            lastEpisode = Timer.time;
        }

        public override void CollectObservations()
        {
            float rayDistance = Consts.OutsideGroundLength * 0.8f;
            float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f};
            string[] detectableObjects = { "Enemy" , "wall" , "obstacle"};
            List<float> buffer = rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f);
            AddVectorObs(buffer);
            AddVectorObs(transform.forward.x);
            AddVectorObs(transform.forward.z);
            AddVectorObs(target.transform.position.x - transform.position.x);
            AddVectorObs(target.transform.position.z - transform.position.z);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider.CompareTag("obstacle"))//stuck in wall
            {
                AddReward(-0.05f);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Debug.Log(hit.collider.name);
            if (collision.collider.CompareTag("wall"))
            {
                AddReward(-1.0f);
                //AddReward(-0.05f * (Timer.time - lastEpisode) / Time.deltaTime);
                //Done();
                AgentReset();
            }
            else if (collision.collider.CompareTag("Enemy"))
            {

                if (collision.collider.GetComponent<EnemyAgent>().isActiveAndEnabled == false)
                {
                    collision.collider.GetComponent<EnemyController>().Kill();
                }
                else
                {
                    collision.collider.GetComponent<EnemyAgent>().Kill();
                }
                CloestEnemyDistance = float.MaxValue;
                AddReward(5.0f);
            }
            else if (collision.collider.CompareTag("obstacle"))
            {
                AddReward(-0.05f);
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // Debug.Log(hit.collider.name);
            return;
            if (hit.collider.CompareTag("wall"))
            {
                AddReward(-1.0f);
                AddReward(-0.05f * (Timer.time - lastEpisode) / Time.deltaTime);
                // Done();
                AgentReset();
            }
            else if (hit.collider.CompareTag("Enemy"))
            {
                
                if (hit.collider.GetComponent<EnemyAgent>().isActiveAndEnabled == false)
                {
                    hit.collider.GetComponent<EnemyController>().Kill();
                }
                else
                {
                    hit.collider.GetComponent<EnemyAgent>().Kill();
                }
               
                AddReward(1.0f);
            }
            else if (hit.collider.CompareTag("obstacle"))
            {
                AddReward(-0.05f);
            }
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            //time punishment
            //AddReward(-0.05f);
            float distance = 0;

            CloestEnemy = trainingGround.Enemies[0];
            foreach (var e in trainingGround.Enemies)
            {
                var ec = e.GetComponent<EnemyController>();
                distance = Vector3.Distance(e.transform.position, transform.position);
                if (ec.isAlive && distance < Vector3.Distance(CloestEnemy.transform.position, transform.position))
                {
                    CloestEnemy = ec;
                }
            }

            distance = Vector3.Distance(CloestEnemy.transform.position, transform.position);//the distance from cloestenemy to Agent this frame
            //get reward from get close to enemy
            if (CloestEnemyDistance - distance > 3 && CloestEnemyDistance - distance > CloestEnemyDistance / 5)
            {
                AddReward(0.1f);
                Debug.Log("DISTANCE REWARD");
                CloestEnemyDistance = distance;
            }
            else if (CloestEnemyDistance - distance < 0)
            {
                AddReward(-0.1f * (distance -CloestEnemyDistance));
            }
           // Debug.Log(CloestEnemyDistance);
            CloestEnemyDistance = distance;

            
            //Debug.Log("time: " + (time - lastEpisode));
            if (Timer.time - lastEpisode > Consts.episodeTime)
            {
                Done();
            }

            GetReward();

            Vector3 dirToGo = Vector3.zero;
            Vector3 rotateDir = Vector3.zero;
            Vector3 MoveToward = Vector3.zero;
                Vector3 LookToward = Vector3.zero;
            if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
            {
                
                MoveToward.x += Mathf.Clamp(vectorAction[0], -1, 1);//钳制value 大于1 则返回1 小于-1则返回-1
                MoveToward.z += Mathf.Clamp(vectorAction[1], -1, 1);
                LookToward.x += Mathf.Clamp(vectorAction[2], -1, 1);
                LookToward.z += Mathf.Clamp(vectorAction[3], -1, 1);
                transform.LookAt(transform.position + LookToward.normalized);
                //cc.SimpleMove(Consts.agentMoveSpeed * MoveToward.normalized);
                agentRb.AddForce(MoveToward.normalized * Consts.agentMoveSpeed, ForceMode.VelocityChange);
                if (agentRb.velocity.magnitude > Consts.agentMoveSpeed)
                {
                    agentRb.velocity = agentRb.velocity.normalized * Consts.agentMoveSpeed;
                }
                //dirToGo = transform.forward * Mathf.Clamp(vectorAction[0], -1f, 1f);
                //rotateDir = transform.up * Mathf.Clamp(vectorAction[1], -1f, 1f);

                //agentRb.AddForce(dirToGo.normalized * Consts.agentMoveSpeed, ForceMode.VelocityChange);
                //if (agentRb.velocity.magnitude > Consts.agentMoveSpeed)
                //{
                //    agentRb.velocity = agentRb.velocity.normalized * Consts.agentMoveSpeed;
                //}
                //transform.Rotate(rotateDir, Time.fixedDeltaTime * Consts.TurningSpeed);
            }
            else
            {
                int forwardAxis = (int)vectorAction[0];
                int rightAxis = (int)vectorAction[1];
                int rotateAxis = (int)vectorAction[2];
                int shootAxis = (int)vectorAction[3];

                switch (forwardAxis)
                {
                    case 0:
                        dirToGo += transform.forward;
                        break;
                    case 1:
                        dirToGo += -transform.forward;
                        break;
                }

                switch (rightAxis)
                {
                    case 0:
                        dirToGo += transform.right;
                        break;
                    case 1:
                        dirToGo += -transform.right;
                        break;
                }

                switch (rotateAxis)
                {
                    case 0:
                        rotateDir = -transform.up;
                        break;
                    case 1:
                        rotateDir = transform.up;
                        break;
                }
                //Debug.Log(rightAxis);

                agentRb.AddForce(dirToGo.normalized * Consts.agentMoveSpeed, ForceMode.VelocityChange);
                if (agentRb.velocity.magnitude > Consts.agentMoveSpeed)
                {
                    agentRb.velocity = agentRb.velocity.normalized * Consts.agentMoveSpeed;
                }
                transform.Rotate(rotateDir, Time.fixedDeltaTime * Consts.TurningSpeed);
            }
            

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
