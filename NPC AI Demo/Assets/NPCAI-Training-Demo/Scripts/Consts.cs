using System;
using UnityEngine;
namespace NpcAI
{
    public static class Consts
    {

        public readonly static float GroundScale = 1f;
        static int PointGroundLengthNormal = 10;
        static int AlertGroundLengthNormal = 17;
        static int OutsideGroundLengthNormal = 24;
        public readonly static float PointGroundLength = GroundScale * PointGroundLengthNormal;
        public readonly static float AlertGroundLength = GroundScale * AlertGroundLengthNormal;
        public readonly static float OutsideGroundLength = GroundScale * OutsideGroundLengthNormal;
        public readonly static float killDistance = 1.2f;
        public readonly static float agentMoveSpeed = 8.0f;
        public readonly static Vector3 HeightOffset = new Vector3(0,0.5f, 0);
        public readonly static float EnemyRespawnTime = 0f;
        /// <summary>
        /// in second
        /// </summary>
        public readonly static float episodeTime = 30f;
    }
}
