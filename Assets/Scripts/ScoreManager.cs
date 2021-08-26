using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public List<int> HighScore;
    public List<string> Scorer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScores();
    }

    [System.Serializable]
    class SaveData
    {
        public List<int> HighScore;
        public List<string> Scorer;
    }

    public void SaveScores()
    {
        SaveData data = new SaveData();
        int[] tempI = HighScore.ToArray();
        string[] tempS = Scorer.ToArray();
        Array.Sort(tempI, tempS);
        Array.Reverse(tempI);
        Array.Reverse(tempS);
        HighScore.Clear();
        Scorer.Clear();

        for (int i = 0; i < 10; i++)
        {
            if (i < tempI.Length)
            {
                HighScore.Add(tempI[i]);
                Scorer.Add(tempS[i]);
            }
        }

        data.HighScore = HighScore;
        data.Scorer = Scorer;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.HighScore;
            Scorer = data.Scorer;
        }
    }
}
