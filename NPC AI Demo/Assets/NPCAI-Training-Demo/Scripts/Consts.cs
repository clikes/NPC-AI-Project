using System;
using UnityEngine;
namespace NpcAI
{
    public static class Consts
    {

        public static float GroundScale { get; set; } = 1.0f;
        public readonly static int NumOfEnemies = 1;
        static int PointGroundLengthNormal = 10;
        static int AlertGroundLengthNormal = 17;
        static int OutsideGroundLengthNormal = 24;
        public readonly static float PointGroundLength = GroundScale * PointGroundLengthNormal;
        public readonly static float AlertGroundLength = GroundScale * AlertGroundLengthNormal;
        public readonly static float OutsideGroundLength = GroundScale * OutsideGroundLengthNormal;
        public readonly static float killDistance = 1.2f;
        public readonly static float agentMoveSpeed = 10.0f;
        public readonly static float enemyMoveSpeed = 7.0f;
        public readonly static Vector3 HeightOffset = new Vector3(0,0.5f, 0);
        public readonly static float EnemyRespawnTime = 0f;
        public readonly static float TurningSpeed = 600.0f;
        /// <summary>
        /// in second
        /// </summary>
        public readonly static float episodeTime = 30f;
    }
}
