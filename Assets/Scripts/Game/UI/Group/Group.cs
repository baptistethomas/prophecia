using UnityEngine;

public class Group : MonoBehaviour
{
    public GameObject group;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameManager.Instance.groupEnabled = !GameManager.Instance.groupEnabled;
            GameManager.Instance.CloseUIWindow();
            Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
        }

        if (GameManager.Instance.groupEnabled == true) group.SetActive(true);
        else group.SetActive(false);
    }
}
