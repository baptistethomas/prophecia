using UnityEngine;

public class Macro : MonoBehaviour
{
    public GameObject macro;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.Instance.macroEnabled = !GameManager.Instance.macroEnabled;
            GameManager.Instance.CloseUIWindow();
            Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
        }

        if (GameManager.Instance.macroEnabled == true) macro.SetActive(true);
        else macro.SetActive(false);
    }
}
