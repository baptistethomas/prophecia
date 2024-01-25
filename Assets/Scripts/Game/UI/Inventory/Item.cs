using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public GameObject itemGo;
    public string type;
    public int encumbrance;
    public int buyPrice;
    public int sellPrice;
    public string name;
    [TextArea]
    public string descriptionShort;
    [TextArea]
    public string descriptionFull;
    public Sprite icon;
    public bool pickedUp;
    public bool equipped;

    public void ItemUsage()
    {
        // Light Healing Potion : +25 life
        if (id == 11)
        {
            if (Player.Instance.currentHealth + 25 < Player.Instance.health) Player.Instance.currentHealth += 25;
            else Player.Instance.currentHealth = Player.Instance.health;
        }
    }
}
