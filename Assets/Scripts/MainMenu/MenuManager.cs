using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private GameObject LogInWindow;
    private Text UserNameInputText;

    private GameObject MainMenuWindow;
    private Text UserNameDisplayText;

    private GameObject AchievementsWindow;
    private GridLayoutGroup AchievementsLayout;
    public GameObject AchievementPrefab;


    void Start()
    {
        LogInWindow = GameObject.Find(nameof(LogInWindow));
        UserNameInputText = LogInWindow.transform.Find("UserNameBox").Find("Text").GetComponentInChildren<Text>();

        MainMenuWindow = GameObject.Find(nameof(MainMenuWindow));
        UserNameDisplayText = MainMenuWindow.transform.Find(nameof(UserNameDisplayText)).GetComponent<Text>();

        AchievementsWindow = GameObject.Find(nameof(AchievementsWindow));
        AchievementsLayout = GameObject.Find(nameof(AchievementsLayout)).GetComponent<GridLayoutGroup>();
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

    public void OnClick_Achievements()
    {
        AchievementsWindow.SetActive(true);
        MainMenuWindow.SetActive(false);

        GameObject achievement;
        for (int i = 0; i < DBConnector.Player.Achievements.Count; i++)
        {
            achievement = Instantiate(AchievementPrefab, AchievementsLayout.transform);
            achievement.GetComponent<AchievementBehaviour>().Fill(DBConnector.Player.Achievements[i]);
        }
    }

    public void OnClick_BackToMainMenu()
    {
        AchievementsWindow.SetActive(false);
        MainMenuWindow.SetActive(true);

        foreach (Transform child in AchievementsLayout.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
