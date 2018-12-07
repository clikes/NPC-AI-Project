using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NpcAI
{
    public class TrainingGround : MonoBehaviour
    {

        public EnemyController[] Enemies { get; private set; }
        // Use this for initialization
        void Start()
        {
            Enemies = GetComponentsInChildren<EnemyController>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
