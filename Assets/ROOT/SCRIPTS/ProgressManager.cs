using UnityEngine;
using TMPro;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private int maxAttempt;
    public static int curAttempt = 8;
    public static int correctObj = 0;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text tmp;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private LoadingScreen loadingScreen;

    public void StartLevel()
    {
        levelGenerator.GenerateLvl();
        correctObj = 0;
        curAttempt = maxAttempt;
        CheckProgress();
    }
    public void RestartLevel()
    {
        levelGenerator.Restart();
        correctObj = 0;
        curAttempt = maxAttempt;
        CheckProgress();
    }
    void GameWon()
    {
        tmp.text = "Уровень пройден!";
        ShowContinueScreen();
    }
    void GameOver()
    {
        tmp.text = "Вы проиграли!)";
        ShowContinueScreen();
    }
    void ShowContinueScreen()
    {
        loadingScreen.gameObject.SetActive(true);
        loadingScreen.FadeIn();
    }
    public void CheckProgress()
    {
        score.text = "" + curAttempt;
        if (correctObj == levelGenerator.subjectsCount / 3)
        {
            GameWon();
        }
        if (curAttempt == 0)
        {
            GameOver();
        }
    }
}
