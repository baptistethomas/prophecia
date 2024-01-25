using System.Collections.Generic;
using UnityEngine;

public class Sack : MonoBehaviour
{

    [SerializeField]
    public List<GameObject> items;
    private bool itemHasBeenClicked;

    private void OnMouseOver()
    {
        Cursor.SetCursor(CustomCursor.Instance.cursorPickItem, Vector3.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector3.zero, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        itemHasBeenClicked = true;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.name == "Player" && itemHasBeenClicked == true)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i].GetComponent<Item>();
                if (item.encumbrance < Player.Instance.leftEncumbrance)
                {
                    GameObject instantiateItem = Instantiate(items[i]);
                    GameObject.Find("Game Manager").GetComponent<Inventory>().AddItem(instantiateItem, item.id, item.type, item.name, item.descriptionShort, item.descriptionFull, item.icon);
                    Player.Instance.currentEncumbrance += item.encumbrance;
                    items.Remove(item.gameObject);
                }

            }
            Player.Instance.ResetTarget();
            if (items.Count == 0) Destroy(gameObject);
            return;
        }
    }
}
