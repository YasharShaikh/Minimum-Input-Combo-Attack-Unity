using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMenuInputHandler : MonoBehaviour
{
    public static ActionMenuInputHandler Instance;

    [Header("InputAction Asset")]
    [SerializeField] InputActionAsset playerControls;

    [Header("ActionMap Name")]
    [SerializeField] string actionMapName;

    [Header("Menu Action")]
    [SerializeField] string RadialMenu;


    InputAction RadialMenuAction;

    public bool RadialMenuTriggered { get; private set; }


    private void Awake()
    {
        Instance = this;
        RadialMenuAction = playerControls.FindActionMap(actionMapName).FindAction(RadialMenu);
    }
    // Update is called once per frame
    void Update()
    {

        if (RadialMenuAction.IsPressed())
        {
            RadialMenuTriggered = true;

        }
        else
            RadialMenuTriggered = false;
    }


    private void OnEnable()
    {
        RadialMenuAction.Enable();

    }

    private void OnDisable()
    {
        RadialMenuAction.Disable();

    }

}
