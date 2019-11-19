using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private GameObject LogInWindow;
    private Text UserNameInputText;

    private GameObject MainMenuWindow;
    private Text UserNameDisplayText;


    void Start()
    {
        LogInWindow = GameObject.Find(nameof(LogInWindow));
        UserNameInputText = LogInWindow.transform.Find("UserNameBox").Find("Text").GetComponentInChildren<Text>();

        MainMenuWindow = GameObject.Find(nameof(MainMenuWindow));
        UserNameDisplayText = MainMenuWindow.transform.Find(nameof(UserNameDisplayText)).GetComponent<Text>();
    }

    public void OnClick_LogIn()
    {
        if (!string.IsNullOrWhiteSpace(UserNameInputText.text))
        {
            DBConnector.SetPlayer(UserNameInputText.text.Trim());
            UserNameDisplayText.text = DBConnector.Player.Name;
            LogInWindow.SetActive(false);
        }
    }

    public void OnClick_LogOut()
    {
        LogInWindow.SetActive(true);
    }

    public void OnClick_Exit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
