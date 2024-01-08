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
            // Last slot child
            slotGameObject = transform.GetChild(transform.childCount - 1).gameObject;
            putAwayLeftHand();
            putAwayRightHand();
            equipLeftHandWeapon();
            equipRightHandWeapon();
            Player.Instance.resetTarget();
        }
        else
        {
            lastClick = Time.time;
        }
    }

    private void putAwayLeftHand()
    {
        leftHand = GameObject.Find("LEFT_HAND_COMBAT");
        var childCound = leftHand.transform.childCount;
        for (int i = 0; i < childCound; i++)
        {
            // Left hand is equiped
            var leftHandWeapon = leftHand.transform.GetChild(i);
            leftHandWeapon.gameObject.SetActive(false);

            // Disable the right hand slot on character inventory
            GameObject leftHandSlot = GameObject.Find("Left Hand");
            leftHandSlot.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void equipLeftHandWeapon()
    {
        leftHand = GameObject.Find("LEFT_HAND_COMBAT");
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

                    // Activate the right hand slot on character inventory
                    GameObject leftHandSlot = GameObject.Find("Left Hand");
                    leftHandSlot.transform.GetChild(1).gameObject.SetActive(true);
                    leftHandSlot.transform.GetChild(1).GetComponent<Image>().sprite = icon;

                    // Clean slot
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void putAwayRightHand()
    {
        rightHand = GameObject.Find("RIGHT_HAND_COMBAT");
        var childCound = rightHand.transform.childCount;
        for (int i = 0; i < childCound; i++)
        {
            // Put away weapon in right hand
            var rightHandWeapon = rightHand.transform.GetChild(i);
            rightHandWeapon.gameObject.SetActive(false);

            // Disable the right hand slot on character inventory
            GameObject rightHandSlot = GameObject.Find("Right Hand");
            rightHandSlot.transform.GetChild(1).gameObject.SetActive(false);
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

                    // Clean slot
                    gameObject.SetActive(false);

                }
            }
        }
    }
}
