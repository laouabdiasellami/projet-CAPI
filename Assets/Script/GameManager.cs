using UnityEngine;
using System.IO;
using System.Collections.Generic;

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
        UiManager.instance.NextTask(currentTask + 1, myTaskListe.taskListe[currentTask].taskName);
        //UiManager.instance.NextPlayer(playerListe[currentPlayer]);
        
    }

    public bool Paire(int x)
    {
        return x % 2 == 0;
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
                    UiManager.instance.TalkTime();
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
        if(jsonText != null)
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
        List<Player> numbersValue = new List<Player>();

        for(int i=0; i < playerListe.Length -1 ;i++)
        {
            if (playerValue[i] != "Cafée?" && playerValue[i] != "?")
            {
                Player p = new Player();
                p.playerName = playerListe[i];
                p.value = int.Parse(playerValue[i]);
                numbersValue.Add(p);
            }
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

    [System.Serializable]
    public class Player
    {
        public string playerName;
        public float value;
    }
}
