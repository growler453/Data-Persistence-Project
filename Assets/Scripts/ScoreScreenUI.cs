using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScreenUI : MonoBehaviour
{
    public Text scores;
    private List<int> score;
    private List<string> scoreName;

    private void Awake()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            ScoreManager.Instance.LoadScores();
            score = ScoreManager.Instance.HighScore;
            scoreName = ScoreManager.Instance.Scorer;
        }

        for (int i = 0; i < 10; i++)
        {
            if (i < score.Count)
                scores.text += score[i] + " - " + scoreName[i] + "\n";
            else
                scores.text += "0 - Name\n";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }

}
