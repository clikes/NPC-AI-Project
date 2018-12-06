using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NpcAI
{
    public class PlayerController : MonoBehaviour
    {

        public GameObject[] EnemiesObject;
        Animator animator;
        public EnemyController[] Enemies;

        public EnemyController Target;

        public TargetFllower tf;

        System.Random random = new System.Random();

        CharacterController cc;

        public float speed;
        Vector3 lookAtPos;

        void Start()
        {
            animator = GetComponent<Animator>();
            EnemiesObject = GameObject.FindGameObjectsWithTag("Enemy");
            Enemies = new EnemyController[EnemiesObject.Length];
            for (int i = 0; i < EnemiesObject.Length; i++)
            {
                Enemies[i] = EnemiesObject[i].GetComponent<EnemyController>();
            }
            cc = GetComponent<CharacterController>();
            Target = null;
            speed = 8;
        }

        void UpdateTarget()
        {
            if (Target != null && Target.isAlive) return;
            do
            {
                Target =  Enemies[random.Next(Enemies.Length - 1)];
                tf.target = Target.gameObject;
            } while (!Target.isAlive);

        }
        /// <summary>
        /// OnControllerColliderHit is called when the controller hits a
        /// collider while performing a Move.
        /// </summary>
        /// <param name="hit">The ControllerColliderHit data associated with this collision.</param>
        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject == Target.gameObject)
            {
                Debug.Log(Vector3.Distance(hit.transform.position, transform.position));
                Target.Kill();
            }
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            UpdateTarget();
            Vector3 deltaTarget = Target.transform.position - transform.position;
            lookAtPos = transform.position + deltaTarget.normalized * 2.0f;
            transform.LookAt(lookAtPos);
            cc.SimpleMove(speed * deltaTarget.normalized);
            if (Vector3.Distance(Target.transform.position, transform.position) > 0.5f)
            {
                animator.SetBool("Idling", false);

            }
            else
            {
                animator.SetBool("Idling", true);
            }

        }
    }
}