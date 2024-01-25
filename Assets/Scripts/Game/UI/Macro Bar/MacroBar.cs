using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MacroBar : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) UseBindedConsumable();
        if (eventData.button == PointerEventData.InputButton.Right) UnbindConsumable();
    }

    private void UseBindedConsumable()
    {
        Item item = transform.Find("Panel").GetComponent<Item>();
        if (item != null) Inventory.Instance.ConsumeItem(item.id, item.type);
    }

    private void UnbindConsumable()
    {
        Item item = transform.Find("Panel").GetComponent<Item>();
        Image image = transform.Find("Panel").GetComponent<Image>();

        if (item != null)
        {
            item.id = 0;
            item.type = string.Empty;
            item.descriptionFull = string.Empty;
            item.icon = null;
            item.pickedUp = false;
            item.equipped = false;
        }

        if (image != null) image.enabled = false;
    }
}
