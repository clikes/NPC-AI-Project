using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NpcAI
{
    public class TreasureController : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            
        }

        public void Steal()
        {
            float[] rpos = EnemyController.GetRandomPosition();
            Vector3 rposvector = new Vector3(rpos[0],0.0f,rpos[1]);
            transform.position = GetComponentInParent<TrainingGround>().transform.position + rposvector * Consts.PointGroundLength;


        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}