using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class KeyItem : MonoBehaviour
    {
        //Configuration
        public int id;

        //Methods
        private void OnTriggerEnter(Collider other)
        {
            PlayerController targetPlayer = other.GetComponent<PlayerController>();
            if(targetPlayer != null)
            {
                targetPlayer.keyIdsObtained.Add(id);
                Destroy(gameObject);
            }
        }
    }
}