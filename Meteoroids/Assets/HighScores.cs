using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public HighScoreData data;

    public string highScoresFileName = "com.charlierobin.meteoroids.high-scores.json";

    private string pathToHighScoresStorageLocation()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX)
        {
            path = Path.Combine(path, "Library/Preferences");
        }
        else if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Linux)
        {
            path = Path.Combine(path, "Documents");
        }

        return path;
    }

    private string pathToHighScores()
    {
        return Path.Combine(this.pathToHighScoresStorageLocation(), this.highScoresFileName);
    }

    private void Start()
    {
        this.name = typeof(HighScores).Name;

        string path = this.pathToHighScores();

        string json = "{\"entries\":[]}";

        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path, true);

            json = reader.ReadToEnd();

            reader.Close();
        }

        this.data = JsonUtility.FromJson<HighScoreData>(json);

        this.data.sort();
    }

    private void OnDestroy()
    {
        string json = JsonUtility.ToJson(this.data);

        string path = this.pathToHighScores();

        StreamWriter writer = new StreamWriter(path, false);

        writer.Write(json);

        writer.Flush();
        writer.Close();
    }

    public bool check(int score)
    {
        if (score == 0) return false;

        if (this.data.entries.Count < 10) return true;

        foreach (HighScoreEntry entry in this.data.entries)
        {
            if (score > entry.score) return true;
        }

        return false;
    }

    public void add(string name, int score)
    {
        HighScoreEntry e = new HighScoreEntry();

        e.name = name;
        e.score = score;

        this.data.add(e);
    }
}

[Serializable]

public class HighScoreData
{
    public List<HighScoreEntry> entries = new List<HighScoreEntry>();

    public void sort()
    {
        this.entries.Sort(CompareEntriesByScore);
    }

    private int CompareEntriesByScore(HighScoreEntry x, HighScoreEntry y)
    {
        if (x.score > y.score)
        {
            return -1;
        }
        else if (x.score < y.score)
        {
            return 1;
        }

        return 0;
    }

    public void add(HighScoreEntry e)
    {
        bool handled = false;

        foreach (HighScoreEntry entry in this.entries)
        {
            if (e.score > entry.score)
            {
                this.entries.Insert(this.entries.IndexOf(entry), e);

                handled = true;

                break;
            }
        }

        if (!handled)
        {
            this.entries.Add(e);
        }

        while (this.entries.Count > 10)
        {
            this.entries.RemoveAt(10);
        }
    }
}

[Serializable]

public class HighScoreEntry
{
    public string name;
    public int score;
}

