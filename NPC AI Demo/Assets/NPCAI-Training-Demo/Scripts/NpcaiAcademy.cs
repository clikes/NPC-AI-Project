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
        List<Vector3> oriGroundScaler;
        

        void Start()
        {
            GameObject[] EnemiesObject = GameObject.FindGameObjectsWithTag("Enemy");
            Enemies = new EnemyController[EnemiesObject.Length];
            for (int i = 0; i < EnemiesObject.Length; i++)
            {
                Enemies[i] = EnemiesObject[i].GetComponent<EnemyController>();
                Enemies[i].Respawn();
            }

            oriGroundScaler = new List<Vector3>();
            GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground");
            foreach (var ground in grounds)
            {
                oriGroundScaler.Add(ground.transform.localScale);
                // Debug.Log(ground.name);
                ground.transform.localScale *= Consts.GroundScale;
            }
        }

        

        public override void AcademyReset()
        {

        }

    }
}