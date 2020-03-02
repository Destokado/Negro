 using System;
using System.IO;
using System.Net;
 using System.Net.Cache;
 using UnityEditor;
using UnityEngine;

class CsvDownloader
{
    private class WebClientEx : WebClient
    {
        public WebClientEx(CookieContainer container)
        {
            this.container = container;
        }

        private readonly CookieContainer container = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest r = base.GetWebRequest(address);
            HttpWebRequest request = r as HttpWebRequest;
            if (request != null)
            {
                request.CookieContainer = container;
            }
            return r;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            WebResponse response = base.GetWebResponse(request, result);
            ReadCookies(response);
            return response;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            ReadCookies(response);
            return response;
        }

        private void ReadCookies(WebResponse r)
        {
            HttpWebResponse response = r as HttpWebResponse;
            
            if (response == null) 
                return;
            
            CookieCollection cookies = response.Cookies;
            container.Add(cookies);
        }
    }

    private static bool DownloadAndSaveIntoResources(CsvOnlineSource csvOnlineSource, string directoryPath = "Assets/Resources/")
    {
        Debug.Log("Downloading " + csvOnlineSource + " as csv.");
        
        if (csvOnlineSource == null)
        {
            Debug.LogError("Trying to download the file of a 'null' csvOnlineSource");
            return false;
        }

        /*
            INSTRUCTIONS:
            In your Google Spread, go to: File > Publish to the Web > Link > CSV
            You'll be given a link. Put that link into a WWW request and the text you get back will be your data in CSV form.
            // Example URL
            //string url = @"https://docs.google.com/spreadsheets/d/e/2PACX-1vQGs31fwKF9vuUg9uUOvgN8Jr7bVSQvDILQEMPk6xiKkzk3PDYosuOPMhd0FjrnKPzLkMA998tnZfGN/pub?output=csv"; //Published to the web
        */

        WebClientEx wc = new WebClientEx(new CookieContainer());
        wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:22.0) Gecko/20100101 Firefox/22.0");
        wc.Headers.Add("DNT", "1");
        wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        wc.Headers.Add("Accept-Encoding", "deflate");
        wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");
        wc.Headers.Add("Cache-Control", "no-cache");
        wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

        if (string.IsNullOrEmpty(csvOnlineSource.url))
        {
            Debug.LogError("The file referenced with the csvOnlineSource " + csvOnlineSource  + " can not be downloaded because the 'url' is not valid.");
            return false;
        }
        
        byte[] dt = wc.DownloadData(csvOnlineSource.url);

        if (dt.Length <= 0)
        {
            Debug.LogError("The downloaded data for the csvOnlineSource " + csvOnlineSource  + " is empty.");
            return false;
        }
        
        if(!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        File.WriteAllBytes(directoryPath + csvOnlineSource.downloadedFileName + ".csv", dt);

        //To convert it to string...
        //var outputCSVdata = System.Text.Encoding.UTF8.GetString(dt ?? new byte[] { });

        Debug.Log(csvOnlineSource + " file has been downloaded successfully.");
        
        return true;
    }

#if UNITY_EDITOR
    [MenuItem("Unity Essentials/CSV Manager/Download all")]
    [MenuItem("Negro/Update events")]
    public static bool DownloadAllCsvOnlineSources()
    {
        //Search all csvOnlineSource
        CsvOnlineSource[] csvOnlineSources = CsvOnlineSource.GetAllInAssets();

        if (csvOnlineSources.Length <= 0)
        {
            Debug.Log("No 'CsvOnlineSource' have been found in the assets folder.\nCreate them by right-clicking on the project structure > Create > CSV online source");
            return false;
        }

        //Download every csvOnlineSource
        foreach (CsvOnlineSource csvOnlineSource in csvOnlineSources)
            if (!DownloadAndSaveIntoResources(csvOnlineSource))
                return false;
        
        AssetDatabase.Refresh();

        Debug.Log("All csv files from online sources have been downloaded.");
        return true;
    }
#endif
    
#if UNITY_EDITOR
    [MenuItem("Unity Essentials/CSV Manager/Delete all")]
    public static void DeleteAllCsvFromOnlineSourcesInResources()
    {
        //Search all sections
        CsvOnlineSource[] csvOnlineSources = CsvOnlineSource.GetAllInAssets();

        //Delete all CsvOnlineSource
        foreach (CsvOnlineSource csvOnlineSource in csvOnlineSources)
            File.Delete("Assets/Resources/" + csvOnlineSource + ".csv");

        AssetDatabase.Refresh();

        Debug.Log("All csv files from online sources have been deleted.");
    }
#endif
}