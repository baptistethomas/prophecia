using UnityEngine;
using UnityEngine.EventSystems;

public class SlotEquiped : MonoBehaviour, IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {
        // Equip Item on double click inventory slot
        if ((lastClick + interval) > Time.time)
        {
            // slot Game Object
            slotGameObject = transform.GetChild(transform.childCount - 1).gameObject;

            if (slotGameObject.GetComponent<Armor>() != null)
            {

                // Remove Visual Armor from Character
                GameObject armorSet = GameObject.Find(slotGameObject.GetComponent<Armor>().category);
                if (armorSet != null)
                {
                    for (int i = 0; i < armorSet.transform.childCount; i++)
                    {
                        // Remove Part Armor Active Object
                        if (armorSet.transform.GetChild(i) && armorSet.transform.GetChild(i).GetComponent<Armor>().bodyPart == slotGameObject.GetComponent<Armor>().bodyPart)
                        {
                            // Disable Armor
                            armorSet.transform.GetChild(i).gameObject.SetActive(false);

                            // Active Skin Mesh
                            if (GameObject.Find(slotGameObject.GetComponent<Armor>().bodyPart)) GameObject.Find(slotGameObject.GetComponent<Armor>().bodyPart).GetComponent<Renderer>().enabled = true;
                            if (slotGameObject.GetComponent<Armor>().bodyPart == "Chest") GameObject.Find("Arms").GetComponent<Renderer>().enabled = true; // Chest need arms being active too
                            if (slotGameObject.GetComponent<Armor>().bodyPart == "Legs") GameObject.Find("Underwear").GetComponent<Renderer>().enabled = true; // Legs need underwear active disabled too

                            // Disable Slot Equipment
                            GameObject armorSlot = GameObject.Find("Slot_" + slotGameObject.GetComponent<Armor>().bodyPart);
                            if (armorSlot.transform.GetChild(1)) armorSlot.transform.GetChild(1).gameObject.SetActive(false);

                            // Adding Equipment Bonus & Malus
                            Player.Instance.armorClassBuff -= slotGameObject.GetComponent<Armor>().armorClass;
                            Player.Instance.dodgeBuff -= slotGameObject.GetComponent<Armor>().bonusDodge;
                            Player.Instance.dodgeMalus -= slotGameObject.GetComponent<Armor>().malusDodge;
                            Player.Instance.encombrement -= slotGameObject.GetComponent<Armor>().encombrement;

                            // Back in inventory
                            if (armorSlot.transform.childCount == 3) ResetEquipedSlot(armorSlot);
                        }
                    }
                }

                // Reset Player Target
                Player.Instance.ResetTarget();
            }

            if (slotGameObject.GetComponent<Weapon>() != null)
            {
                GameObject leftHand = GameObject.Find("LEFT_HAND_COMBAT");

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

                GameObject rightHand = GameObject.Find("RIGHT_HAND_COMBAT");

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
        }
        else
        {
            lastClick = Time.time;
        }
    }

    private void ResetEquipedSlot(GameObject slotGameObject)
    {
        var item = slotGameObject.transform.GetChild(2).gameObject.GetComponent<Item>();
        GameManager.Instance.GetComponent<Inventory>().AddItem(slotGameObject.transform.GetChild(2).gameObject, item.id, item.type, item.description, item.icon);
    }
}
