using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CsLocalization : MonoBehaviour
{
    

    [SerializeField] TextAsset csvFile;
    [SerializeField] string language;

    [SerializeField] TMP_FontAsset _fontAssetRu;
    [SerializeField] TMP_FontAsset _fontAssetCn;
    [SerializeField] TMP_FontAsset _fontAssetKr;
    [SerializeField] TMP_FontAsset _fontAssetJp;

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

    public void SetLanguage(string a)
    {
        language = a;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public string GetLanguage()
    {
    return language;
    }
    public TMP_FontAsset GetAtlas()
    {
        if (language == "") language = "RU";

        if (language == "RU")
            return _fontAssetRu;
        else if(language == "CN")
            return _fontAssetCn;
        else if (language == "KR")
            return _fontAssetKr;
        else if(language == "JP")
            return _fontAssetJp;
        else return _fontAssetRu;


    }

    public string GetText(string rowKey)
    {
        if (language == "") language = "RU";

        if (table.ContainsKey(rowKey) && table[rowKey].ContainsKey(language))
            return table[rowKey][language];
        return rowKey;
    }

    public void SetText(TextMeshProUGUI textMesh, string rowKey, string colKey)
    {
        textMesh.text = GetText(rowKey);
    }

}
