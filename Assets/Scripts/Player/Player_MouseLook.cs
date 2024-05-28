using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MouseLook : MonoBehaviour
{
     private Player_Inputs player_Inputs;
    public Transform playerBody;
    float xRotation = 0f;
    float mouseX, mouseY;
    float finalmouseSensitivity;

    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player_Inputs = Player_Inputs.Inst;
    }

    // Update is called once per frame
    void Update()
    {
        //finalmouseSensitivity = mouseSensitivity * Time.deltaTime;
        //finalmouseSensitivity = mouseSensitivity * Time.unscaledDeltaTime;
        Check_Input();
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f,0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void Check_Input()
    {
        mouseX = player_Inputs.mouseX * Time.deltaTime;
        mouseY = player_Inputs.mouseY * Time.deltaTime;
    }
}
