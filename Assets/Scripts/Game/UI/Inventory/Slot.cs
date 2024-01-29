using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject item;
    private GameObject slotContent;
    private GameObject leftHand;
    private GameObject rightHand;
    public int id;
    public string type;
    public string description;
    public Sprite icon;
    public bool empty;
    private float lastClick = 0;
    private float interval = 0.5f;
    private GameObject labelItemGo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!labelItemGo)
        {
            int slotContentCountChild = eventData.pointerEnter.transform.parent.childCount;
            GameObject slotGameObject = eventData.pointerEnter.transform.parent.GetChild(slotContentCountChild - 1).gameObject;
            ShowDescriptionItem(slotGameObject.gameObject);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(labelItemGo);
    }

    private void ShowDescriptionItem(GameObject itemGameObject)
    {
        GameObject ui = GameObject.Find("UI").gameObject;

        if (ui != null)
        {
            GameObject canvas = ui.transform.Find("Canvas").gameObject;
            if (canvas != null)
            {
                GameObject labelItem = canvas.transform.Find("Item Description").gameObject;
                if (labelItem != null)
                {
                    if (!labelItemGo)
                    {
                        labelItemGo = Instantiate(labelItem, transform.position, Quaternion.identity);
                        labelItemGo.transform.SetParent(canvas.transform);
                        labelItemGo.transform.position = new Vector3(transform.position.x, transform.position.y - 125, transform.position.z);
                        GameObject backgroundItemLabel = labelItemGo.transform.Find("Background").gameObject;
                        if (backgroundItemLabel != null)
                        {
                            GameObject descriptionItem = backgroundItemLabel.transform.Find("Description").gameObject;
                            if (descriptionItem != null)
                            {
                                Item item = itemGameObject.GetComponent<Item>();
                                TextMeshProUGUI labelText = descriptionItem.GetComponent<TextMeshProUGUI>();
                                labelText.text = DescriptionItemContent(item);
                                float scaleFactor = Screen.width / 1920f;
                                Vector2 labelTextDimensions = labelText.GetPreferredValues();
                                backgroundItemLabel.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(labelTextDimensions.x + 16 * scaleFactor, 200 * scaleFactor);
                                labelItemGo.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
    }

    private string DescriptionItemContent(Item item)
    {
        string text =
            "<smallcaps>" + item.name + "</smallcaps>\n\n" +
            "<size=\"14\">" + item.descriptionShort + "</size>\n" +
            "<size=\"14\">Sell price : " + item.sellPrice + "</size>\n";
        return text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Equip Item on double click inventory slot
        if ((lastClick + interval) > Time.time)
        {
            EquipSlot(this);
        }
        else
        {
            lastClick = Time.time;
        }
    }

    public void EquipSlot(Slot slot)
    {
        // Get Item Object from Inventory
        int slotContentCountChild = slot.transform.childCount;
        GameObject slotContentGameObject = slot.transform.GetChild(slotContentCountChild - 1).gameObject;
        Weapon weapon = slotContentGameObject.GetComponent<Weapon>();
        Armor armor = slotContentGameObject.GetComponent<Armor>();

        // Weapon
        if (weapon != null)
        {
            // Check requirements
            if (weapon.requiredStrenght > Player.Instance.strenght) return; // To do : Add log message
            if (weapon.requiredEndurance > Player.Instance.endurance) return; // To do : Add log message
            if (weapon.requiredDexterity > Player.Instance.dexterity) return; // To do : Add log message
            if (weapon.requiredIntelect > Player.Instance.intelect) return; // To do : Add log message
            if (weapon.requiredWisdom > Player.Instance.wisdom) return; // To do : Add log message

            // Equip if requirements are passed
            EquipWeaponSlot(weapon);
        }

        // Armor part
        if (armor != null)
        {
            // Check requirements
            if (armor.requiredStrenght > Player.Instance.strenght) return; // To do : Add log message
            if (armor.requiredEndurance > Player.Instance.endurance) return; // To do : Add log message
            if (armor.requiredDexterity > Player.Instance.dexterity) return; // To do : Add log message
            if (armor.requiredIntelect > Player.Instance.intelect) return; // To do : Add log message
            if (armor.requiredWisdom > Player.Instance.wisdom) return; // To do : Add log message

            // Equip if requirements are passed
            EquipArmorSlot(armor);
        }

        // Reset Player Target
        Player.Instance.ResetTarget();

        // Reset Slot Status
        ResetSlot(slot);
    }

    private void ResetSlot(Slot slot)
    {
        slot.item = null;
        slot.id = 0;
        slot.type = null;
        slot.description = null;
        slot.icon = null;
        slot.empty = true;
    }

    public void EquipArmorSlot(Armor armor)
    {
        PutAwayArmorPart(armor);
        EquipArmorPart(armor);
    }

    public void EquipWeaponSlot(Weapon weapon)
    {
        if (weapon.isRange == true)
        {
            PutAwayLeftHand();
            PutAwayRightHand();
            EquipLeftHandWeapon(weapon);
        }
        if (weapon.isMelee == true)
        {
            PutAwayLeftHand();
            PutAwayRightHand();
            EquipRightHandWeapon(weapon);
        }
        return;
    }

    public void EquipArmorPart(Armor armor)
    {
        GameObject armorSet = GameObject.Find(armor.category);
        if (armorSet != null)
        {
            for (int i = 0; i < armorSet.transform.childCount; i++)
            {
                var armorPart = armorSet.transform.GetChild(i);
                if (armorPart.GetComponent<Item>().id == armor.gameObject.GetComponent<Item>().id)
                {
                    // Armor Part is equiped
                    armorPart.gameObject.SetActive(true);

                    // Get the Armor Part Slot
                    GameObject armorPartSlot = GameObject.Find("Slot_" + armor.bodyPart);

                    // If we try to re equip already equiped armor part, we return
                    if (armorPartSlot != null & armorPartSlot.transform.childCount == 3 && armorPartSlot.transform.GetChild(2).GetComponent<Item>().id == armor.gameObject.GetComponent<Item>().id) return;

                    // Activate the armor part slot on character equipment                 
                    armorPartSlot.transform.GetChild(1).gameObject.SetActive(true);
                    armorPartSlot.transform.GetChild(1).GetComponent<Image>().sprite = armor.gameObject.GetComponent<Item>().icon;

                    // Disabled skin mesh from player matching with armor parts
                    if (GameObject.Find(armor.bodyPart)) GameObject.Find(armor.bodyPart).GetComponent<Renderer>().enabled = false;
                    if (armor.bodyPart == "Chest") Player.Instance.body.transform.Find("Arms").GetComponent<Renderer>().enabled = false; // Chest need arms being disabled too
                    if (armor.bodyPart == "Legs")
                    {
                        // Legs need underwear & legs being disabled too
                        Player.Instance.accessories.transform.Find("Underwear").GetComponent<Renderer>().enabled = false;
                        Player.Instance.body.transform.Find("Legs").GetComponent<Renderer>().enabled = false;
                    }

                    // Copy armor part to equiped slot
                    armor.transform.SetParent(armorPartSlot.transform);
                    armor.GetComponent<Item>().equipped = true;

                    // Adding Equipment Bonus & Malus
                    Player.Instance.armorClassBuff += armor.armorClass;
                    Player.Instance.dodgeBuff += armor.bonusDodge;
                    Player.Instance.dodgeMalus += armor.malusDodge;

                    // Clean Inventory Slot
                    UpdateInventorySlot();
                }
            }
        }
    }

    public void PutAwayArmorPart(Armor armor)
    {
        GameObject armorSet = GameObject.Find(armor.category);
        if (armorSet != null)
        {
            for (int i = 0; i < armorSet.transform.childCount; i++)
            {
                // Remove Part Armor Active Object
                if (armorSet.transform.GetChild(i) && armorSet.transform.GetChild(i).GetComponent<Armor>().bodyPart == armor.bodyPart)
                {

                    // If we try to re equip already equiped armor part, we return
                    GameObject armorSlot = GameObject.Find("Slot_" + armor.bodyPart);

                    // Prevent re equip same
                    if (armorSlot.transform.childCount == 3 && armorSlot.transform.GetChild(2).GetComponent<Item>().id == armor.gameObject.GetComponent<Item>().id) return;

                    // Disabled Equiped Armor Slot
                    if (armorSlot.transform.GetChild(1)) armorSlot.transform.GetChild(1).gameObject.SetActive(false);

                    // Disable Armor
                    armorSet.transform.GetChild(i).gameObject.SetActive(false);

                    // Back in inventory
                    if (armorSlot.transform.childCount == 3) ResetEquipedSlot(armorSlot);
                }
            }
        }
    }

    public void PutAwayLeftHand()
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
            if (leftHandSlot.transform.childCount == 3) ResetEquipedSlot(leftHandSlot);
        }
    }

    public void PutAwayRightHand()
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
            if (rightHandSlot.transform.childCount == 3) ResetEquipedSlot(rightHandSlot);
        }
    }

    public void EquipLeftHandWeapon(Weapon weapon)
    {
        leftHand = GameObject.Find("LEFT_HAND_COMBAT");

        // Range Weapon are equiped on right hand
        if (weapon.isRange)
        {
            var childCound = leftHand.transform.childCount;
            for (int i = 0; i < childCound; i++)
            {
                var leftHandWeapon = leftHand.transform.GetChild(i);
                if (leftHandWeapon.GetComponent<Item>().id == weapon.gameObject.GetComponent<Item>().id)
                {
                    // Left hand is equiped
                    leftHandWeapon.gameObject.SetActive(true);
                    Player.Instance.weapon = weapon;
                    leftHandWeapon.GetComponent<Item>().equipped = true;

                    // Activate the left hand slot on character inventory
                    GameObject leftHandSlot = GameObject.Find("Left Hand");
                    leftHandSlot.transform.GetChild(1).gameObject.SetActive(true);
                    leftHandSlot.transform.GetChild(1).GetComponent<Image>().sprite = weapon.gameObject.GetComponent<Item>().icon;

                    // Copy left weapon object to equiped slot
                    GameObject leftWeapon = weapon.gameObject;
                    leftWeapon.transform.SetParent(leftHandSlot.transform);

                    // Clean Inventory Slot
                    UpdateInventorySlot();
                }
            }
        }
    }

    public void EquipRightHandWeapon(Weapon weapon)
    {
        rightHand = GameObject.Find("RIGHT_HAND_COMBAT");

        // Melee Weapon are equiped on right hand
        if (weapon.isMelee)
        {
            var childCound = rightHand.transform.childCount;
            for (int i = 0; i < childCound; i++)
            {
                // A bit different of left hand, cuz right hand could be punch weapon without item component
                var rightHandWeapon = rightHand.transform.GetChild(i);
                var rightHandWeaponItem = rightHandWeapon.GetComponent<Item>();
                if (rightHandWeaponItem && rightHandWeaponItem.id == weapon.gameObject.GetComponent<Item>().id)
                {
                    // Right hand is equiped
                    rightHandWeapon.gameObject.SetActive(true);
                    Player.Instance.weapon = weapon;
                    rightHandWeapon.GetComponent<Item>().equipped = true;

                    // Activate the right hand slot on character inventory
                    GameObject rightHandSlot = GameObject.Find("Right Hand");
                    rightHandSlot.transform.GetChild(1).gameObject.SetActive(true);
                    rightHandSlot.transform.GetChild(1).GetComponent<Image>().sprite = weapon.gameObject.GetComponent<Item>().icon;

                    // Copy left weapon object to equiped slot
                    GameObject rightWeapon = weapon.gameObject;
                    rightWeapon.transform.SetParent(rightHandSlot.transform);

                    // Clean Inventory Slot
                    UpdateInventorySlot();

                }
            }
        }
    }

    public void UpdateInventorySlot()
    {
        // Check the quantity of the item that we equiped
        transform.Find("Count").GetComponent<TMPro.TextMeshProUGUI>().text = (Int32.Parse(transform.Find("Count").GetComponent<TMPro.TextMeshProUGUI>().text) - 1).ToString();

        // If this is 0, we clean the slot
        if (Int32.Parse(transform.Find("Count").GetComponent<TMPro.TextMeshProUGUI>().text) == 0)
        {

            transform.GetChild(0).GetComponent<Image>().enabled = false;
            //transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        }
    }

    public void ResetEquipedSlot(GameObject slotEquiped)
    {
        if (slotEquiped.GetComponent<Armor>() != null)
        {
            // Remove Equipment Bonus & Malus
            Player.Instance.armorClassBuff -= slotEquiped.GetComponent<Armor>().armorClass;
            Player.Instance.dodgeBuff -= slotEquiped.GetComponent<Armor>().bonusDodge;
            Player.Instance.dodgeMalus -= slotEquiped.GetComponent<Armor>().malusDodge;
        }

        // Back to Inventory
        Item item = slotEquiped.transform.GetChild(2).gameObject.GetComponent<Item>();
        GameManager.Instance.GetComponent<Inventory>().AddItem(item.gameObject, item.id, item.type, item.name, item.descriptionShort, item.descriptionFull, item.icon);
    }
}
