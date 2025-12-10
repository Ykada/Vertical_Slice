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
    [SerializeField] private Canvas CampainstartCanvas;
    [SerializeField] private Canvas savingCanvas;
    [Header("Saving Canvas")]
    [SerializeField] private Button StartGameButten; // Saves the text and switches to nieuw scene
    [SerializeField] private InputField SaveGametext1;
    [SerializeField] private InputField SaveGametext2;
    

    private string savefilepath;

    private void Start()
    {
        mainMenuCanvas.enabled = true;
        CampainstartCanvas.enabled = false;
        savingCanvas.enabled = false;
        savefilepath = Path.Combine(Application.persistentDataPath, "MenuSave.txt");
        StartGameButten.onClick.AddListener(() =>
        {
            Savedata();
            //SceneManager.LoadScene(""); // Put the scene name here where you want to go
        });
        loaddata();
    }
    private void Update()
    {
    }

    private void Savedata()
    {
        MenuTextSavingData data = new MenuTextSavingData();
        data.SaveGame1 = SaveGametext1.text;
        data.SaveGame2 = SaveGametext2.text;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savefilepath, json);
        Debug.Log("Data Saved to " + savefilepath);
        Debug.Log(json);
    }
    private void loaddata()
    {
        if (File.Exists(savefilepath))
        {
            string json = File.ReadAllText(savefilepath);
            MenuTextSavingData data = JsonUtility.FromJson<MenuTextSavingData>(json);
            Debug.Log(json);
            SaveGametext1.text = data.SaveGame1;
            SaveGametext2.text = data.SaveGame2;

        }
        else {
            Debug.Log("No save file found at " + savefilepath);
        }
        Debug.Log("Data Loaded from " + savefilepath);
        
    }

    public class MenuTextSavingData
    {
        public string SaveGame1;
        public string SaveGame2;
    }
}
