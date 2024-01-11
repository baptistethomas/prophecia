using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public GameObject item;
    public int id;
    public string type;
    public string description;
    public bool empty;

    public Transform slotIconGO;
    public Sprite icon;

    private float lastClick = 0;
    private float interval = 0.5f;
    private GameObject slotGameObject;
    private GameObject itemBackToInventoryGameObject;
    private GameObject leftHand;
    private GameObject rightHand;

    private void Start()
    {
        slotIconGO = transform.GetChild(0);
    }

    public void UpdateSlot()
    {
        slotIconGO.GetComponent<Image>().sprite = icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Equip Item on double click inventory slot
        if ((lastClick + interval) > Time.time)
        {
            // slot Game Object
            slotGameObject = transform.GetChild(transform.childCount - 1).gameObject;

            // Weapon
            equipWeaponSlot();

            // Armor part
            equipArmorSlot();

            // Reset Player Target
            Player.Instance.resetTarget();
        }
        else
        {
            lastClick = Time.time;
        }
    }

    private void equipArmorSlot()
    {
        if (slotGameObject.GetComponent<Armor>())
        {
            putAwayArmorPart();
            equipArmorPart();
        }
    }
    private void equipWeaponSlot()
    {
        if (slotGameObject.GetComponent<Weapon>() && slotGameObject.GetComponent<Weapon>().isRange == true)
        {
            putAwayLeftHand();
            putAwayRightHand();
            equipLeftHandWeapon();
        }
        if (slotGameObject.GetComponent<Weapon>() && slotGameObject.GetComponent<Weapon>().isMelee == true)
        {
            putAwayLeftHand();
            putAwayRightHand();
            equipRightHandWeapon();
        }
    }

    private void putAwayArmorPart()
    {
        GameObject armorSet = GameObject.Find(slotGameObject.GetComponent<Armor>().category);
        if (armorSet != null)
        {
            for (int i = 0; i < armorSet.transform.childCount; i++)
            {
                // Remove Part Armor Active Object
                if (armorSet.transform.GetChild(i) && armorSet.transform.GetChild(i).GetComponent<Armor>().bodyPart == slotGameObject.GetComponent<Armor>().bodyPart)
                {
                    armorSet.transform.GetChild(i).gameObject.SetActive(false);
                }

                // Disable body part slot on character equipment
                GameObject armorSlot = GameObject.Find("Slot_" + slotGameObject.GetComponent<Armor>().bodyPart);
                armorSlot.transform.GetChild(1).gameObject.SetActive(false);

                // Back in inventory
                if (armorSlot.transform.childCount == 3) resetEquipedSlot(armorSlot);
            }
        }
    }

    private void equipArmorPart()
    {
        GameObject armorSet = GameObject.Find(slotGameObject.GetComponent<Armor>().category);
        if (armorSet != null)
        {
            for (int i = 0; i < armorSet.transform.childCount; i++)
            {
                var armorPart = armorSet.transform.GetChild(i);
                if (armorPart.GetComponent<Item>().id == slotGameObject.GetComponent<Item>().id)
                {
                    // Armor Part is equiped
                    armorPart.gameObject.SetActive(true);
                    armorPart.GetComponent<Item>().equipped = true;

                    // Activate the armor part slot on character equipment
                    GameObject armorPartSlot = GameObject.Find("Slot_" + slotGameObject.GetComponent<Armor>().bodyPart);
                    armorPartSlot.transform.GetChild(1).gameObject.SetActive(true);
                    armorPartSlot.transform.GetChild(1).GetComponent<Image>().sprite = icon;
                    GameObject armor = new GameObject(slotGameObject.name);

                    // Disabled skin mesh from player matching with armor parts
                    if (GameObject.Find(slotGameObject.GetComponent<Armor>().bodyPart)) GameObject.Find(slotGameObject.GetComponent<Armor>().bodyPart).SetActive(false);
                    if (slotGameObject.GetComponent<Armor>().bodyPart == "Chest") GameObject.Find("Arms").SetActive(false); // Chest need arms being disabled too
                    if (slotGameObject.GetComponent<Armor>().bodyPart == "Legs") GameObject.Find("Underwear").SetActive(false); // Legs need underwear being disabled too

                    // Copy armor part to equiped slot
                    armor = slotGameObject;
                    armor.transform.SetParent(armorPartSlot.transform);

                    // Adding Equipment Bonus & Malus
                    Player.Instance.armorClassBuff += slotGameObject.GetComponent<Armor>().armorClass;
                    Player.Instance.dodgeBuff += slotGameObject.GetComponent<Armor>().bonusDodge;
                    Player.Instance.dodgeMalus += slotGameObject.GetComponent<Armor>().malusDodge;
                    Player.Instance.encombrement += slotGameObject.GetComponent<Armor>().encombrement;

                    // Clean Inventory Slot
                    resetInventorySlot();
                }
            }
        }
    }

    private void putAwayLeftHand()
    {
        leftHand = GameObject.Find("LEFT_HAND_COMBAT");

        for (int i = 0; i < leftHand.transform.childCount; i++)
        {
            // Left hand is equiped
            var leftHandWeapon = leftHand.transform.GetChild(i);
            leftHandWeapon.gameObject.SetActive(false);

            // Disable the right hand slot on character inventory
            GameObject leftHandSlot = GameObject.Find("Left Hand");
            leftHandSlot.transform.GetChild(1).gameObject.SetActive(false);

            // Back in inventory
            if (leftHandSlot.transform.childCount == 3) resetEquipedSlot(leftHandSlot);
        }
    }

    private void putAwayRightHand()
    {
        rightHand = GameObject.Find("RIGHT_HAND_COMBAT");

        for (int i = 0; i < rightHand.transform.childCount; i++)
        {
            // Put away weapon in right hand
            var rightHandWeapon = rightHand.transform.GetChild(i);
            rightHandWeapon.gameObject.SetActive(false);

            // Disable the right hand slot on character inventory
            GameObject rightHandSlot = GameObject.Find("Right Hand");
            rightHandSlot.transform.GetChild(1).gameObject.SetActive(false);

            // Back in inventory
            if (rightHandSlot.transform.childCount == 3) resetEquipedSlot(rightHandSlot);
        }
    }

    private void equipLeftHandWeapon()
    {
        leftHand = GameObject.Find("LEFT_HAND_COMBAT");

        // Range Weapon are equiped on right hand
        if (slotGameObject.GetComponent<Weapon>().isRange)
        {
            var childCound = leftHand.transform.childCount;
            for (int i = 0; i < childCound; i++)
            {
                var leftHandWeapon = leftHand.transform.GetChild(i);
                if (leftHandWeapon.GetComponent<Item>().id == slotGameObject.GetComponent<Item>().id)
                {
                    // Left hand is equiped
                    leftHandWeapon.gameObject.SetActive(true);
                    Player.Instance.weapon = slotGameObject.GetComponent<Weapon>();
                    leftHandWeapon.GetComponent<Item>().equipped = true;

                    // Activate the left hand slot on character inventory
                    GameObject leftHandSlot = GameObject.Find("Left Hand");
                    leftHandSlot.transform.GetChild(1).gameObject.SetActive(true);
                    leftHandSlot.transform.GetChild(1).GetComponent<Image>().sprite = icon;
                    GameObject leftWeapon = new GameObject(slotGameObject.name);

                    // Copy left weapon object to equiped slot
                    leftWeapon = slotGameObject;
                    leftWeapon.transform.SetParent(leftHandSlot.transform);

                    // Clean Inventory Slot
                    resetInventorySlot();
                }
            }
        }
    }

    private void equipRightHandWeapon()
    {
        rightHand = GameObject.Find("RIGHT_HAND_COMBAT");

        // Melee Weapon are equiped on right hand
        if (slotGameObject.GetComponent<Weapon>().isMelee)
        {
            var childCound = rightHand.transform.childCount;
            for (int i = 0; i < childCound; i++)
            {
                // A bit different of left hand, cuz right hand could be punch weapon without item component
                var rightHandWeapon = rightHand.transform.GetChild(i);
                var rightHandWeaponItem = rightHandWeapon.GetComponent<Item>();
                if (rightHandWeaponItem && rightHandWeaponItem.id == slotGameObject.GetComponent<Item>().id)
                {
                    // Right hand is equiped
                    rightHandWeapon.gameObject.SetActive(true);
                    Player.Instance.weapon = slotGameObject.GetComponent<Weapon>();
                    rightHandWeapon.GetComponent<Item>().equipped = true;

                    // Activate the right hand slot on character inventory
                    GameObject rightHandSlot = GameObject.Find("Right Hand");
                    rightHandSlot.transform.GetChild(1).gameObject.SetActive(true);
                    rightHandSlot.transform.GetChild(1).GetComponent<Image>().sprite = icon;
                    GameObject rightWeapon = new GameObject(slotGameObject.name);

                    // Copy left weapon object to equiped slot
                    rightWeapon = slotGameObject;
                    rightWeapon.transform.SetParent(rightHandSlot.transform);

                    // Clean Inventory Slot
                    resetInventorySlot();

                }
            }
        }
    }

    private void resetInventorySlot()
    {
        // Clean Slot by inactive used one to equip an generate a new empty one
        GameObject slotRef = GameObject.Find("Slot Ref");
        GameObject slot = Instantiate(slotRef);
        slot.name = "Slot";
        slot.transform.SetParent(transform.parent);
        empty = true;
        gameObject.SetActive(false);
    }

    private void resetEquipedSlot(GameObject handSlot)
    {
        itemBackToInventoryGameObject = handSlot.transform.GetChild(2).gameObject;
        Item item = itemBackToInventoryGameObject.GetComponent<Item>();
        Player.Instance.GetComponent<Inventory>().AddItem(itemBackToInventoryGameObject, item.id, item.type, item.description, item.icon);
    }

}
