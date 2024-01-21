using UnityEngine;
using UnityEngine.EventSystems;

public class Shortcuts : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData eventData)
    {
        string windowName = transform.GetChild(0).gameObject.name;

        if (windowName == "Options Shortcut") GameManager.Instance.optionsEnabled = !GameManager.Instance.optionsEnabled;
        if (windowName == "Inventory Shortcut") GameManager.Instance.inventoryEnabled = !GameManager.Instance.inventoryEnabled;
        if (windowName == "Character Sheet Shortcut") GameManager.Instance.characterSheetEnabled = !GameManager.Instance.characterSheetEnabled;
        if (windowName == "Guild Shortcut") GameManager.Instance.guildEnabled = !GameManager.Instance.guildEnabled;
        if (windowName == "Spells Shortcut") GameManager.Instance.spellsEnabled = !GameManager.Instance.spellsEnabled;
        if (windowName == "Macro Shortcut") GameManager.Instance.macroEnabled = !GameManager.Instance.macroEnabled;
        if (windowName == "Group Shortcut") GameManager.Instance.groupEnabled = !GameManager.Instance.groupEnabled;
        GameManager.Instance.CloseUIWindow();
    }
}
