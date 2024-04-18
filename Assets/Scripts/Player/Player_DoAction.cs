using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DoAction : MonoBehaviour
{
    private Player_Inputs player_Inputs;
    [HideInInspector] public bool hasInteractableTarget = false; // info: used by player_interaction
    public LayerMask ignoreLayerMask;
    public Interactable interaction_Target;
    private Interactable interactableChecker;
    private bool interaction_TargetIsStillInArea  = false;
    public Transform interactionAreaCenter;
    public Vector3 iAreaSize;
    private Collider[] collidersInIArea;

    public Transform camera_gameobject;

    private void Start()
    {
        player_Inputs = GetComponent<Player_Inputs>();
    }

    private void Update()
    {
        Check_Input();
        CheckForInteractables();
    }

    private void Check_Input()
    {
        if(player_Inputs.Btn_E)
        {
            DoAction();
        }
    }

    private void CheckForInteractables()
    {
        collidersInIArea = Physics.OverlapBox(interactionAreaCenter.position, iAreaSize, interactionAreaCenter.rotation, ignoreLayerMask);
        interaction_TargetIsStillInArea = false;
        foreach (var item in collidersInIArea)
        {
            interactableChecker = item.GetComponent<Interactable>();
            if(interactableChecker != null)
            {
                interaction_TargetIsStillInArea = true;
                interaction_Target = interactableChecker;
                hasInteractableTarget  = true;
            }
        }

        if(interaction_Target != null && !interaction_TargetIsStillInArea)
        {
            interaction_Target = null;
            hasInteractableTarget = false;
        }
    }

    public void DoAction()
    {
        if(interaction_Target != null)
        {
            Debug.Log("INTERACT IS CALLEd");
            interaction_Target.Interact();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(interactionAreaCenter.position, iAreaSize);
    }
}
