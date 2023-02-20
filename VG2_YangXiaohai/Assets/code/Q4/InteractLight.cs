using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class InteractLight : MonoBehaviour
    {
        public void Interact()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
