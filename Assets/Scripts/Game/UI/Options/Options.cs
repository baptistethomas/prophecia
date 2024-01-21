using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject options;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameManager.Instance.optionsEnabled = !GameManager.Instance.optionsEnabled;
            GameManager.Instance.CloseUIWindow();
            Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
        }

        if (GameManager.Instance.optionsEnabled == true)
        {
            Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
            options.SetActive(true);
        }
        else
        {
            options.SetActive(false);
        }
    }
}
