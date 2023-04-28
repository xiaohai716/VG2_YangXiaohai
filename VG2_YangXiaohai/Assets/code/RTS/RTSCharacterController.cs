using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public class RTSCharacterController : MonoBehaviour
    {
        // Outlets
        Animator animator;
        NavMeshAgent navAgent;
        public GameObject selectionIndicator;
        public GameObject attackTarget;
        public GameObject projectilePrefab;

        //Configuration
        public float attackDistance;
        public float attackDelay;

        //State Tracking
        float _timeSinceLastAttack;

        //Methods
        public void Select()
        {
            selectionIndicator.SetActive(true);
        }

        public void Deselect()
        {
            selectionIndicator.SetActive(false);
        }

        void Start()
        {
            animator = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            animator.SetFloat("velocity", navAgent.velocity.magnitude);

            _timeSinceLastAttack += Time.deltaTime;

            if (attackTarget)
            {
                SetDestination(attackTarget.transform.position);
                if(Vector3.Distance(attackTarget.transform.position, transform.position) <= attackDistance)
                {
                    if(_timeSinceLastAttack >= attackDelay)
                    {
                        _timeSinceLastAttack = 0;

                        Vector3 projectileOrigin = transform.position + new Vector3(0, 2f, 0);
                        Vector3 directionToTarget = (attackTarget.transform.position - projectileOrigin).normalized;

                        GameObject projectile = Instantiate(
                            projectilePrefab,
                            projectileOrigin,
                            Quaternion.LookRotation(directionToTarget)
                        );

                        projectile.transform.localScale = Vector3.one * 10f;
                        projectile.GetComponent<Rigidbody>().AddForce(directionToTarget * 50f, ForceMode.Impulse);

                        Destroy(projectile, 10f);
                    }
                }
            }
        }

        public void SetDestination(Vector3 targetPosition)
        {
            navAgent.destination = targetPosition;
        }

        public void SetTarget(GameObject target)
        {
            attackTarget = target;
        }

    }
}
