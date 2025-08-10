using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class SaveManager : MonoBehaviour
{
    public static SaveManager current;
    public SaveManagersData saveManagersData;
    public event Action saveDataEvent;

    [System.Serializable]
    public class SaveManagersData
    {
        public List<GameObject> savedObjects;
        public List<SaveData> savedData;
    }

    private string saveJsonData;
    private string jsonFileLocation;

    void OnEnable()
    {
        if (current == null) { current = this; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jsonFileLocation = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SavedDataObjects.json";

        if (!File.Exists(jsonFileLocation))
        {
            File.Create(jsonFileLocation);
        }

        if (File.ReadAllText(jsonFileLocation) == "")
        {
            saveManagersData = new SaveManagersData();

            saveManagersData.savedData = new List<SaveData>();
            saveManagersData.savedObjects = new List<GameObject>();
        }

        saveManagersData = JsonUtility.FromJson<SaveManagersData>(saveJsonData);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            saveDataEvent();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            SaveDataToJson(saveManagersData);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {

        }
    }

    private void SaveDataToJson(SaveManagersData data)
    {
        // data.T();

        saveJsonData = JsonUtility.ToJson(data, true);

        File.WriteAllText(jsonFileLocation, saveJsonData);
    }

    private void LoadDataFromJson(SaveManagersData data)
    {
        for (int i = 0; i < data.savedObjects.Count; i++)
        {
            var obj = GameObject.Find(data.savedData[i].objSavedName).GetComponent<SaveDataObj>();
            obj.RecieveSavedData(data.savedData[i]);
        }
        

    }
    
    
}

