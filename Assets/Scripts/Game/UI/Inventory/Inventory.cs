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
        InitSlots();
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
            ShowAttributes();
        }
        else
        {
            inventory.SetActive(false);
            characterStats.SetActive(false);
        }
    }

    private void InitSlots()
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

    private void ShowAttributes()
    {
        GameObject strenght = GameObject.Find("Strenght");
        strenght.transform.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text =
            Player.Instance.strenghtFinal.ToString() + "/" + Player.Instance.strenght.ToString();

        GameObject endurance = GameObject.Find("Endurance");
        endurance.transform.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text =
            Player.Instance.enduranceFinal.ToString() + "/" + Player.Instance.endurance.ToString();

        GameObject dexterity = GameObject.Find("Dexterity");
        dexterity.transform.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text =
            Player.Instance.dexterityFinal.ToString() + "/" + Player.Instance.dexterity.ToString();

        GameObject intelect = GameObject.Find("Intelect");
        intelect.transform.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text =
            Player.Instance.intelectFinal.ToString() + "/" + Player.Instance.intelect.ToString();

        GameObject wisdom = GameObject.Find("Wisdom");
        wisdom.transform.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text =
            Player.Instance.wisdomFinal.ToString() + "/" + Player.Instance.wisdom.ToString();
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
