using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Game.UI.Inventory
{
    public class SlotDrop : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.GetComponent<Slot>() != null && eventData.pointerDrag.GetComponent<Slot>().item != null)
            {
                eventData.pointerDrag.GetComponent<SlotDragable>().parentAfterDrag = transform;
            }
        }
    }
}