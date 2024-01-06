using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public string type;
    public string description;
    public Sprite icon;
    public bool pickedUp;
    private bool itemHasBeenClicked;
    public bool equipped;

    private void OnMouseOver()
    {
        if (equipped == false) Cursor.SetCursor(CustomCursor.Instance.cursorPickItem, Vector3.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector3.zero, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        if (equipped == false) itemHasBeenClicked = true;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.name == "Player" && itemHasBeenClicked == true && equipped == false)
        {
            GameObject itemPickedUp = gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            Player.Instance.GetComponent<Inventory>().AddItem(itemPickedUp, item.id, item.type, item.description, item.icon);
            Player.Instance.resetTarget();
        }
    }
    public void ItemUsage()
    {
        // Weapon
        if (type == "Weapon")
        {
            equipped = true;
        }

        // Armor

        // Pots

    }
}
