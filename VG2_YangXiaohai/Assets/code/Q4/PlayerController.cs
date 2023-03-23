using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS
{

    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;

        //Outlets
        public Transform povOrigin;
        public Transform projectileOrigin;
        public GameObject projectilePrefab;

        //Configuration
        public float attackRange;

        //State Tracking
        public List<int> keyIdsObtained;

        //Methods
        private void Awake()
        {
            instance = this;
            keyIdsObtained = new List<int>();
        }

        void OnPrimaryAttack()
        {
            RaycastHit hit;
            bool hitSomething = Physics.Raycast(povOrigin.position, povOrigin.forward, out hit, attackRange);
            if (hitSomething)
            {
                Rigidbody targetRigidbody = hit.transform.GetComponent<Rigidbody>();
                if (targetRigidbody)
                {
                    targetRigidbody.AddForce(povOrigin.forward * 100f, ForceMode.Impulse);
                }
            }
        }

        void OnSecondaryAttack()
        {
            GameObject projectile = Instantiate(projectilePrefab,
                                                projectileOrigin.position,
                                                Quaternion.LookRotation(povOrigin.forward));
            projectile.transform.localScale = Vector3.one * 5f;
            projectile.GetComponent<Rigidbody>().AddForce(povOrigin.forward * 25f, ForceMode.Impulse);
        }

        void OnInteract()
        {
            RaycastHit hit;
            if (Physics.Raycast(povOrigin.position, povOrigin.forward ,out hit, 2f))
            {

                //Doors
                Door targetDoor = hit.transform.GetComponent<Door>();
                if (targetDoor)
                {
                    targetDoor.Interact();
                }

                //Buttons
                InteractButton targetButton = hit.transform.GetComponent<InteractButton>();
                if (targetButton != null)
                {
                    targetButton.Interact();
                }
            }
        }
    }
}
