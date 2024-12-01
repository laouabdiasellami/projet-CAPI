using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public TextAsset jsonText;
    public TaskListe myTaskListe = new TaskListe();

    public int currentTask =-1;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }


    void Start()
    {
        ReadJson();
        TaskUpdate();
    }

    public void TaskUpdate()
    {
        if(currentTask<myTaskListe.taskListe.Length-1)
            currentTask++;
        else
        {
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




    [System.Serializable]
    public class Task
    {
        public string taskName;
        public string taskDifficulty;
    }

    [System.Serializable]
    public class TaskListe
    {
        public Task[] taskListe;
    }
}
