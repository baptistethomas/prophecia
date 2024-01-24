using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    // Cursor Instance
    private static CustomCursor _instance;
    public static CustomCursor instance;

    public static CustomCursor Instance
    {
        get { return _instance; }
    }

    // Cursors
    public Texture2D cursorDefault;
    public Texture2D cursorTop;
    public Texture2D cursorTopRight;
    public Texture2D cursorRight;
    public Texture2D cursorBottomRight;
    public Texture2D cursorBottom;
    public Texture2D cursorBottomLeft;
    public Texture2D cursorLeft;
    public Texture2D cursorTopLeft;
    public Texture2D cursorMeleeAttack;
    public Texture2D cursorRangeAttack;
    public Texture2D cursorPickItem;
    public Texture2D cursorChat;
    public CursorMode cursorMode = CursorMode.Auto;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }
}
