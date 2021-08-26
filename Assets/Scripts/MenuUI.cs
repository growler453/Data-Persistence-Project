using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    public Text bestScore;
    public InputField input;
    private int score;
    private string scoreName;
    private bool nameInput = false;

    private void Awake()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            LoadHighestScore();
        }
    }

    public void StartNew()
    {
        if (nameInput)
        {
            ScoreManager.Instance.Scorer.Add(input.text);
            SceneManager.LoadScene(1);
        }
    }

    public void NameInput()
    {
        if (input.text != "")
        {
            nameInput = true;
        } else
        nameInput = false;
    }

    public void Exit()
    {
        ScoreManager.Instance.SaveScores();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void LoadHighestScore()
    {
        ScoreManager.Instance.LoadScores();
        score = ScoreManager.Instance.HighScore[0];
        scoreName = ScoreManager.Instance.Scorer[0];
        bestScore.text = "Best Score: " + score +"\n" + scoreName;
    }
}
