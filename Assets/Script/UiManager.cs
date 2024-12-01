using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;
    public TextMeshProUGUI taskText;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void DisplayTask(int index,string taskName)
    {
        taskText.text = "Task n°" + index + ": " + taskName;
    }

    public void NextTask()
    {
        GameManager.instance.TaskUpdate();
    }
}
