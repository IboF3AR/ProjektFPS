using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class IAC_Lock : MonoBehaviour
{
    private Interactable interactable;
    public Item neededKey;

    private bool hasBeenUnlocked  = false;

    public MonoBehaviour[] interactionsToUnlock;

    public string interactableDescriptionAfterUnlock;

    public string onFailText = "Benötigt: ";

    public bool hasUnlockSound = false;

    public AudioSource unlockSound; // ignore if hasUnlockSound is false



    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.e_Interact += PerformAction; 
    }

    public void PerformAction()
    {
        if(!hasBeenUnlocked)
        {
            if(Inventory.Inst.items.ContainsKey(neededKey))
            {
                foreach (var item in interactionsToUnlock)
                {
                    item.enabled = true;
                    // info: MonoBehaviours start is called after enabled;
                    interactable.e_Interact -= PerformAction;
                    interactable.description = interactableDescriptionAfterUnlock;
                    interactable.deactivateAfterUsed = true;
                    neededKey.Use();
                    if(hasUnlockSound)
                    {
                        unlockSound.Play();
                    }
                }
            }
            else
            {
                GameManager.Inst.SetMessage(1.5f,onFailText + " \n" +  neededKey.name);
            }
        }
    }
}
