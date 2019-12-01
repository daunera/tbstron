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

    private GameObject CharacterSelectorWindow;


    void Start()
    {
        LogInWindow = GameObject.Find(nameof(LogInWindow)).gameObject;
        UserNameInputText = LogInWindow.transform.Find("UserNameBox").Find("Text").GetComponentInChildren<Text>();

        MainMenuWindow = GameObject.Find(nameof(MainMenuWindow)).gameObject;
        UserNameDisplayText = MainMenuWindow.transform.Find(nameof(UserNameDisplayText)).GetComponent<Text>();

        AchievementsWindow = GameObject.Find(nameof(AchievementsWindow)).gameObject;
        AchievementsLayout = GameObject.Find(nameof(AchievementsLayout)).GetComponent<GridLayoutGroup>();

        CharacterSelectorWindow = GameObject.Find(nameof(CharacterSelectorWindow)).gameObject;

        MainMenuWindow.SetActive(false);
        AchievementsWindow.SetActive(false);
        CharacterSelectorWindow.SetActive(false);

        LogIn();
    }

    public void OnClick_LogIn()
    {
        if (!string.IsNullOrWhiteSpace(UserNameInputText.text))
        {
            DBConnector.SetPlayer(UserNameInputText.text.Trim());
            LogIn();
        }
    }

    private void LogIn()
    {
        if (PlayerManager.Instance.Player != null)
        {
            UserNameDisplayText.text = PlayerManager.Instance.Player.Name;
            MainMenuWindow.SetActive(true);
            LogInWindow.SetActive(false);
            DBConnector.RefreshUnlocks();
        }
    }

    public void OnClick_LogOut()
    {
        DBConnector.Save();

        LogInWindow.SetActive(true);
        MainMenuWindow.SetActive(false);
    }

    public void OnClick_Exit()
    {

        DBConnector.Save();

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
        for (int i = 0; i < PlayerManager.Instance.Player.Achievements.Count; i++)
        {
            achievement = Instantiate(AchievementPrefab, AchievementsLayout.transform);
            achievement.GetComponent<AchievementBehaviour>().Fill(PlayerManager.Instance.Player.Achievements[i]);
        }
    }

    public void OnClick_BackFromAchievements()
    {
        AchievementsWindow.SetActive(false);
        MainMenuWindow.SetActive(true);

        foreach (Transform child in AchievementsLayout.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnClick_CharacterSelector()
    {
        CharacterSelectorWindow.SetActive(true);
        MainMenuWindow.SetActive(false);
        CharacterSelectorWindow.GetComponent<CharacterSelectorWindow>().Inicialize();
    }

    public void OnClick_BackFromCharacterSelector()
    {
        CharacterSelectorWindow.SetActive(false);
        MainMenuWindow.SetActive(true);
    }
}
