using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public string difficulty;
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

    void Awake()
    {
        if (instance == null)
            instance = this;
    }


    void Start()
    {
        ReadJson();
        myTaskListe.difficulty = difficulty;
        TaskUpdate();
        UiManager.instance.CardInit(cardDeck);
        UiManager.instance.NextPlayer(playerListe[currentPlayer]);
        
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
            if(currentTurn ==0)
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
                    UiManager.instance.NextPlayer(playerListe[currentPlayer]);
                }
            }
            else if(myTaskListe.difficulty == "Moyenne")
            {
                myTaskListe.taskListe[currentTask].taskDifficulty = Moyenne();
                currentPlayer = 0;
                currentTurn = 0;
                UiManager.instance.NextPlayer(playerListe[currentPlayer]);
                TaskUpdate();
            }

            /*
            currentPlayer = 0;
            UiManager.instance.NextPlayer(playerListe[currentPlayer]);
            int val = 0;
            foreach (string value in playerValue)
            {
                val += int.Parse(value);
            }
            myTaskListe.taskListe[currentTask].taskDifficulty = val.ToString();

            TaskUpdate();
            */
        }
        
    }

    public void TaskUpdate()
    {
        if(currentTask<myTaskListe.taskListe.Length-1)
            currentTask++;
        else
        {
            WriteJson();
            Debug.Log("Task liste ended");
            return;
        }

        UiManager.instance.DisplayTask(currentTask+1, myTaskListe.taskListe[currentTask].taskName);
    }

    public void ReadJson()
    {
        myTaskListe = JsonUtility.FromJson<TaskListe>(jsonText.text);
    }

    public void WriteJson()
    {
        string tasks = JsonUtility.ToJson(myTaskListe);
        File.WriteAllText(Application.dataPath + "/TaskListeUpgraded.json", tasks);
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

    public string Moyenne()
    {
        float result = 0;
        foreach (string value in playerValue)
        {
            if(value != "Cafée?" && value != "?")
                result += int.Parse(value);
        }
        result /= playerListe.Length;
        return result.ToString();
    }

    public void Extrem(out string max, out string min)
    {
        float[] intValue;

        foreach (string value in playerValue)
        {
            if (value != "Cafée?" && value != "?")
                
        }

        max = "player1";
        min = "player2";
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
}
