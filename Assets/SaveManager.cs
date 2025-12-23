using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using SimpleJSON;
using CustomFileFunc;
using JetBrains.Annotations;

public class SaveManager : MonoBehaviour
{
    public static SaveManager current;
    public string sceneString;

    // public event Action saveDataEvent;
    public event Action<GameObject> savePositionEvent;
    public event Action saveDataEvent;
    // public Dictionary<string, JSONNode> curJsonObjects = new Dictionary<string, JSONNode>();
    public Dictionary<string, JSONArray> curJsonObjects = new Dictionary<string, JSONArray>();
    public Dictionary<string, Dictionary<string, JSONArray>> fuck = new Dictionary<string, Dictionary<string, JSONArray>>();
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

    public class SaveManagersStringFiles
    {
        public List<string> dataFileLocations = new List<string>();
    }
    public SaveManagersStringFiles savedFilesString;

    //File locaitons
    
    private const string SAVEFILEFOLDER = "SceneData";
    private const string DATAFOLDERLOCATIONS = "DataFolderLocations";


    void OnEnable()
    {
        if (current == null) { current = this; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SafteyStartFile();


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
            SaveDataToJson();
        }




    }
    
    private void SaveDataEvents()
    {
        if (savePositionEvent != null)
        {

        }

        //put in all saved keys into an array and save that as a JSONARRAY into a seperate file using the saveUsedFiles variable
    }

    private void SaveDataToJson()
    {
        
    }

    //This ensures that if a different object tries to load data from the savemanager but there is no file it will create one.
    //Really just a saftey measure so no weird errors happen with accessing the files
    private void SafteyStartFile()
    {

        string sceneDataLocation = CustomFuncs.GetFileByNameFolder(SAVEFILEFOLDER);
        string mainDataFolder = CustomFuncs.GetFileByNameFolder(sceneString, SAVEFILEFOLDER);
        string currentFolder = SAVEFILEFOLDER + Path.AltDirectorySeparatorChar + sceneString;//This holds the extra strings that lead us to the scenes specific folder.
        string dataFolderLocations = CustomFuncs.GetFileByNameJson(DATAFOLDERLOCATIONS, currentFolder);
        if (File.Exists(CustomFuncs.GetFileByNameJson(dataFolderLocations)))
        {
            CustomFuncs.CreateFolder(sceneDataLocation);
            CustomFuncs.CreateFolder(mainDataFolder);
            CustomFuncs.CreateJsonFile(dataFolderLocations);
            if (File.ReadAllText(dataFolderLocations) == "")
            {
                savedFilesString = new SaveManagersStringFiles();
            }
            else
            {
                savedFilesString = JsonUtility.FromJson<SaveManagersStringFiles>(File.ReadAllText(dataFolderLocations));
                if (savedFilesString.dataFileLocations.Count > 0)
                {
                    foreach (string file in savedFilesString.dataFileLocations)
                    {
                        fuck.Add(file, )
                    }
                }
            }



        }

        


        //Get the folder and json file location
        //Create datafile locations if needed
        //Create folder if needed
        //Create json if needed
    }

    
    
}

