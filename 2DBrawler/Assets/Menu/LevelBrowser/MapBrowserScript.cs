using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;

public class MapBrowserScript: MonoBehaviour
{
    //Paths where it scans for Levels, going from the MainGamePath
    String[] LevelPaths = {"/Levels","/Scenes"};
    // Use this for initialization
    public GameObject MapButton;
    public Transform SpawnPoint;
    public RectTransform content;
    void Start()
    {
        PopulateScrollBox();
    }
    private void PopulateScrollBox() {
        List<LevelClass> LevelList = GetAllLevels();
        content.sizeDelta = new Vector2(0, LevelList.Count * 60);
        for (int i = 0; i < LevelList.Count; i++)
        {
            // 60 width of item
            float spawnY = i * 60;
            //newSpawn Position
            Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
            //instantiate item
            GameObject SpawnedItem = Instantiate(MapButton, pos, SpawnPoint.rotation);
            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            //get ItemDetails Component
            LevelClass Details = null;
            try
            {
                Details = SpawnedItem.GetComponent<GameObject>().GetComponent<MapLoader>().Level;
                if (Details != null) { Debug.Log("WORKS!!!!"); }
            }
            catch (Exception e) {
            }
            if (SpawnedItem.GetComponentInChildren<MapLoader>() != null) { Debug.Log("MapLoadScript"); }
            //SpawnedItem.GetComponentInChildren<MapLoader>().Level.Name = "ITFUCKINGWORKS";
            //LevelList[i].Description.text = "ITFUCKINGWORKS";
            SpawnedItem.GetComponentInChildren<MapLoader>().Level = LevelList[i];
            if (SpawnedItem.GetComponentInChildren<MapLoader>().Level.Name != null) { Debug.Log("NameWorks"); }

        }
    }











    // Gets All Levels in Predefined Paths
    private List<LevelClass> GetAllLevels() {
        List<LevelClass> levels = new List<LevelClass>();
        foreach(String subpath in LevelPaths)
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
                l.Add(new LevelClass(null, info.FullName, null, new DirectoryInfo(path +"/"+ info.Name)));
                l[l.Count - 1 ].Name = info.Name.Substring(0,info.Name.IndexOf("."));
            }
        }
        return l;
    }

}