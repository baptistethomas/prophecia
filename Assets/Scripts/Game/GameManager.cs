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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
