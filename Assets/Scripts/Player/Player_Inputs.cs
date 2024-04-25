using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Inputs : Singleton<Player_Inputs>
{
    
    public PlayerInput_Character inputs;
    [HideInInspector] public bool LeftMB;
    [HideInInspector] private bool Btn_E;
    [HideInInspector] private bool Btn_I;
    [HideInInspector] private bool Btn_Space;
    [HideInInspector] private bool Btn_WS1, Btn_WS2, Btn_WS3, Btn_WS4;
    [HideInInspector] public float mouseX, mouseY;
    [HideInInspector] public Vector2 movementInput;
    public float mouseSensitivity = 1f;


    private void SetUpControlls()
    {
        inputs.InGame.Interact.started += c => Btn_E = true;
        inputs.InGame.Jump.started += c => Btn_Space = true;
        inputs.InGame.Inventar.started += c => Btn_I = true;
        inputs.InGame.WeaponSlot1.started += c => Btn_WS1 = true;
        inputs.InGame.WeaponSlot2.started += c => Btn_WS2 = true;
        inputs.InGame.WeaponSlot3.started += c => Btn_WS3 = true;
        inputs.InGame.WeaponSlot4.started += c => Btn_WS4 = true;

        inputs.InGame.Shoot.started += c => LeftMB = true;
        inputs.InGame.Shoot.canceled += c => LeftMB = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = inputs.InGame.MouseX.ReadValue<float>() * mouseSensitivity;
        mouseY = inputs.InGame.MouseY.ReadValue<float>() * mouseSensitivity;
        movementInput = inputs.InGame.Movement.ReadValue<Vector2>();
    }

    public void ResetAllInputs()
    {
        LeftMB = false;
        Btn_E = false;
        Btn_I = false;
        Btn_Space = false;
        Btn_WS1 = false;
        Btn_WS2 = false;
        Btn_WS3 = false;
        Btn_WS4 = false;
        movementInput = Vector2.zero;
        mouseX = 0f;
        mouseY = 0f; 
    }

    private void OnEnable()
    {
        if (inputs == null)
            inputs = new PlayerInput_Character();
        inputs.Enable();
        SetUpControlls();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    public bool Get_Btn_E()
    {
        if (Btn_E)
        {
            Btn_E = false;
            return true;
        }
        return false;
    }

    public bool Get_Btn_Space()
    {
        if (Btn_Space)
        {
            Btn_Space = false;
            return true;
        }
        return false;
    }

    public bool Get_Btn_I()
    {
        if (Btn_I)
        {
            Btn_I = false;
            return true;
        }
        return false;
    }

    public bool Get_Btn_WS1()
    {
        if (Btn_WS1)
        {
            Btn_WS1 = false;
            return true;
        }
        return false;
    }

    public bool Get_Btn_WS2()
    {
        if (Btn_WS2)
        {
            Btn_WS2 = false;
            return true;
        }
        return false;
    }

    public bool Get_Btn_WS3()
    {
        if (Btn_WS3)
        {
            Btn_WS3 = false;
            return true;
        }
        return false;
    }

    public bool Get_Btn_WS4()
    {
        if (Btn_WS4)
        {
            Btn_WS4 = false;
            return true;
        }
        return false;
    }
}
