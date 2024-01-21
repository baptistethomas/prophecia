using UnityEngine;

public class Spells : MonoBehaviour
{
    public GameObject spells;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            GameManager.Instance.spellsEnabled = !GameManager.Instance.spellsEnabled;
            GameManager.Instance.CloseUIWindow();
            Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
        }

        if (GameManager.Instance.spellsEnabled == true)
        {
            Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
            spells.SetActive(true);
        }
        else
        {
            spells.SetActive(false);
        }
    }
}
