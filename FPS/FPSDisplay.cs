using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FPSDisplay : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Tools/FPS")]
    static void CreateFPSDisplay()
    {
        if (FindObjectOfType<FPSDisplay>() == null)
        {
            var go = new GameObject("FPS");
            go.AddComponent<FPSDisplay>();
        }
    }
#endif
    private int _fps;
    private int _count;
    private float _deltaTime;
	private GUIStyle _style;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		_style = new GUIStyle();
		_style.fontSize = 30;
		_style.normal.textColor = Color.white;
        _style.alignment = TextAnchor.MiddleRight;
	}

    void LateUpdate()
    {
        _count++;
        _deltaTime += Time.unscaledDeltaTime;
        if (_count > 10)
        {
            _fps = (int)(1f / (_deltaTime / _count));
            _count = 0;
            _deltaTime = 0f;
        }
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 60, 0, 60, 30), _fps.ToString(), _style);
    }
}
