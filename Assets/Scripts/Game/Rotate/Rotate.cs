using UnityEngine;

public class Rotate : MonoBehaviour
{

    // Rotate Instance
    private static Rotate _instance;
    public static Rotate instance;
    public static Rotate Instance
    {
        get { return _instance; }
    }

    // Rotate & Character
    private Quaternion rotation;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }

    public Vector3 IsoVectorConvert(Vector3 vector, Quaternion rotation)
    {
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }

    public Vector3 RotateCharacterAccordingMouseClick()
    {
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 characterPosition = Player.Instance.transform.position;
        var toConvert = mousePositionWorld - characterPosition;
        var positionMouseLeftButtonPressed = IsoVectorConvert(toConvert, Quaternion.Euler(0, 180, 0));

        // Left
        if (positionMouseLeftButtonPressed.x > 0 && positionMouseLeftButtonPressed.y > 0 && positionMouseLeftButtonPressed.z < 0)
        {
            // Top Left
            if (positionMouseLeftButtonPressed.y >= 2.25 && positionMouseLeftButtonPressed.y <= 12)
            {
                //print("Top Left");
                toConvert = new Vector3(-0.71f, 0, 0.71f);
                Player.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
                rotation = Quaternion.Euler(0, 45, 0);
                Cursor.SetCursor(CustomCursor.Instance.cursorTopLeft, Vector2.zero, CustomCursor.Instance.cursorMode);
            }
            else
            {
                //print("Top");
                toConvert = new Vector3(-1, 0, 0);
                Player.Instance.transform.rotation = Quaternion.Euler(0, 315, 0);
                rotation = Quaternion.Euler(0, 90, 0);
                Cursor.SetCursor(CustomCursor.Instance.cursorLeft, Vector2.zero, CustomCursor.Instance.cursorMode);
            }
        }

        // Top
        if (positionMouseLeftButtonPressed.x < 0 && positionMouseLeftButtonPressed.y > 0 && positionMouseLeftButtonPressed.z < 0)
        {
            //print("Top");
            toConvert = new Vector3(0, 0, 1);
            Player.Instance.transform.rotation = Quaternion.Euler(0, 45, 0);
            rotation = Quaternion.Euler(0, 0, 0);
            Cursor.SetCursor(CustomCursor.Instance.cursorTop, Vector2.zero, CustomCursor.Instance.cursorMode);
        }

        // Right
        if (positionMouseLeftButtonPressed.x < 0 && positionMouseLeftButtonPressed.y > 0 && positionMouseLeftButtonPressed.z > 0)
        {
            // Top Right           
            if (positionMouseLeftButtonPressed.y >= 2.5 && positionMouseLeftButtonPressed.y <= 12)
            {
                //print("Top Right");
                toConvert = new Vector3(0.71f, 0, 0.71f);
                Player.Instance.transform.rotation = Quaternion.Euler(0, 90, 0);
                rotation = Quaternion.Euler(0, 315, 0);
                Cursor.SetCursor(CustomCursor.Instance.cursorTopRight, Vector2.zero, CustomCursor.Instance.cursorMode);
            }
            else
            {
                //print("Right");
                toConvert = new Vector3(1, 0, 0);
                Player.Instance.transform.rotation = Quaternion.Euler(0, 135, 0);
                rotation = Quaternion.Euler(0, 270, 0);
                Cursor.SetCursor(CustomCursor.Instance.cursorRight, Vector2.zero, CustomCursor.Instance.cursorMode);
            }
        }

        // Bottom
        if (positionMouseLeftButtonPressed.x > 0 && positionMouseLeftButtonPressed.y < 0 && positionMouseLeftButtonPressed.z > 0)
        {
            //print("Bottom");
            toConvert = new Vector3(0, 0, -1);
            Player.Instance.transform.rotation = Quaternion.Euler(0, 225, 0);
            rotation = Quaternion.Euler(0, 180, 0);
            Cursor.SetCursor(CustomCursor.Instance.cursorBottom, Vector2.zero, CustomCursor.Instance.cursorMode);
        }

        // Bottom Left
        if (positionMouseLeftButtonPressed.x > 0 && positionMouseLeftButtonPressed.y < 0 && positionMouseLeftButtonPressed.z < 0)
        {
            //print("Bottom Left");
            toConvert = new Vector3(-0.71f, 0, -0.71f);
            Player.Instance.transform.rotation = Quaternion.Euler(0, 270, 0);
            rotation = Quaternion.Euler(0, 135, 0);
            Cursor.SetCursor(CustomCursor.Instance.cursorBottomLeft, Vector2.zero, CustomCursor.Instance.cursorMode);
        }

        // Bottom Right
        if (positionMouseLeftButtonPressed.x < 0 && positionMouseLeftButtonPressed.y < 0 && positionMouseLeftButtonPressed.z > 0)
        {
            //print("Bottom Right");
            toConvert = new Vector3(0.71f, 0, -0.71f);
            Player.Instance.transform.rotation = Quaternion.Euler(0, 180, 0);
            rotation = Quaternion.Euler(0, 225, 0);
            Cursor.SetCursor(CustomCursor.Instance.cursorBottomRight, Vector2.zero, CustomCursor.Instance.cursorMode);
        }

        // Get Direction
        return IsoVectorConvert(toConvert, rotation);
    }

    public Vector3 RotateCharacterAccordingKeyboardDirectionals(Vector3 toConvert)
    {
        rotation = Quaternion.Euler(0, 180, 0);

        // Left
        if (toConvert.ToString() == "(-1.00, 0.00, 0.00)")
        {
            Player.Instance.transform.rotation = Quaternion.Euler(0, 315, 0);
            rotation = Quaternion.Euler(0, 90, 0);
        }
        // Left Top
        if (toConvert.ToString() == "(-0.71, 0.00, 0.71)")
        {
            Player.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            rotation = Quaternion.Euler(0, 45, 0);
        }
        // Top
        if (toConvert.ToString() == "(0.00, 0.00, 1.00)")
        {
            Player.Instance.transform.rotation = Quaternion.Euler(0, 45, 0);
            rotation = Quaternion.Euler(0, 0, 0);
        }
        // Top Right
        if (toConvert.ToString() == "(0.71, 0.00, 0.71)")
        {
            Player.Instance.transform.rotation = Quaternion.Euler(0, 90, 0);
            rotation = Quaternion.Euler(0, 315, 0);

        }
        // Right
        if (toConvert.ToString() == "(1.00, 0.00, 0.00)")
        {
            Player.Instance.transform.rotation = Quaternion.Euler(0, 135, 0);
            rotation = Quaternion.Euler(0, 270, 0);
        }
        // Bottom Right
        if (toConvert.ToString() == "(0.71, 0.00, -0.71)")
        {
            Player.Instance.transform.rotation = Quaternion.Euler(0, 180, 0);
            rotation = Quaternion.Euler(0, 225, 0);
        }
        // Bottom
        if (toConvert.ToString() == "(0.00, 0.00, -1.00)")
        {
            Player.Instance.transform.rotation = Quaternion.Euler(0, 225, 0);
            rotation = Quaternion.Euler(0, 180, 0);
        }

        // Bottom Left
        if (toConvert.ToString() == "(-0.71, 0.00, -0.71)")
        {
            Player.Instance.transform.rotation = Quaternion.Euler(0, 270, 0);
            rotation = Quaternion.Euler(0, 135, 0);
        }

        // Get Direction
        return IsoVectorConvert(toConvert, rotation);

    }
}
