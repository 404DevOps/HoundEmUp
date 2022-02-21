using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public string PlayerName;
    public int Score;

    public static int firstPlace;
    public static string firstPlaceName;

    public static int secondPlace;
    public static string secondPlaceName;

    public static int thirdPlace;
    public static string thirdPlaceName;

    public TextMeshProUGUI highscoreText;



    public static ScoreManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();

        Debug.Log("ScoreManager Instance loaded.");

    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Serializable]
    class SaveData {
        public int FirstPlace;
        public string FirstPlaceName;

        public int SecondPlace;
        public string SecondPlaceName;

        public int ThirdPlace;
        public string ThirdPlaceName;
    }

    public void SaveScore()
    {
        int caseSwitch = Score > thirdPlace ? 3 : 0;
        caseSwitch = Score > secondPlace ? 2 : caseSwitch;
        caseSwitch = Score > firstPlace ? 1 : caseSwitch;
        caseSwitch = Score < thirdPlace ? 0 : caseSwitch;

        Debug.Log("CaseSwitch / Rank: " + caseSwitch);

        bool updateScore = true;

        switch (caseSwitch)
        {
            case 1:
                thirdPlace = secondPlace;
                thirdPlaceName = secondPlaceName;
                secondPlace = firstPlace;
                secondPlaceName = firstPlaceName;
                firstPlace = Score;
                firstPlaceName = PlayerName;
                break;
            case 2:
                
                thirdPlace = secondPlace;
                thirdPlaceName = secondPlaceName;
                secondPlace = Score;
                secondPlaceName = PlayerName;
                break;
            case 3:
                thirdPlace = Score;
                thirdPlaceName = PlayerName;
                break;
            case 0:
                updateScore = false;
                break;
        }

        if (updateScore)
        {
            SaveData data = new SaveData();
            data.FirstPlace = firstPlace;
            data.SecondPlace = secondPlace;
            data.ThirdPlace = thirdPlace;
            data.FirstPlaceName = firstPlaceName;
            data.SecondPlaceName = secondPlaceName;
            data.ThirdPlaceName = thirdPlaceName;
            string path = Application.persistentDataPath + "/savefile.json";
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);

            Debug.Log("Score saved to File: 1. " + data.FirstPlace + " " + data.FirstPlaceName + Environment.NewLine
                            + "2. " + data.SecondPlace + " " + data.SecondPlaceName + Environment.NewLine
                            + "3. " + data.ThirdPlace + " " + data.ThirdPlaceName);


        }
        LoadScore();
    }

    public void LoadScore()
    {
        
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            firstPlace = data.FirstPlace;
            secondPlace = data.SecondPlace;
            thirdPlace = data.ThirdPlace;
            firstPlaceName = data.FirstPlaceName;
            secondPlaceName = data.SecondPlaceName;
            thirdPlaceName = data.ThirdPlaceName;
            UpdateScoreText();
            Debug.Log("Score Loaded from File: 1. " + firstPlaceName + " " + firstPlace + Environment.NewLine
                            + "2. " + secondPlaceName + " " + secondPlace + Environment.NewLine
                            + "3. " + thirdPlaceName + " " + thirdPlace);
        }
    }

    public void UpdateScoreText()
    {
        highscoreText.SetText("1. " + firstPlaceName + " " + firstPlace + Environment.NewLine
                            + "2. " + secondPlaceName + " " + secondPlace + Environment.NewLine
                            + "3. " + thirdPlaceName + " " + thirdPlace);


    }
}
