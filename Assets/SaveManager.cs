using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using SimpleJSON;
using CustomFileFunc;

public class SaveManager : MonoBehaviour
{
    public static SaveManager current;
    public string sceneString;
    public SaveManagersData saveManagersData;

    // public event Action saveDataEvent;
    public event Action<GameObject> savePositionEvent;
    public event Action saveDataEvent;
    // public Dictionary<string, JSONNode> curJsonObjects = new Dictionary<string, JSONNode>();
    public Dictionary<string, JSONArray> curJsonObjects = new Dictionary<string, JSONArray>();
    public void SaveToCurJsonObjects(string fileName, JSONObject jsonObject, int objectID)
    {
        JSONArray arr;
        bool containsKey = curJsonObjects.ContainsKey(fileName);
        if (containsKey)
        {
            arr = curJsonObjects[fileName];
        }
        else
        {
            arr = new JSONArray();
        }
        arr.Add(objectID.ToString(), jsonObject);

        if (containsKey)
        {
            curJsonObjects[fileName] = arr;
        }
        else
        {
            curJsonObjects.TryAdd(fileName, jsonObject.AsArray);
        }

    }

    [System.Serializable]
    public class SaveManagersData
    {
        public List<SavePositionData> positionData = new List<SavePositionData>();
        public List<ISaveData> savedDatas = new List<ISaveData>();
        public void CreateEmptyList()
        {
            positionData = new List<SavePositionData>();
        }
        public void LoadAllData()
        {
            foreach (ISaveData data in savedDatas)
            {
                data.LoadDataValue();
            }
        }
    }

    public class SaveManagersStringFiles
    {
        public List<string> dataFileLocations = new List<string>();
    }

    //File locaitons
    private string saveJsonData;
    private string dataFileLocation;
    private string saveUsedFiles;
    private const string saveFileFolders = "SceneData";


    void OnEnable()
    {
        if (current == null) { current = this; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string sceneDataLocation = CustomFuncs.GetFileByNameFolder(saveFileFolders);



        //Get the folder and json file location
        //Create datafile locations if needed
        //Create folder if needed
        //Create json if needed


        dataFileLocation = CustomFuncs.GetFileByNameJson(sceneString);
        // if (!File.Exists(jsonFileLocation))
        // {
        //     File.Create(jsonFileLocation);
        // }

        if (File.ReadAllText(jsonFileLocation) == "")
        {
            saveManagersData = new SaveManagersData();
        }
        else
        {
            saveJsonData = File.ReadAllText(jsonFileLocation);
            saveManagersData = JsonUtility.FromJson<SaveManagersData>(saveJsonData);
            // var data = JSONNode.LoadFromBinaryFile(saveJsonData);
            var data = JSONNode.Parse(saveJsonData);
            // saveManagersData = JSONNode.Parse(saveJsonData);

            LoadDataFromJson(saveManagersData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveDataEvents();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            SaveDataToJson(saveManagersData);
        }




    }
    
    private void SaveDataEvents()
    {
        saveManagersData.CreateEmptyList();
        if (savePositionEvent != null)
        {

        }

        //put in all saved keys into an array and save that as a JSONARRAY into a seperate file using the saveUsedFiles variable
    }

    private void SaveDataToJson(SaveManagersData data)
    {
        saveJsonData = JsonUtility.ToJson(data, true);

        File.WriteAllText(jsonFileLocation, saveJsonData);
    }

    //This ensures that if a different object tries to load data from the savemanager but there is no file it will create one.
    //Really just a saftey measure so no weird errors happen with accessing the files
    private void SafteyStartFile()
    {
        string sceneDataLocation = CustomFuncs.GetFileByNameFolder(saveFileFolders);
        string mainDataFolder = CustomFuncs.GetFileByNameFolder(sceneString, saveFileFolders);
        CustomFuncs.CreateFolder(sceneDataLocation);
        string 


        //Get the folder and json file location
        //Create datafile locations if needed
        //Create folder if needed
        //Create json if needed
    }

    
    
}

