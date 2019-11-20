using UnityEngine;
using UnityEngine.UI;

public class AchievementBehaviour : MonoBehaviour
{
    public Text Name;
    public Text Text;
    public Text ProgressText;

    public void Fill(Achievement achievement)
    {
        Name.text = achievement.Name;
        Text.text = achievement.Text;
        ProgressText.text = achievement.ProgressText;
    }
}