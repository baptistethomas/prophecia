using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject characterStats;
    private bool inventoryEnabled;

    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;

    public GameObject slotHolder;

    void Start()
    {
        allSlots = 128;
        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
        }

        if (inventoryEnabled == true)
        {
            for (int i = 0; i < allSlots; i++)
            {
                if (slot[i].transform.Find("Count"))
                {
                    int countItem = slot[i].transform.childCount - 2;
                    if (countItem > 0) slot[i].transform.Find("Count").GetComponent<TMPro.TextMeshProUGUI>().text = countItem.ToString();
                }
            }

            inventory.SetActive(true);
            characterStats.SetActive(true);
        }
        else
        {

            inventory.SetActive(false);
            characterStats.SetActive(false);
        }
    }

    public void AddItem(GameObject itemObject, int itemId, string itemType, string itemDescription, Sprite itemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {

            if (slot[i].GetComponent<Slot>().empty || slot[i].GetComponent<Slot>().id == itemId)
            {
                // Add Item
                itemObject.GetComponent<Item>().pickedUp = true;

                // Mapping Item Data
                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().icon = itemIcon;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().id = itemId;
                slot[i].GetComponent<Slot>().description = itemDescription;

                // Item got sloted on inventory, he is not active anymore
                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                // Slot isnt avalaible anymore
                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;

                return;
            }
        }
    }
}
