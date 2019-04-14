using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
namespace NpcAI
{
    public class EnemyAgent : Agent
    {

        // Use this for initialization
        NpcaiAcademy academy;
        //Animator animator;
        private ObjectPercepton rayPer;//for enemy detect
        TrainingGround trainingGround;
        CharacterController cc;
        Rigidbody agentRb;
        TreasureController target;
        NpcAIAgent player;
        float disToTarget;
        System.DateTime currentTime = new System.DateTime();

        /// <summary>
        /// All enemy share a random, in case of same seed
        /// </summary>
        static System.Random random = new System.Random();

        public Material alive;
        public Material dead;
        public bool isAlive;
        float deathTime;
        float struckTime = float.MinValue;

        public float lastEpisode { get; private set; } = 0;
        // Use this for initialization
        void Start()
        {
            cc = GetComponent<CharacterController>();
            cc.enabled = false;
            academy = GameObject.Find("Academy").GetComponent<NpcaiAcademy>();
            rayPer = GetComponent<ObjectPercepton>();
            rayPer.enemy = this;
            lastEpisode = Timer.time;
            trainingGround = GetComponentInParent<TrainingGround>();
            isAlive = true;
            deathTime = 0;
            target = trainingGround.GetComponentInChildren<TreasureController>();
            agentRb = GetComponent<Rigidbody>();
            disToTarget = float.MaxValue;
            player = trainingGround.GetComponentInChildren<NpcAIAgent>();
        }

        public override void AgentReset()
        {
            struckTime = float.MinValue;
            transform.position = trainingGround.transform.position;
            transform.position += Consts.HeightOffset;
            
            Vector3 randomPos = new Vector3
            {
                x = ((float)random.NextDouble() * Consts.OutsideGroundLength / 2) - 2,
                z = ((float)random.NextDouble() * Consts.OutsideGroundLength / 2) - 2
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
            //cc.enabled = true;
            GetComponent<Renderer>().material = alive;

        }

        public override void CollectObservations()
        {
            float rayDistance = Consts.OutsideGroundLength * 0.8f;
            float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
            string[] detectableObjects = { "obstacle","wall", "agent", "target"};
            List<float> buffer = rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f);
            AddVectorObs(buffer);
            AddVectorObs(transform.forward.x);
            AddVectorObs(transform.forward.z);
            //AddVectorObs(target.transform.position.x - transform.position.x);
            //AddVectorObs(target.transform.position.z - transform.position.z);

        }

        /// <summary>
        /// this enemy been kill by playeragent
        /// </summary>
        public void Kill()
        {
            //animator.SetInteger("Death", 2);
            isAlive = false;
            deathTime = Time.time;
            GetComponent<CharacterController>().enabled = false;
            GetComponent<Renderer>().material = dead;
            AddReward(-2.5f);
        }


        private void OnCollisionEnter(Collision collision)
        {
            // Debug.Log(hit.collider.name);
            if (collision.collider.CompareTag("wall"))
            {
                AddReward(-1.0f);
                //AddReward(-0.05f * (Timer.time - lastEpisode) / Time.deltaTime);

                //isAlive = false;
            }
            if (collision.collider.CompareTag("target"))
            {
                collision.collider.GetComponent<TreasureController>().Steal();
                disToTarget = float.MaxValue;
                AddReward(5.0f);
            }
            if (collision.collider.CompareTag("obstacle"))
            {
                AddReward(-0.05f);
            }
            
        }

        void OnCollisionExit(Collision collision)
        {
            if (collision.collider.CompareTag("obstacle"))
            {
                struckTime = float.MinValue;
            }
        }

        void OnCollisionStay(Collision collision)
        {
            if (collision.collider.CompareTag("obstacle"))//stuck in wall
            {
                //AgentReset();
                AddReward(-0.05f);
                if (struckTime == float.MinValue)
                {
                    struckTime = Timer.time;
                }
                else if (Timer.time - struckTime > 2)
                {
                    AgentReset();
                }
            }
            if (collision.collider.CompareTag("wall"))
            {
                AddReward(-0.05f);
                //AddReward(-0.05f * (Timer.time - lastEpisode) / Time.deltaTime);

                //isAlive = false;
            }
        }

        

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            return;
            // Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("wall"))
            {
                AddReward(-1.0f);
                AddReward(-0.05f * (Timer.time - lastEpisode) / Time.deltaTime);

                isAlive = false;
            }
            if (hit.collider.CompareTag("target"))
            {
                hit.collider.GetComponent<TreasureController>().Steal();
                AddReward(5.0f);
            }
            if (hit.collider.CompareTag("obstacle"))
            {
                AddReward(-0.05f);
            }

        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {

            if (!isAlive)
            {
                if (Time.time - deathTime >= Consts.EnemyRespawnTime)
                {
                    AgentReset();
                }
                return;
            }

            //time punishment
            //AddReward(-0.05f);

            
            //Debug.Log("time: " + (time - lastEpisode));
            if (Timer.time - lastEpisode > Consts.episodeTime)
            {
                lastEpisode = Timer.time;
                Done();
            }

            float curDisToTarget = Vector3.Distance(target.transform.position, transform.position);
            //get close to target
            if (curDisToTarget > 3 && disToTarget - curDisToTarget > 1)
            {
                //Debug.Log("disreward");
                disToTarget = curDisToTarget;
                AddReward(0.05f);
            }

            //too close to player punish
            //if (Vector3.Distance(player.transform.position, transform.position) <= 2)
            //{
            //    AddReward(-0.05f);
            //}

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

            }
            agentRb.AddForce(dirToGo.normalized * Consts.enemyMoveSpeed, ForceMode.VelocityChange);
            if (agentRb.velocity.magnitude > Consts.enemyMoveSpeed)//let speed not pass the max speed
            {
                agentRb.velocity = agentRb.velocity.normalized * Consts.enemyMoveSpeed;
            }
            transform.Rotate(rotateDir, Time.fixedDeltaTime * Consts.TurningSpeed);
        }



    }
}