using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Inputs : MonoBehaviour
{
    public bool LeftMB;
    public bool Btn_E;
    public bool Btn_I;
    public bool Btn_Space;
    public bool Btn_A1, Btn_A2;
    public float mouseX, mouseY;
    public float xInput, zInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Inst.gameIsPaused) return;
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        } 
        LeftMB = Input.GetMouseButtonDown(0);
        Btn_E = Input.GetKeyDown(KeyCode.E);
        Btn_I = Input.GetKeyDown(KeyCode.I);
        Btn_Space = Input.GetKeyDown(KeyCode.Space);
        Btn_A1 = Input.GetKeyDown(KeyCode.Alpha1);
        Btn_A2 = Input.GetKeyDown(KeyCode.Alpha2);

        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }

    public void ResetAllInputs()
    {
        LeftMB = false;
        Btn_E = false;
        Btn_I = false;
        Btn_Space = false;
        Btn_A1 = false;
        Btn_A2 = false;

        xInput = 0f;
        zInput = 0f;
        mouseX = 0f;
        mouseY = 0f;
    }
}
