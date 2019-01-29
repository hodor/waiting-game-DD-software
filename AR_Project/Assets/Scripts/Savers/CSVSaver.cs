using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AR_Project.Savers;
using UnityEngine;

public class CSVSaver {

    private List<string[]> rowData = new List<string[]>();

    string fileName;
    string filePath;
    string delimiter = ",";

    StreamWriter writer;
    StreamReader reader;
    StringBuilder sb;

    public void SaveCSV()
    {
        sb = new StringBuilder();
        fileName = GetFileName();
        AddUserData();

        string[][] output = new string[rowData.Count][];
        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);


        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));
            
        filePath = GetPath();

        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        filePath += GetFileName();

        if (File.Exists(filePath))
            File.Delete(filePath);

        writer = File.CreateText(filePath);
        writer.WriteLine(sb);
        writer.Close();
    }
    void AddUserData()
    {
        string[] rowDataTitles = new string[5];
        rowDataTitles[0] = "Nome";
        rowDataTitles[1] = "Data de nascimento";
        rowDataTitles[2] = "Gênero";
        rowDataTitles[3] = "Personagem";
        rowDataTitles[4] = "Total de pontos";
        rowData.Add(rowDataTitles);

        ReadCSV();

        var rowDataUser = new string[5];
        rowDataUser[0] = PlayerPrefsSaver.instance.name;
        rowDataUser[1] = PlayerPrefsSaver.instance.birthday;
        rowDataUser[2] = PlayerPrefsSaver.instance.gender;
        rowDataUser[3] = PlayerPrefsSaver.instance.character.name.ToString();
        rowDataUser[4] = PlayerPrefsSaver.instance.totalPoints.ToString();
        rowData.Add(rowDataUser);
    }

    string GetFileName()
    {
        return "/Dados_Jogador.csv";
    }

    void ReadCSV()
    {
        Debug.Log("READ CSV");

        filePath = GetPath() + GetFileName();
        if (!File.Exists(filePath))
            return;

        reader = new StreamReader(filePath);

        string line;
        string firstLineHeader = reader.ReadLine();
        Debug.Log("First Line: " + firstLineHeader);

        while ((line = reader.ReadLine()) != null)
        {
            Debug.Log("Line: " + line);
            if (line != "")
            {
                string[] split = line.Split(',');

                var rowDataUser = new string[5];
                rowDataUser[0] = split[0];
                rowDataUser[1] = split[1];
                rowDataUser[2] = split[2];
                rowDataUser[3] = split[3];
                rowDataUser[4] = split[4];
                rowData.Add(rowDataUser);

            }
        }
    }

    public string GetPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/";
#elif PLATFORM_STANDALONE_WIN
        return Application.dataPath + "/Data/";
#endif

    }
}
