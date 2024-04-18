using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Inventory_UI : MonoBehaviour
{
    private Player_Inputs player_Inputs;
    public GameObject inventoryGameObject;
    Inventory inventory;
    public Transform itemsParent;
    List<Inventory_Slot> slots = new List<Inventory_Slot>();
    private int numberOfSlots = 0;

    public Inventory_Slot slotPrefab;

    public TMP_Text pickedItem_Title;
    public TMP_Text pickedItem_Description;
    public Image pickedItem_Image;

    
    void Start()
    {
        inventory = Inventory.Inst;
        inventory.e_OnItemChanged += UpdateUI;
        player_Inputs = FindObjectOfType<Player_Inputs>();
    }

    void Update()
    {
        Check_Input();
        if(inventoryGameObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            GameManager.Inst.PauseGame();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.Inst.UnPauseGame();
        }
    }

    private void Check_Input()
    {
        if(Input.GetKeyDown(KeyCode.I))
        { 
            inventoryGameObject.SetActive(!inventoryGameObject.activeSelf); 
            if(inventoryGameObject.activeSelf) GetComponentInChildren<Scrollbar>().value = 1f; 
            player_Inputs.ResetAllInputs();
        }
    }

    void UpdateUI(Item item, bool isAdd, int amount)
    {
        if(isAdd)
            AddItemToSlot(item, amount);
        else 
        {
            RemoveItemFromSlot(item, amount);
            if(item == Inventory.Inst.pickeditem)
            {
                DePickItem();
            }
        }
            
       
    }

    public void PickItem(Item item)
    {
        pickedItem_Title.text = item.name;
        pickedItem_Image.sprite = item.icon;
        pickedItem_Image.gameObject.SetActive(true);
        pickedItem_Description.text = item.description;
        inventory.SetPickedItem(item);
    }

    public void DePickItem()
    {
        pickedItem_Title.text = "";
        pickedItem_Image.sprite = null;
        pickedItem_Image.gameObject.SetActive(false);
        pickedItem_Description.text = "";
        inventory.SetPickedItem(null);
    }

    public void AddItemToSlot(Item item_toAdd, int amount)
    {
        // BUG: increase number of item when item is already in List
        Inventory_Slot slot = GetItemsSlot(item_toAdd);
        if(slot != null)
        {
            slot.AddAmount(amount);
        }
        else 
        {
            numberOfSlots++;
            if(slots.Count < numberOfSlots)
            {
                Inventory_Slot newSlot =  Instantiate(slotPrefab, itemsParent.position, itemsParent.rotation, itemsParent);
                newSlot.AddItem(item_toAdd, amount);
                slots.Add(newSlot);
            } 
        }
    }

    public void RemoveItemFromSlot(Item item_toRemove, int amount)
    {
        // BUG: decrease number of item when item is still there
        Inventory_Slot slot = GetItemsSlot(item_toRemove);
        if(slot != null)
        {
            slot.AddAmount(-amount);
            if(slot.amount <= 0)
            {
                numberOfSlots--;
                slots.Remove(slot);
                Destroy(slot.gameObject);
            }
        }
    }

    private Inventory_Slot GetItemsSlot(Item item_)
    {
        Inventory_Slot item_slot = null;
        foreach (Inventory_Slot slot in slots)
        {
            if(slot.GetItem() == item_)
            {
                item_slot = slot;
            }
        }
        return item_slot;
    }

    public void btn_OnClicked_UseItem()
    {
        Inventory.Inst.UsePickedItem();
    }
}
