using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private string savePath;
    [SerializeField]
    private string saveFileName;

    private void Awake()
    {
        savePath = Application.persistentDataPath;
        // C:\Users\[user name]\AppData\LocalLow\[company name]\[product name]
        saveFileName = "ScoreSaveFile";
    }

    public void F_Save() 
    {
        Debug.Log("����Ǿ����ϴ�");

        ScoreSaveWrapper wapper = new ScoreSaveWrapper( GameManager.Instnace.playerScoreList );

        // ����ȭ 
        string json = JsonUtility.ToJson( wapper );

        Debug.Log(json);

        CreateJsonFile( json , saveFileName);
    }

    private void CreateJsonFile(string jsonString ,string saveFileName) 
    {
        // ��ġ/�̸�.json
        // �����ο� ����
        try
        {
            string fullPath = Path.Combine(savePath, saveFileName + ".json");
            using (StreamWriter sw = File.CreateText(fullPath))
            {
                sw.Write(jsonString);
            }
            Debug.Log($"������ ���������� ����Ǿ����ϴ�: {fullPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"���� ���� �� ���� �߻�: {e.Message}");
        }
    }

    public ScoreSaveWrapper F_Load() 
    {
        try 
        {
            string fullPath = Path.Combine(savePath, saveFileName + ".json");
            string desString = File.ReadAllText(fullPath);

            // �޾ƿ� json �� �� ����ȭ
            ScoreSaveWrapper wapper = JsonUtility.FromJson<ScoreSaveWrapper>(desString);

            Debug.Log($"������ ���������� �ҷ��Խ��ϴ� : {fullPath}");

            // ScroeSaveWrapper ����
            return wapper;
        }
        catch (Exception e)
        {
            Debug.LogError($"���� �ҷ����� �� ���� �߻�: {e.Message}");

            return null;
        }
    }

}
