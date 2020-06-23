using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void OnClickedSinglePlayer()
    {
        Debug.Log("LocalPlayer");
        DirectoryInfo path = new DirectoryInfo(Application.dataPath + "/Levels");
        Debug.Log((new DirectoryInfo(Application.dataPath + "/Levels")).ToString());
        Debug.Log(GetAllLevels().Count);

    }
    public void OnClickedMultiPlayer()
    {
        Debug.Log("Online");
    }
    public void OnClickedOptions()
    {
        Debug.Log("Options");
    }
    public void OnClickedQuit()
    {
        Application.Quit(0);
        UnityEditor.EditorApplication.isPlaying = false;
    }
    String[] LevelPaths = { "/Levels", "/Scenes" };
    private List<LevelClass> GetAllLevels()
    {
        List<LevelClass> levels = new List<LevelClass>();
        DirectoryInfo path = new DirectoryInfo(Application.dataPath + "/Levels");
        foreach (String subpath in LevelPaths)
        {
            levels.AddRange(GetLevelsInPath(new DirectoryInfo(Application.dataPath + subpath)));
        }
        return levels;
    }
    private List<LevelClass> GetLevelsInPath(DirectoryInfo path)
    {
        List<LevelClass> l = new List<LevelClass>();
        foreach (FileInfo info in path.GetFiles("."))
        {
            if (info.Extension.Equals(".unity"))
            {
                l.Add(new LevelClass(null, info.FullName, null, new DirectoryInfo(path + info.Name + info.Extension)));
                DirectoryInfo lo = new DirectoryInfo(path + "/" + info.Name);
                if (lo.Exists)
                {
                    Debug.Log("Works");
                }
                else
                {
                    Debug.Log("Fail");
                    Debug.Log(lo.ToString());
                }
            }
        }
        return l;
    }
}
