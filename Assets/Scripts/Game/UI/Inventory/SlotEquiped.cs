using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotEquiped : MonoBehaviour, IPointerClickHandler, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
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
    private Transform initialEquipParent;
    private Vector3 initialEquipPosition;
    private Transform draggedEquipment;

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialEquipParent = transform.Find("Equip").parent;
        initialEquipPosition = transform.Find("Equip").position;
        draggedEquipment = transform.Find("Equip");
        //draggedEquipment.SetParent(GameObject.Find("Equipement").transform);
        //transform.SetAsLastSibling();
        transform.GetComponent<CanvasGroup>().alpha = .5f;
        draggedEquipment.GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggedEquipment.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //draggedEquipment.SetParent(initialEquipParent);
        UnEquipSLot();
        transform.GetComponent<CanvasGroup>().alpha = 1;
        draggedEquipment.GetComponent<Image>().raycastTarget = true;
        draggedEquipment.position = initialEquipPosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.gameObject.name == "Slot Content")
        {
            GameObject droppedSlot = eventData.pointerDrag.gameObject;
            Slot slot = droppedSlot.GetComponent<Slot>();
            droppedSlot.GetComponent<Slot>().EquipSlot(slot);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Equip Item on double click inventory slot
        if ((lastClick + interval) > Time.time)
        {
            UnEquipSLot();
        }
        else
        {
            lastClick = Time.time;
        }
    }

    private void UnEquipSLot()
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
                        if (slotGameObject.GetComponent<Armor>().bodyPart == "Chest") Player.Instance.body.transform.Find("Arms").GetComponent<Renderer>().enabled = true; // Chest need arms being active too
                        if (slotGameObject.GetComponent<Armor>().bodyPart == "Legs")
                        {
                            // Legs need underwear active disabled too
                            Player.Instance.accessories.transform.Find("Underwear").GetComponent<Renderer>().enabled = true;
                            Player.Instance.body.transform.Find("Legs").GetComponent<Renderer>().enabled = true;
                        }

                        // Disable Slot Equipment
                        GameObject armorSlot = GameObject.Find("Slot_" + slotGameObject.GetComponent<Armor>().bodyPart);
                        if (armorSlot.transform.GetChild(1)) armorSlot.transform.GetChild(1).gameObject.SetActive(false);

                        // Adding Equipment Bonus & Malus
                        Player.Instance.armorClassBuff -= slotGameObject.GetComponent<Armor>().armorClass;
                        Player.Instance.dodgeBuff -= slotGameObject.GetComponent<Armor>().bonusDodge;
                        Player.Instance.dodgeMalus -= slotGameObject.GetComponent<Armor>().malusDodge;

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

    private void ResetEquipedSlot(GameObject slotGameObject)
    {
        var item = slotGameObject.transform.GetChild(2).gameObject.GetComponent<Item>();
        GameManager.Instance.GetComponent<Inventory>().AddItem(slotGameObject.transform.GetChild(2).gameObject, item.id, item.type, item.name, item.descriptionShort, item.descriptionFull, item.icon);
    }
}
