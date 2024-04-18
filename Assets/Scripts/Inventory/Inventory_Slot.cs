using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory_Slot : MonoBehaviour
{
    private Item currentItem;
    public Image icon;

    public TMP_Text title;
    public TMP_Text amount_text;


    public int amount = 0;

    public void AddItem(Item item, int amount)
    {
        currentItem = item;
        icon.sprite = currentItem.icon;
        icon.enabled = true;
        this.amount = amount; 
        title.text = currentItem.name;
        amount_text.text = amount.ToString();
    }

    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
        amount = 0;
        title.text = "";
        amount_text.text = "";
    }

    public void AddAmount(int number)
    {
        amount += number;
        amount_text.text = amount.ToString();
        if(amount == 0)
        {
            ClearSlot();
        }
    }

    public Item GetItem()
    {
        return currentItem;
    }

    public void btn_OnClicked()
    {
        Inventory.Inst.inventory_ui.PickItem(currentItem);
    }
}
