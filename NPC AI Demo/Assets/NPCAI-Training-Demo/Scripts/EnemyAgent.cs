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
        TreasureController target;
        System.DateTime currentTime = new System.DateTime();

        /// <summary>
        /// All enemy share a random, in case of same seed
        /// </summary>
        static System.Random random = new System.Random();

        public Material alive;
        public Material dead;
        public bool isAlive;
        float deathTime;

        float time = 0;
        float lastEpisode = 0;
        // Use this for initialization
        void Start()
        {
            cc = GetComponent<CharacterController>();
            academy = GameObject.Find("Academy").GetComponent<NpcaiAcademy>();
            rayPer = GetComponent<ObjectPercepton>();
            lastEpisode = currentTime.Second;
            trainingGround = GetComponentInParent<TrainingGround>();
            isAlive = true;
            deathTime = 0;
            target = trainingGround.GetComponentInChildren<TreasureController>();
        }

        public override void AgentReset()
        {
            transform.position = trainingGround.transform.position;
            transform.position += Consts.HeightOffset;
            lastEpisode = time;
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
            cc.enabled = true;
            GetComponent<Renderer>().material = alive;

        }

        public override void CollectObservations()
        {
            float rayDistance = Consts.OutsideGroundLength * 0.8f;
            float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
            string[] detectableObjects = { "player", "obstacle","wall" };
            List<float> buffer = rayPer.EnemyPerceive(rayDistance, rayAngles, detectableObjects, 0f, 0f);
            AddVectorObs(buffer);
            AddVectorObs(transform.forward.x);
            AddVectorObs(transform.forward.z);
            AddVectorObs(target.transform.position.x - transform.position.x);
            AddVectorObs(target.transform.position.z - transform.position.z);
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
            AddReward(-1.0f);
        }

        float[] GetRandomPosition()
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


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("wall"))
            {
                AddReward(-1.0f);
                AddReward(-0.05f * (time - lastEpisode) / Time.deltaTime);

                isAlive = false;
            }
            if (hit.collider.CompareTag("target"))
            {
                hit.collider.GetComponent<TreasureController>().Steal();
                AddReward(1.0f);
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
                  Done();
                }
                return;
            }

            //time punishment
            AddReward(-0.05f);

            time += Time.deltaTime;
            //Debug.Log("time: " + (time - lastEpisode));
            if (time - lastEpisode > Consts.episodeTime)
            {
                Done();
            }



            Vector3 MoveToward = Vector3.zero;
            MoveToward.x += Mathf.Clamp(vectorAction[0], -1, 1);//钳制value 大于1 则返回1 小于-1则返回-1
            MoveToward.z += Mathf.Clamp(vectorAction[1], -1, 1);
            transform.LookAt(transform.position + MoveToward);
            cc.SimpleMove(Consts.agentMoveSpeed * MoveToward.normalized);
        }



    }
}