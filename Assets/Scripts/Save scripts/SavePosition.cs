using UnityEngine;

public class SavePosition : MonoBehaviour, ISaveData
{
    [SerializeField] private SavePositionData savePositionData;
    private void Start()
    {

        if (SaveManager.current != null) { SaveManager.current.saveDataEvent += SendSavedData; }
        if (savePositionData == null)
        {
            savePositionData = new SavePositionData();
        }
    }

    public void SetSavePositionData(SavePositionData data)
    {
        savePositionData = data;
    }

    public void SendSavedData()
    {
        savePositionData.currentObject = this.gameObject;
        savePositionData.objectPosition = this.gameObject.transform.position;

        if (SaveManager.current != null)
        {
            SaveManager.current.saveManagersData.positionData.Add(savePositionData);
        }
    }
    public void SaveDataValue()
    {
        Debug.Log("FUCK");
        if (SaveManager.current != null) { SaveManager.current.saveManagersData.savedDatas.Add(this); }
    }
    public void LoadDataValue()
    {
        
    }   
}

[System.Serializable]
public class SavePositionData
{
    public GameObject currentObject;
    public Vector2 objectPosition;

    public void LoadData()
    {
        currentObject.GetComponent<SavePosition>().SetSavePositionData(this);
        currentObject.transform.position = objectPosition;
    }

    public void SaveDataVal(GameObject obj)
    {
        
    }
}
