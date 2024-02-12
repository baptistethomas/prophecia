using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace Assets.Scripts.Game.UI.Inventory
{
    public class SlotDragable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public Transform parentBeforeDrag;
        public Transform parentAfterDrag;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (transform.GetComponent<Slot>().item != null)
            {
                parentBeforeDrag = transform.parent;
                parentAfterDrag = transform.parent;
                if (GameManager.Instance.storageEnabled == false) transform.SetParent(GameObject.Find("Equipement").transform);
                if (GameManager.Instance.storageEnabled == true) transform.SetParent(GameObject.Find("Storage").transform);
                transform.SetAsLastSibling();
                transform.GetComponent<CanvasGroup>().alpha = .5f;
                transform.Find("Panel").GetComponent<Image>().raycastTarget = false;
            }
            else return;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(parentAfterDrag);
            // Endrag for Macro bar
            if (eventData.pointerDrag.transform.parent.transform.parent.transform.parent.gameObject.name == "Macro Bar")
            {

                // Get the Item dragged
                Item draggedItem = transform.Find("Panel").parent.GetChild(2).gameObject.GetComponent<Item>();

                // If it's a consumable, it is slotable on macro bar
                if (draggedItem.type == "Consumable")
                {
                    // Use the icon on the macro bar
                    GameObject macroPanel = eventData.pointerDrag.transform.parent.GetChild(1).gameObject;
                    macroPanel.GetComponent<Image>().sprite = transform.Find("Panel").GetComponent<Image>().sprite;
                    macroPanel.GetComponent<Image>().enabled = true;
                    macroPanel.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);

                    macroPanel.GetComponent<Item>().id = draggedItem.id;
                    macroPanel.GetComponent<Item>().type = draggedItem.type;
                    macroPanel.GetComponent<Item>().name = draggedItem.name;
                    macroPanel.GetComponent<Item>().descriptionShort = draggedItem.descriptionShort;
                    macroPanel.GetComponent<Item>().descriptionFull = draggedItem.descriptionFull;
                    macroPanel.GetComponent<Item>().icon = draggedItem.icon;
                }
                transform.SetParent(parentBeforeDrag);
            }
            // Endrag for Inventory
            else
            {
                // We swap the slots
                if (parentBeforeDrag != parentAfterDrag)
                {
                    GameObject slotContentToSwap = parentAfterDrag.GetChild(0).gameObject;
                    slotContentToSwap.transform.SetParent(parentBeforeDrag);
                    slotContentToSwap.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

                    // We add or sub encumbrance depending it's storage to inventory or inventory to storage
                    string parentBeforeDragRootObjectName = parentBeforeDrag.transform.parent.transform.parent.transform.parent.gameObject.name;
                    string parentAfterDragRootObjectName = parentAfterDrag.transform.parent.transform.parent.transform.parent.gameObject.name;

                    int countItem = transform.childCount - 2;
                    GameObject itemGameObject = transform.GetChild(transform.childCount - 1).gameObject;

                    // This is Inventory to Storage, so we sub encumbrance of items moved
                    if (parentBeforeDragRootObjectName == "Inventory" && parentAfterDragRootObjectName == "Storage")
                    {
                        Player.Instance.currentEncumbrance -= (itemGameObject.GetComponent<Item>().encumbrance * countItem);
                    }
                    // This is Storage to Inventory, so we add encumbrance of items moved
                    if (parentBeforeDragRootObjectName == "Storage" && parentAfterDragRootObjectName == "Inventory")
                    {
                        if (Player.Instance.leftEncumbrance >= (itemGameObject.GetComponent<Item>().encumbrance * countItem))
                        {
                            Player.Instance.currentEncumbrance += (itemGameObject.GetComponent<Item>().encumbrance * countItem);
                        }
                        else
                        {
                            Debug.Log("Too Heavy Exception");
                        }
                    }

                }
            }
            transform.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            transform.GetComponent<CanvasGroup>().alpha = 1;
            transform.Find("Panel").GetComponent<Image>().raycastTarget = true;
        }
    }
}