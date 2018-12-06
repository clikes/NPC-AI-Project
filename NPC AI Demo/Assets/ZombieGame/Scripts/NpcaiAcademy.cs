using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
namespace NpcAI
{   
    /// <summary>
    /// Collect all information in game scene
    /// </summary>
    public class NpcaiAcademy : Academy
    {
        
        public EnemyController[] Enemies { get; private set; }
        System.Random random = new System.Random();
        void Start()
        {
            GameObject[] EnemiesObject = GameObject.FindGameObjectsWithTag("Enemy");
            Enemies = new EnemyController[EnemiesObject.Length];
            for (int i = 0; i < EnemiesObject.Length; i++)
            {
                Enemies[i] = EnemiesObject[i].GetComponent<EnemyController>();
                //Vector3 randomPos = new Vector3
                //{
                //    x = (float)random.NextDouble() * (Consts.OutsideGroundLength - Consts.AlertGroundLength) / 2 + Consts.AlertGroundLength / 2,
                //    z = (float)random.NextDouble() * (Consts.OutsideGroundLength - Consts.AlertGroundLength) / 2 + Consts.AlertGroundLength / 2
                //};
                Vector3 randomPos = new Vector3
                {
                    x = (float)random.NextDouble() * Consts.OutsideGroundLength / 2,
                    z = (float)random.NextDouble() * Consts.OutsideGroundLength / 2
                };
                if (random.NextDouble() > 0.5)
                {
                    randomPos.x *= -1;
                }
                if (random.NextDouble() > 0.5)
                {
                    randomPos.z *= -1;
                }
                Enemies[i].transform.position = randomPos;
                Enemies[i].bornPosition = randomPos;
            }

        }

        public override void AcademyReset()
        {

        }

    }
}