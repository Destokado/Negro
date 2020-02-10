using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New CSV online source File", menuName = "CSV online source")]
public class CsvOnlineSource : ScriptableObject
{
    [Tooltip("The name that will have the downloaded file. If it is not defined, this object's name will be used.")]
    [SerializeField] private string _downloadedFileName;
    
    [TextArea]
    public string url;

    public string downloadedFileName{ 
        get{
            if(string.IsNullOrEmpty(_downloadedFileName))
                return this.ToString();
            return _downloadedFileName;
        }
        set { _downloadedFileName = value; }
    }

    /*
    public int quantityOfLines
    {
        get
        {
            if (_quantityOfLines < 0) 
                _quantityOfLines = CalculateQuantityOfSentences();
            return _quantityOfLines;
        }
        private set { _quantityOfLines = value; }
    }

    private int _quantityOfLines = -1;
    */
    
    /*
    private int CalculateQuantityOfSentences()
    {
        return CsvReader.Read(this).ToArray().Length;
    }
    */
    
    public override string ToString()
    {
        return name;
    }
    
    private sealed class LocalizationUrlEqualityComparer : IEqualityComparer<CsvOnlineSource>
    {
        public bool Equals(CsvOnlineSource x, CsvOnlineSource y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.url == y.url;
        }

        public int GetHashCode(CsvOnlineSource obj)
        {
            return (obj.url != null ? obj.url.GetHashCode() : 0);
        }
    }
    public static IEqualityComparer<CsvOnlineSource> localizationUrlComparer { get; } = new LocalizationUrlEqualityComparer();
    
    
    public static CsvOnlineSource[] GetAllInAssets()
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(CsvOnlineSource).Name);
        CsvOnlineSource[] localizationFiles = new CsvOnlineSource[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            localizationFiles[i] = AssetDatabase.LoadAssetAtPath<CsvOnlineSource>(path);
        }

        return localizationFiles;
    }
    
}