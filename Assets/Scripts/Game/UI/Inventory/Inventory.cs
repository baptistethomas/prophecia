using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject characterStats;

    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;

    public GameObject slotHolder;

    void Start()
    {
        InitSlots();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Player.Instance.inventoryEnabled = !Player.Instance.inventoryEnabled;
        }

        if (Player.Instance.inventoryEnabled == true)
        {

            // Slots logic
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
            ShowAttributes();

            // Get Current Gold Amount
            CurrentGoldAmount();
        }
        else
        {
            inventory.SetActive(false);
            characterStats.SetActive(false);
        }
    }

    private void InitSlots()
    {
        allSlots = 120;
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

    private void ShowAttributes()
    {
        GameObject strenght = GameObject.Find("Strenght");
        strenght.transform.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text =
            Player.Instance.strenghtFinal.ToString();

        GameObject endurance = GameObject.Find("Endurance");
        endurance.transform.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text =
            Player.Instance.enduranceFinal.ToString();

        GameObject dexterity = GameObject.Find("Dexterity");
        dexterity.transform.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text =
            Player.Instance.dexterityFinal.ToString();

        GameObject intelect = GameObject.Find("Intelect");
        intelect.transform.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text =
            Player.Instance.intelectFinal.ToString();

        GameObject wisdom = GameObject.Find("Wisdom");
        wisdom.transform.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text =
            Player.Instance.wisdomFinal.ToString();

        GameObject armor = GameObject.Find("Armor");
        armor.transform.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text =
            Player.Instance.armorClassFinal.ToString();
    }

    public void AddItem(GameObject itemObject, int itemId, string itemType, string itemDescription, Sprite itemIcon)
    {
        // Look for Stacking First
        for (int i = 0; i < allSlots; i++)
        {

            if (slot[i].GetComponent<Slot>().id == itemId)
            {
                AddItemSlotMapping(itemObject, itemId, itemType, itemDescription, itemIcon, i);
                return;
            }
        }

        // Look for Empty if nothing to stack
        for (int i = 0; i < allSlots; i++)
        {

            if (slot[i].GetComponent<Slot>().empty)
            {
                AddItemSlotMapping(itemObject, itemId, itemType, itemDescription, itemIcon, i);
                return;
            }
        }
    }

    public void AddItemSlotMapping(GameObject itemObject, int itemId, string itemType, string itemDescription, Sprite itemIcon, int i)
    {
        // Add Item
        itemObject.GetComponent<Item>().pickedUp = true;
        itemObject.GetComponent<Item>().equipped = false;

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

        // Make the slot active
        slot[i].SetActive(true);
    }

    public void CurrentGoldAmount()
    {
        GameObject goldInventory = GameObject.Find("Gold");
        goldInventory.transform.Find("Gold Amount").GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.gold.ToString();
    }
}
