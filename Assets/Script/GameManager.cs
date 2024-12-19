using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public string[] playerListe;
    public string[] playerValue;
    public int currentPlayer = 0;
    [Space]
    public TextAsset jsonText;
    public TaskListe myTaskListe = new TaskListe();
    [Space]
    public int currentTask =-1;
    public int currentTurn = 0;
    [Space]
    public CardScriptableGameObject[] cardDeck;
    public string dataPath;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }


    void Start()
    {

        //UiManager.instance.NextPlayer(playerListe[currentPlayer]);
        
    }

    public void StartGame(string path, string difficulty, List<string>name, int players)
    {
        dataPath = path;
        playerListe = name.ToArray();
        playerValue = new string[players];
        ReadJson();
        myTaskListe.difficulty = difficulty;
        TaskUpdate();
        UiManager.instance.CardInit(cardDeck);
        UiManager.instance.NextTask(currentTask + 1, myTaskListe.taskListe[currentTask].taskName);
    }



    public void NextPlayer(CardScriptableGameObject card)
    {
        playerValue[currentPlayer] = card.value;
        if(currentPlayer+1 < playerListe.Length)
        {
            currentPlayer++;
            UiManager.instance.NextPlayer(playerListe[currentPlayer]);
        }
        else
        {
            if(currentTurn ==0 || myTaskListe.difficulty == "Unanimité")
            {
                currentTurn++;
                if(Unanimite(out string val))
                {
                    myTaskListe.taskListe[currentTask].taskDifficulty = val;
                    currentPlayer = 0;
                    currentTurn = 0;
                    UiManager.instance.NextPlayer(playerListe[currentPlayer]);
                    TaskUpdate();
                }
                else
                {
                    //Discution
                    currentPlayer = 0;
                    Extrem(playerListe, playerValue, out string max, out string min);
                    UiManager.instance.TalkTime(min,max);
                }
            }
            else
            {
                if(Unanimite(out string val))
                {
                    myTaskListe.taskListe[currentTask].taskDifficulty = val;
                }
                else if (myTaskListe.difficulty == "Moyenne")
                {
                    myTaskListe.taskListe[currentTask].taskDifficulty = Moyenne(playerValue);
                }
                else if (myTaskListe.difficulty == "Médiane")
                {
                    myTaskListe.taskListe[currentTask].taskDifficulty = Mediane(playerValue);
                }

                currentPlayer = 0;
                currentTurn = 0;
                UiManager.instance.NextPlayer(playerListe[currentPlayer]);
                TaskUpdate();
            }

        }
        
    }

    public void TaskUpdate()
    {
        if(currentTask<myTaskListe.taskListe.Length-1)
        {
            currentTask++;
            UiManager.instance.NextTask(currentTask + 1, myTaskListe.taskListe[currentTask].taskName);
        }
            
        else
        {
            WriteJson();
            Debug.Log("Task liste ended");
            return;
        }

        
    }

    public void ReadJson()
    {
        /*
        if(jsonText != null)
            myTaskListe = JsonUtility.FromJson<TaskListe>(jsonText.text);
        */
        StreamReader reader;
        reader = new StreamReader(File.OpenRead(Application.streamingAssetsPath + "/" + dataPath));
        string json = "";
        while (!reader.EndOfStream)
        {
            json+= reader.ReadLine();

        }
        reader.Close();
        myTaskListe = JsonUtility.FromJson<TaskListe>(json);
    }

    public void WriteJson()
    {
        string tasks = JsonUtility.ToJson(myTaskListe);
        //File.WriteAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonDesktopDirectory) + "/TaskListeUpgraded.json", tasks);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/TaskListeUpgraded.json", tasks);
    }


    public bool Unanimite(out string result)
    {
        foreach (string value in playerValue)
        {
            if (value != playerValue[0])
            {
                result = null;
                return false;
            }
        }
        result = playerValue[0];
        return true;    
    }

    public string Moyenne(string[] values)
    {
        float result = 0;
        int i = 0;
        foreach (string value in values)
        {
            if(value != "Cafée?" && value != "?")
            {
                result += int.Parse(value);
                i++;
            }
                
        }
        result /= i;
        result = Mathf.FloorToInt(result);
        return result.ToString();
    }

    public string Mediane(string[] values)
    {
        List<Player> numbersValue = new List<Player>();
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] != "Cafée?" && values[i] != "?")
            {
                Player p = new Player();
                p.value = int.Parse(values[i]);
                numbersValue.Add(p);
            }
        }
        numbersValue.Sort(SortByValue);

        return numbersValue[numbersValue.Count/2].value.ToString();
    }

    public void Extrem(string[] players, string[] values, out string max, out string min)
    {
        List<Player> numbersValue = new List<Player>();

        for (int i = 0; i < players.Length; i++)
        {
            if (values[i] != "Cafée?" && values[i] != "?")
            {
                Player p = new Player();
                p.playerName = players[i];
                p.value = int.Parse(values[i]);
                numbersValue.Add(p);
            }
        }

        numbersValue.Sort(SortByValue);

        max = numbersValue[numbersValue.Count-1].playerName;
        min = numbersValue[0].playerName;


    }

    static int SortByValue(Player p1, Player p2)
    {
        return p1.value.CompareTo(p2.value);
    }

    [System.Serializable]
    public class Task
    {
        public string taskName;
        public string taskDifficulty;
    }

    [System.Serializable]
    public class TaskListe
    {
        public string difficulty;
        public Task[] taskListe;
    }

    [System.Serializable]
    public class Player
    {
        public string playerName;
        public float value;


    }
}
