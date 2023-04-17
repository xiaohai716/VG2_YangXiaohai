using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RTS
{
    public class RTSGameController : MonoBehaviour
    {
        // State Tracking
        public GameObject currentSelection;

        // Update is called once per frame
        void Update()
        {
            Mouse mouse = Mouse.current;
            if(mouse != null)
            {
                if (mouse.leftButton.wasPressedThisFrame)
                {

                    Ray selectionRaycast = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
                    RaycastHit[] hits = Physics.RaycastAll(selectionRaycast);

                    currentSelection = null;
                    foreach(RaycastHit hit in hits)
                    {
                        if (hit.collider.GetComponent<RTSCharacterController>())
                        {
                            currentSelection = hit.collider.gameObject;
                        }
                    }
                }

                if (mouse.rightButton.wasPressedThisFrame && currentSelection)
                {
                    RTSCharacterController character = currentSelection.GetComponent<RTSCharacterController>();
                    if (character)
                    {
                        Ray selectionRaycast = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
                        RaycastHit[] hits = Physics.RaycastAll(selectionRaycast);

                        foreach (RaycastHit hit in hits)
                        {
                            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) 
                            {
                                character.SetDestination(hit.point);
                            }
                        }
                    }
                }
            }
        }
    }
}
