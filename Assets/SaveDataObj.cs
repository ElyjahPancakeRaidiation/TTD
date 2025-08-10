using Unity.Mathematics;
using UnityEngine;


public class SaveDataObj : MonoBehaviour
{
    public SaveData saveData;
    public ExtraSaveData extraSaveData;

    private void Start()
    {
        if (SaveManager.current != null) { SaveManager.current.saveDataEvent += SendSavedData; }
        if (saveData == null)
        {
            saveData = new SaveData();
        }
    }

    private void SendSavedData()
    {
        saveData.objSavedPosition = gameObject.transform.position;
        saveData.objSavedName = gameObject.name;

        //Need to think about this more
        if (extraSaveData != null) { saveData.extraSavedData = extraSaveData; }

        SaveManager.current.saveManagersData.savedData.Add(saveData);
        SaveManager.current.saveManagersData.savedObjects.Add(this.gameObject);
    }

    public void RecieveSavedData(SaveData recievedData)
    {
        saveData = recievedData;
    }
}


[System.Serializable]
public class SaveData
{
    public Vector3 objSavedPosition;
    public string objSavedName;
    public ExtraSaveData extraSavedData;

    public void LoadData()
    {
        
    }
}

[System.Serializable]
public abstract class ExtraSaveData
{
    public abstract void SaveExtraData();
}
