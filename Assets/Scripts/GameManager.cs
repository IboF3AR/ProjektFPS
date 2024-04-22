using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Inst;
    public bool Settings_ShakeOn = true;

    void Awake()
    {
        if(Inst != null) return;
        Inst = this;
    }
    #endregion

    public bool gameIsPaused;
    public GameObject player;

    // Message
    public TMP_Text txt_message;
    private bool messageIsShowing = false;
    private float message_timer;
    private float message_duration;

    private void Start()
    {
        player.transform.parent = null;
    }

    public void PauseGame()
    {  
        Time.timeScale = 0; 
        gameIsPaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    private void Update()
    {
        if(messageIsShowing)
        {
            message_timer += Time.deltaTime;
            if(message_timer > message_duration)
            {
                messageIsShowing = false;
                message_timer = 0f;
                txt_message.text = "";
                txt_message.gameObject.SetActive(false);
            }
        }
    }

    public void SetMessage(float message_duration, string message)
    {
        messageIsShowing  = true;
        message_timer = 0f;
        this.message_duration = message_duration;
        message_timer = 0f;
        txt_message.text = message;
        txt_message.gameObject.SetActive(true);
    }
}
