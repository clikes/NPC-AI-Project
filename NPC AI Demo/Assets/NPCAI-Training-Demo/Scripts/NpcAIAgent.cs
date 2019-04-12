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
        int killcount = 0;
        System.DateTime currentTime = new System.DateTime();
        EnemyController CloestEnemy = null;
        float CloestEnemyDistance = float.MaxValue;
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
            target = trainingGround.GetComponentInChildren<TreasureController>();
        }

        public override void AgentReset()
        {
            killcount = 0;
            CloestEnemyDistance = float.MaxValue;
            transform.position = trainingGround.transform.position;
            transform.position += Consts.HeightOffset;
            lastEpisode = time;
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

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.collider);
            if (collision.collider.CompareTag("wall"))
            {
                AddReward(-1.0f);
                AddReward(-0.01f * (time - lastEpisode) / Time.deltaTime);
                Done();
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
           // Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("wall"))
            {
                AddReward(-1.0f);
                AddReward(-0.05f * (time - lastEpisode) / Time.deltaTime);
                Done();
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
            AddReward(-0.05f);
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
                AddReward(0.05f);
                Debug.Log("DISTANCE REWARD");
                CloestEnemyDistance = distance;
            }
           // Debug.Log(CloestEnemyDistance);
            CloestEnemyDistance = distance;

            time += Time.deltaTime;
            //Debug.Log("time: " + (time - lastEpisode));
            if ( time - lastEpisode > Consts.episodeTime)
            {
                Done();
            }



            Vector3 MoveToward = Vector3.zero;
            MoveToward.x += Mathf.Clamp(vectorAction[0], -1, 1);//钳制value 大于1 则返回1 小于-1则返回-1
            MoveToward.z += Mathf.Clamp(vectorAction[1], -1, 1);
            transform.LookAt(transform.position + MoveToward);
            cc.SimpleMove(Consts.agentMoveSpeed * MoveToward.normalized);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
