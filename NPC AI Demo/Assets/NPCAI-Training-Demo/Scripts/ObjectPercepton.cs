using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NpcAI
{
    public class ObjectPercepton : MonoBehaviour
    {
        List<float> perceptionBuffer = new List<float>();
        Vector3 endPosition;
        public NpcAIAgent player = null;
        public EnemyAgent enemy = null;
        bool rewardFlag = true;
        RaycastHit hit;
        public List<GameObject> objects { get; private set; } = new List<GameObject>();
        /// <summary>
        /// Creates perception vector to be used as part of an observation of an agent.
        /// </summary>
        /// <returns>The partial vector observation corresponding to the set of rays</returns>
        /// <param name="rayDistance">Radius of rays</param>
        /// <param name="rayAngles">Anlges of rays (starting from (1,0) on unit circle).</param>
        /// <param name="detectableObjects">List of tags which correspond to object types agent can see</param>
        /// <param name="startOffset">Starting heigh offset of ray from center of agent.</param>
        /// <param name="endOffset">Ending height offset of ray from center of agent.</param>
        public List<float> Perceive(float rayDistance,
            float[] rayAngles, string[] detectableObjects,
                                    float startOffset, float endOffset)
        {
            perceptionBuffer.Clear();
            objects.Clear();
            // For each ray sublist stores categorial information on detected object
            // along with object distance.
            foreach (float angle in rayAngles)
            {
                endPosition = transform.TransformDirection(
                    PolarToCartesian(rayDistance, angle));
                endPosition.y = endOffset;
                if (Application.isEditor)
                {
                    Debug.DrawRay(transform.position + new Vector3(0f, startOffset, 0f),
                        endPosition, Color.black, 0.01f, true);
                }
                //extra 3 for 1,if not found object 2, enemy isalive, the distance to the object
                float[] subList = new float[detectableObjects.Length + 3];
                if (Physics.SphereCast(transform.position +
                                       new Vector3(0f, startOffset, 0f), Consts.GroundScale,
                    endPosition, out hit, rayDistance))
                {
                    for (int i = 0; i < detectableObjects.Length; i++)
                    {
                        if (hit.collider.gameObject.CompareTag(detectableObjects[i]))
                        {
                            //objects.Add(hit.collider.gameObject);
                            if (hit.collider.gameObject.CompareTag("Enemy"))
                            {
                                if ( player != null  )
                                {
                                    rewardFlag = false;
                                    player.AddReward(0.001f);
                                }
                                
                                //Debug.Log("detect enemy at pos" + hit.point);
                            }
                            if (hit.collider.gameObject.CompareTag("target"))
                            {
                                if (enemy != null)
                                {
                                    enemy.AddReward(0.02f);
                                }
                            }
                            subList[i] = 1;

                            subList[detectableObjects.Length + 1] = hit.point.x - transform.position.x;
                            subList[detectableObjects.Length + 2] = hit.point.z - transform.position.z;
                            
                            break;
                        }
                    }
                }
                else
                {
                    subList[detectableObjects.Length] = 1f;
                }
                string test = "";
                foreach (var item in subList)
                {
                    test += item.ToString();
                    test += ",";
                }
                //Debug.Log(test);
                perceptionBuffer.AddRange(subList);
            }

            return perceptionBuffer;
        }

        public List<float> EnemyPerceive(float rayDistance,
            float[] rayAngles, string[] detectableObjects,
                                    float startOffset, float endOffset)
        {
            perceptionBuffer.Clear();
            objects.Clear();
            // For each ray sublist stores categorial information on detected object
            // along with object distance.
            foreach (float angle in rayAngles)
            {
                endPosition = transform.TransformDirection(
                    PolarToCartesian(rayDistance, angle));
                endPosition.y = endOffset;
                if (Application.isEditor)
                {
                    Debug.DrawRay(transform.position + new Vector3(0f, startOffset, 0f),
                        endPosition, Color.black, 0.01f, true);
                }
                //extra 3 for 1,if not found object 2, enemy isalive, the distance to the object
                float[] subList = new float[detectableObjects.Length + 3];
                if (Physics.SphereCast(transform.position +
                                       new Vector3(0f, startOffset, 0f), Consts.GroundScale,
                    endPosition, out hit, rayDistance))
                {
                    for (int i = 0; i < detectableObjects.Length; i++)
                    {
                        if (hit.collider.gameObject.CompareTag(detectableObjects[i]))
                        {
                            //objects.Add(hit.collider.gameObject);

                            subList[i] = 1;
                            //subList[detectableObjects.Length + 2] = hit.distance / rayDistance;
                            if (hit.collider.gameObject.CompareTag("player"))
                            {
                                //Debug.Log("detect enemy: " + hit.collider.name);
                                subList[detectableObjects.Length + 1] = hit.collider.gameObject.transform.position.x - transform.position.x;
                                subList[detectableObjects.Length + 2] = hit.collider.gameObject.transform.position.z - transform.position.z;
                            }
                            else
                            {
                                subList[detectableObjects.Length + 2] = hit.distance / rayDistance;
                            }
                            if (hit.collider.gameObject.CompareTag("target"))
                            {
                                Debug.Log("see target");
                            }
                            break;
                        }
                    }
                }
                else
                {
                    subList[detectableObjects.Length] = 1f;
                }
                string test = "";
                foreach (var item in subList)
                {
                    test += item.ToString();
                    test += ",";
                }
                //Debug.Log(test);
                perceptionBuffer.AddRange(subList);
            }

            return perceptionBuffer;
        }

        /// <summary>
        /// Converts polar coordinate to cartesian coordinate.
        /// </summary>
        public static Vector3 PolarToCartesian(float radius, float angle)
        {
            float x = radius * Mathf.Cos(DegreeToRadian(angle));
            float z = radius * Mathf.Sin(DegreeToRadian(angle));
            return new Vector3(x, 0f, z);
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        public static float DegreeToRadian(float degree)
        {
            return degree * Mathf.PI / 180f;
        }
    }
}