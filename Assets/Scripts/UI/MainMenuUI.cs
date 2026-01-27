using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Used for Saving Data for the ui and other stuff yes :3
// Ykada was here

public class MainMenuUI : MonoBehaviour
{


    [Header("UI Canvas")]
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private GameObject CampainstartCanvas;
    [SerializeField] private GameObject savingCanvas;

    [Header("Main Menu Canvas")]
    [SerializeField] private Button StartButton; // Switches to CampainstartCanvas

    [Header("Saving Canvas")]
    [SerializeField] private Button StartGameButten; // Saves the text and switches to nieuw scene
    [SerializeField] private InputField SaveGametext1;
    [SerializeField] private InputField SaveGametext2;

    [Header("transform of objects")]
    [SerializeField] private Transform Backgroundimage;
    [SerializeField] private Transform[] othertransforms;

    private string savefilepath;

    private void Start()
    {
        /* CampainstartCanvas.SetActive(true);
         savingCanvas.SetActive(false);
         savefilepath = Path.Combine(Application.persistentDataPath, "MenuSave.txt");
         StartButton.onClick.AddListener(() =>
         {
             CampainstartCanvas.SetActive(false);
             savingCanvas.SetActive(true);
             Backgroundimage.transform.position = new Vector3(1955, 950, 0);
             othertransforms[0].transform.position = new Vector3(1955, 1050, 0);
             othertransforms[1].transform.position = new Vector3(1955, 850, 0);
             othertransforms[2].transform.position = new Vector3(1955, 750, 0);
             othertransforms[3].transform.position = new Vector3(1955, 850, 0);
             othertransforms[4].transform.position = new Vector3(1955, 750, 0);
             othertransforms[5].transform.position = new Vector3(1955, 950, 0);


         }); */
        StartGameButten.onClick.AddListener(() =>
        {
            //Savedata();
            SceneManager.LoadScene("Artist test"); // Put the scene name here where you want to go
        });
        //loaddata();
    }
    private void Update()
    {

    }
}

 /*   private void Savedata()
    {
        MenuTextSavingData data = new MenuTextSavingData();
        data.SaveGame1 = SaveGametext1.text;
        data.SaveGame2 = SaveGametext2.text;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savefilepath, json);
    }
    private void loaddata()
    {
        if (File.Exists(savefilepath))
        {
            string json = File.ReadAllText(savefilepath);
            MenuTextSavingData data = JsonUtility.FromJson<MenuTextSavingData>(json);
            SaveGametext1.text = data.SaveGame1;
            SaveGametext2.text = data.SaveGame2;

        }
        else {
            Debug.Log("No save file found at " + savefilepath);
        }
        
    }

    public class MenuTextSavingData
    {
        public string SaveGame1;
        public string SaveGame2;
    }
}*/
