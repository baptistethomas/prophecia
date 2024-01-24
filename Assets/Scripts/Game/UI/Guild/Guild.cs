using UnityEngine;

public class Guild : MonoBehaviour
{
    public GameObject guild;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && Player.Instance.isFocusedChat == false)
        {
            GameManager.Instance.guildEnabled = !GameManager.Instance.guildEnabled;
            GameManager.Instance.CloseUIWindow();
            Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
        }

        if (GameManager.Instance.guildEnabled == true) guild.SetActive(true);
        else guild.SetActive(false);
    }
}
