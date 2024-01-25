using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject characterStats;
    public GameObject slotHolder;
    private GameObject[] slotContent;
    private int allSlots;

    // Player Instance
    private static Inventory _instance;
    public static Inventory instance;
    public static Inventory Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        // Instance Singleton, Player is used in Monster Class
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        //DontDestroyOnLoad(this.gameObject);
        InitSlots();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && Player.Instance.isFocusedChat == false)
        {
            GameManager.Instance.inventoryEnabled = !GameManager.Instance.inventoryEnabled;
            GameManager.Instance.CloseUIWindow();
            Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
        }

        if (GameManager.Instance.inventoryEnabled == true)
        {
            Player.Instance.animator.SetBool("run", false);
            inventory.SetActive(true);
            characterStats.SetActive(true);
            ShowAttributes();
            CurrentCountOfItems();
            CurrentGoldAmount();
            CurrentEncumbrance();
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
        slotContent = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slotContent[i] = slotHolder.transform.GetChild(i).Find("Slot Content").gameObject;
            if (slotContent[i].GetComponent<Slot>().item == null)
            {
                slotContent[i].GetComponent<Slot>().empty = true;
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

    public void AddItem(GameObject itemObject, int itemId, string itemType, string name, string itemDescriptionShort, string itemDescriptionFull, Sprite itemIcon)
    {
        // Look for Stacking First
        for (int i = 0; i < allSlots; i++)
        {
            if (slotContent[i].GetComponent<Slot>().id == itemId)
            {
                AddItemSlotMapping(itemObject, itemId, itemType, name, itemDescriptionShort, itemDescriptionFull, itemIcon, i);
                return;
            }
        }

        // Look for Empty if nothing to stack
        for (int i = 0; i < allSlots; i++)
        {

            if (slotContent[i].GetComponent<Slot>().empty)
            {
                AddItemSlotMapping(itemObject, itemId, itemType, name, itemDescriptionShort, itemDescriptionFull, itemIcon, i);
                return;
            }
        }
    }

    public void AddItemSlotMapping(GameObject itemObject, int itemId, string itemType, string name, string itemDescriptionShort, string itemDescriptionFull, Sprite itemIcon, int i)
    {
        // Add Item
        itemObject.GetComponent<Item>().pickedUp = true;
        itemObject.GetComponent<Item>().equipped = false;

        // Mapping Item Data
        slotContent[i].GetComponent<Slot>().item = itemObject;
        slotContent[i].GetComponent<Slot>().icon = itemIcon;
        slotContent[i].GetComponent<Slot>().type = itemType;
        slotContent[i].GetComponent<Slot>().id = itemId;
        slotContent[i].GetComponent<Slot>().description = itemDescriptionFull;

        // Changing Slot Image
        if (slotContent[i].transform.Find("Panel"))
        {
            slotContent[i].transform.Find("Panel").GetComponent<Image>().sprite = itemIcon;
            slotContent[i].transform.Find("Panel").GetComponent<Image>().enabled = true;
        }

        // Item got sloted on inventory, he is not active anymore
        itemObject.transform.SetParent(slotContent[i].transform);
        itemObject.SetActive(false);

        // Slot isnt avalaible anymore
        slotContent[i].GetComponent<Slot>().empty = false;

        // Make the slot active
        slotContent[i].SetActive(true);
    }

    public void ConsumeItem(int itemId, string itemType)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slotContent[i].GetComponent<Slot>().id == itemId && itemType == "Consumable")
            {
                if (slotContent[i].transform.childCount > 2)
                {
                    Transform transformItemToConsume = slotContent[i].transform.GetChild(2);
                    if (transformItemToConsume != null)
                    {
                        transformItemToConsume.gameObject.GetComponent<Item>().ItemUsage();
                        Destroy(transformItemToConsume.gameObject);
                        return;
                    }

                }
            }
        }
    }

    public void CurrentGoldAmount()
    {
        GameObject goldInventory = GameObject.Find("Gold");
        goldInventory.transform.Find("Gold Amount").GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.gold.ToString();
    }

    public void CurrentEncumbrance()
    {
        GameObject encumbranceInventory = GameObject.Find("Encumbrance");
        encumbranceInventory.transform.Find("Encumbrance Amount").GetComponent<TMPro.TextMeshProUGUI>().text =
            Player.Instance.currentEncumbrance.ToString() + " / " + Player.Instance.encumbrance.ToString();
    }

    public void CurrentCountOfItems()
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slotHolder.transform.GetChild(i) != null && slotHolder.transform.GetChild(i).Find("Slot Content") != null)
            {
                slotContent[i] = slotHolder.transform.GetChild(i).Find("Slot Content").gameObject;
                // Changing Slot Count
                if (slotContent[i].transform.Find("Count"))
                {
                    int countItem = slotContent[i].transform.childCount - 2;
                    if (countItem > 0)
                    {
                        slotContent[i].transform.Find("Count").GetComponent<TMPro.TextMeshProUGUI>().text = countItem.ToString();
                        slotContent[i].transform.Find("Count").GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
                        slotContent[i].transform.Find("Panel").GetComponent<Image>().enabled = true;
                    }
                    else
                    {
                        slotContent[i].transform.Find("Count").GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
                        slotContent[i].transform.Find("Panel").GetComponent<Image>().enabled = false;
                    }

                }
            }
        }
    }
}
