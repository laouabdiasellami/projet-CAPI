using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public int nbPlayer;
    public string jsonPath;
    public string gamerule;
    public List<string> playersName = new List<string>();
    
    [Header("UI")]
    [SerializeField] private Transform players;

    

    //Modifie le nombre de joueur
    public void ChangePlayerCount(TMP_InputField player)
    {
        //Vérification que nbPlayer est bien entre 2-8
        if(player.text != null || player.text != "")
        {
            nbPlayer = Mathf.Clamp(int.Parse(player.text), 2, 8);
            player.text = nbPlayer.ToString();
        }
        else
        {
            nbPlayer = 2;
            player.text = nbPlayer.ToString();
        }

        //desactiver les slot player non utiliser
        for (int i =1; i<= 8;i++)
        {
            TMP_InputField p = players.GetChild(i).GetComponent<TMP_InputField>();
            p.interactable = i <= nbPlayer;
            
        }
    }

    //Modifie les régles du jeu
    public void ChangeRule(TMP_Dropdown rule)
    {
        gamerule = rule.options[rule.value].text;
    }

    //Modifie le nom du fichier json à importer
    public void ChangePath(TMP_InputField path)
    {
        jsonPath = path.text;

        //Ajoute .json si pas présent
        if(!jsonPath.Contains(".json"))
        {
            jsonPath += ".json";
            path.text = jsonPath;
        }
    }

    public void StartGame()
    {
        //remplie liste des noms de joueurs
        for (int i = 1; i <= nbPlayer; i++)
        {
            TMP_InputField p = players.GetChild(i).GetComponent<TMP_InputField>();
            playersName.Add(p.text);
        }

        GameManager.instance.StartGame(jsonPath,gamerule,playersName,nbPlayer);

        this.gameObject.SetActive(false);
    }
}
