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
        Debug.Log("저장되었습니다");

        ScoreSaveWrapper wapper = new ScoreSaveWrapper( GameManager.Instnace.playerScoreList );

        // 직렬화 
        string json = JsonUtility.ToJson( wapper );

        Debug.Log(json);

        CreateJsonFile( json , saveFileName);
    }

    private void CreateJsonFile(string jsonString ,string saveFileName) 
    {
        // 위치/이름.json
        // 저장경로에 생성
        try
        {
            string fullPath = Path.Combine(savePath, saveFileName + ".json");
            using (StreamWriter sw = File.CreateText(fullPath))
            {
                sw.Write(jsonString);
            }
            Debug.Log($"파일이 성공적으로 저장되었습니다: {fullPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"파일 저장 중 오류 발생: {e.Message}");
        }
    }

    public ScoreSaveWrapper F_Load() 
    {
        try 
        {
            string fullPath = Path.Combine(savePath, saveFileName + ".json");
            string desString = File.ReadAllText(fullPath);

            // 받아온 json 을 역 직렬화
            ScoreSaveWrapper wapper = JsonUtility.FromJson<ScoreSaveWrapper>(desString);

            Debug.Log($"파일을 성공적으로 불러왔습니다 : {fullPath}");

            // ScroeSaveWrapper 리턴
            return wapper;
        }
        catch (Exception e)
        {
            Debug.LogError($"파일 불러오는 중 오류 발생: {e.Message}");

            return null;
        }
    }

}
