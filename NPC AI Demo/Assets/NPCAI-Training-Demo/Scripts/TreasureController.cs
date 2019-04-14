using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NpcAI
{
    public class TreasureController : MonoBehaviour
    {
        TrainingGround ground;
        float lastEpisode = 0;
        // Use this for initialization
        void Start()
        {
            ground = GetComponentInParent<TrainingGround>();
        }

        void OnCollisionStay(Collision collision)
        {
            if (collision.collider.CompareTag("obstacle"))//stuck in wall
            {
                randomPos();

            }
        }

        void randomPos()
        {
            float[] rpos = EnemyController.GetRandomPosition();
            Vector3 rposvector = new Vector3(rpos[0], 0.0f, rpos[1]);
            transform.position = ground.transform.position + rposvector * Consts.PointGroundLength / 2 + new Vector3(0, 0.5f, 0);
        }

        public void Steal()
        {
            randomPos();

            ground.GetComponentInChildren<NpcAIAgent>().AddReward(-1.0f);

        }


        // Update is called once per frame
        void Update()
        {
            if (Timer.time - lastEpisode > Consts.episodeTime * 2)
            {
                randomPos();
                lastEpisode = Timer.time;
            }
        }
    }
}