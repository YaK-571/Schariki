using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CsLocalization : MonoBehaviour
{
    

    [SerializeField] TextAsset csvFile;

    private Dictionary<string, Dictionary<string, string>> table
        = new Dictionary<string, Dictionary<string, string>>();

    private List<string> columnHeaders = new List<string>();

    //синглтон
    public static CsLocalization Local;
    void Awake()
    {
        if (Local == null)
        {
            Local = this;
            DontDestroyOnLoad(gameObject); //сделай обьект неудаляемым

        }
        else
        {
            Destroy(gameObject);
        }
        LoadCSV("Local"); // считываем файл из Resources/Local.csv

    }

    void LoadCSV(string fileName)
    {
        csvFile = Resources.Load<TextAsset>(fileName);
        string[] lines = csvFile.text.Split('\n');

        if (lines.Length <= 1) return;

        // читаем заголовки колонок
        string[] headers = lines[0].Trim().Split(';');
        for (int i = 0; i < headers.Length; i++)
            columnHeaders.Add(headers[i].Trim());

        // читаем строки с данными
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Trim().Split(';');

            string rowKey = values[0]; // ID
            if (!table.ContainsKey(rowKey))
                table[rowKey] = new Dictionary<string, string>();

            for (int j = 1; j < headers.Length && j < values.Length; j++)
            {
                string colKey = headers[j];
                string cellValue = values[j];
                table[rowKey][colKey] = cellValue;
            }
        }
    }

    public string GetText(string rowKey, string colKey)
    {
        if (table.ContainsKey(rowKey) && table[rowKey].ContainsKey(colKey))
            return table[rowKey][colKey];
        return $"[NO DATA {rowKey}:{colKey}]";
    }

    public void SetText(TextMeshProUGUI textMesh, string rowKey, string colKey)
    {
        textMesh.text = GetText(rowKey, colKey);
    }

}
