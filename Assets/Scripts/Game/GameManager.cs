using UnityEngine;

public class GameManager : MonoBehaviour
{

    // GameManager Instance
    private static GameManager _instance;
    public static GameManager instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    // Windows UI Management

    // Inventory, Character Sheets & Others UI Windows
    public bool inventoryEnabled;
    public bool characterSheetEnabled;
    public bool groupEnabled;
    public bool macroEnabled;
    public bool spellsEnabled;
    public bool optionsEnabled;
    public bool guildEnabled;
    public bool merchantEnabled;

    // GameManager Settings
    public int experienceRate;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }
    public void CloseUIWindow()
    {
        if (GameObject.Find("Inventory") != null)
        {
            GameObject.Find("Inventory").SetActive(false);
            inventoryEnabled = false;
        }
        if (GameObject.Find("Character Sheet") != null)
        {
            GameObject.Find("Character Sheet").SetActive(false);
            characterSheetEnabled = false;
        }
        if (GameObject.Find("Spells") != null)
        {
            GameObject.Find("Spells").SetActive(false);
            spellsEnabled = false;
        }
        if (GameObject.Find("Guild") != null)
        {
            GameObject.Find("Guild").SetActive(false);
            guildEnabled = false;
        }
        if (GameObject.Find("Group") != null)
        {
            GameObject.Find("Group").SetActive(false);
            groupEnabled = false;
        }
        if (GameObject.Find("Macro") != null)
        {
            GameObject.Find("Macro").SetActive(false);
            macroEnabled = false;
        }
        if (GameObject.Find("Options") != null)
        {
            GameObject.Find("Options").SetActive(false);
            optionsEnabled = false;
        }
        if (GameObject.Find("Merchant") != null)
        {
            GameObject.Find("Merchant").SetActive(false);
            merchantEnabled = false;
            Player.Instance.lastChat = null;
        }
    }
}
