using UnityEngine;
using SimpleJSON;
using CustomFileFunc;

public class SaveData : MonoBehaviour
{
    public int instaceID => gameObject.GetInstanceID();
    protected string wantedFileName;
    public virtual JSONObject SaveValuesToJson()
    {
        JSONObject dataJSONObj = new JSONObject();
        dataJSONObj["Name"].Add(gameObject.name);
        return dataJSONObj;
    }

    public void SaveDataToJSONObj()
    {
        var saveManager = SaveManager.current;
        if (saveManager != null)
        {
            saveManager.SaveToCurJsonObjects(CustomFuncs.GetFileByNameJson(wantedFileName), SaveValuesToJson(), instaceID);

        }
        else
        {
            Debug.Log("Savemanager current is null");
        }
    }

}
