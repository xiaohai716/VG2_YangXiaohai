using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class Door : MonoBehaviour
    {
        //Outlets
        Animator animator;

        //Confuguration
        public GameObject requiredSender;
        public int keyIdRequired = -1;

        //Methods
        private void Awake()
        {
            animator = GetComponent<Animator>(); 
        }
        public void Interact(GameObject sender = null)
        {
            bool shouldOpen = false;
            if (!requiredSender)
            {
                shouldOpen = true;
            }else if(requiredSender == sender)
            {
                shouldOpen = true;
            }

            bool keyRequired = keyIdRequired >= 0;
            bool keyMissing = !PlayerController.instance.keyIdsObtained.Contains(keyIdRequired);
            if(keyRequired && keyMissing)
            {
                shouldOpen = false;
            }

            if (shouldOpen)
            {
                animator.SetTrigger("Open");
            }           
        }
    }
}
