using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CsvReader
{
    private const string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    private const string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    //private static char[] TRIM_CHARS = { '\"' };

    public static List<string[]> ReadAsList(string pathFromResourcesFolder)
    {
        if (string.IsNullOrEmpty(pathFromResourcesFolder))
        {
            Debug.LogError("Trying to read a 'null' CsvOnlineSource");
            return null;
        }

        List<string[]> table = new List<string[]>();

        TextAsset csvFile = Resources.Load(pathFromResourcesFolder) as TextAsset;

        if (csvFile == null) return table;
        
        string[] lines = Regex.Split(csvFile.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return null;

        string[] headers = Regex.Split(lines[0], SPLIT_RE);

        foreach (string t in lines)
        {
            string[] values = Regex.Split(t, SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            string[] rowInTable = new string[headers.Length];
            for (int j = 0; j < headers.Length && j < values.Length; j++)
            {
                //Clean value
                string value = values[j].Replace("\"\"", "\"");
                value = UnquoteString(value);
                value = value.Replace("\\", "");
                string finalValue = value;

                rowInTable[j] = finalValue;
            }
            table.Add(rowInTable);
        }

        return table;
    }

    private static string UnquoteString(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        int length = str.Length;
        if (length > 1 && str[0] == '\"' && str[length - 1] == '\"')
            str = str.Substring(1, length - 2);

        return str;
    }
    
    public static string[,] ReadAsArray(string pathFromResourcesFolder)
    {
        if (string.IsNullOrEmpty(pathFromResourcesFolder))
        {
            Debug.LogError("Trying to read a 'null' CsvOnlineSource");
            return null;
        }

        List<string[]> lines = ReadAsList(pathFromResourcesFolder);
        int rows = lines.Count;
        int columns = lines[0].Length;
        
        string[,] table = new string[rows,columns];

        for (int row = 0; row < rows; row++)
            for (int col = 0; col < columns; col++)
                table[row, col] = lines[row][col];

        return table;
    }


    /*   // ORIGINAL METHOD //
    public static List<Dictionary<string, object>> Read(string fileName) 
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(fileName) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j].Replace("\"\"", "\"");
                value = UnquoteString(value);
                value = value.Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
    */
}