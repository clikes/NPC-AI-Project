using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
namespace NpcAI
{
    public class NpcAIAgent : Agent
    {
        NpcaiAcademy academy;
        Animator animator;
        private EnemyPercepton rayPer;//for enemy detect
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
            animator = GetComponent<Animator>();
            academy = GameObject.Find("Academy").GetComponent<NpcaiAcademy>();
            rayPer = GetComponent<EnemyPercepton>();
            lastEpisode = currentTime.Second;
        }

        public override void AgentReset()
        {
            killcount = 0;
            CloestEnemyDistance = float.MaxValue;
            transform.position = Vector3.zero;
            lastEpisode = time;
        }

        public override void CollectObservations()
        {
            float rayDistance = Consts.OutsideGroundLength;
            float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f,
                                 -20f, -90f, -160f, -45f, -135f, -70f, -110f};
            string[] detectableObjects = { "Enemy" , "wall"};
            List<float> buffer = rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f);
            AddVectorObs(buffer);
            AddVectorObs(transform.position.x);
            AddVectorObs(transform.position.z);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
           // Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("wall"))
            {
                AddReward(-1.0f);
                //AddReward(-0.01f * (time - lastEpisode) / Time.deltaTime);
                Done();
            }
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            //time punishment
            //AddReward(-0.01f);
            float distance = 0;
            //kill enemies 
            CloestEnemy = academy.Enemies[0];
            foreach (var e in academy.Enemies)
            {
                var ec = e.GetComponent<EnemyController>();
                distance = Vector3.Distance(e.transform.position, transform.position);
                if (ec.isAlive && distance < Consts.killDistance){
                    e.Kill();
                    AddReward(1.0f);
                    CloestEnemyDistance = float.MaxValue;
                }
                if (ec.isAlive && distance < Vector3.Distance(CloestEnemy.transform.position, transform.position))
                {
                    CloestEnemy = ec;
                }
            }

            distance = Vector3.Distance(CloestEnemy.transform.position, transform.position);
            if (distance < CloestEnemyDistance)
            {
                AddReward(0.05f);
                CloestEnemyDistance = distance;
            }
            //Debug.Log(CloestEnemyDistance);
            //CloestEnemyDistance = distance;

            time += Time.deltaTime;
            Debug.Log("time: " + (time - lastEpisode));
            if ( time - lastEpisode > Consts.episodeTime)
            {
                Done();
            }



            Vector3 MoveToward = Vector3.zero;
            MoveToward.x += Mathf.Clamp(vectorAction[0], -1, 1);//钳制value 大于1 则返回1 小于-1则返回-1
            MoveToward.z += Mathf.Clamp(vectorAction[1], -1, 1);
            animator.SetBool("Idling", false);
            transform.LookAt(transform.position + MoveToward);
            cc.SimpleMove(Consts.agentMoveSpeed * MoveToward.normalized);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
