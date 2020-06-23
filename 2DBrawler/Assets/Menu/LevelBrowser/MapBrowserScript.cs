using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class MapBrowserScript: MonoBehaviour
{
    String[] LevelPaths = {"/Levels","/Scenes"};    //Paths where it scans for Levels, going from the MainGamePath
    public GameObject MapButton;                    //Item to Spawn, set by Unity from outside
    public Transform SpawnPoint;                    // the spawnpoint for Levels
    public RectTransform content;                   //to resize it depending ond the amount of Levels
    public RectTransform Scrollview;                //for width of Scrollbox
    private int ButtonWidth = 120;                  //With of Item
    private int ButtonHeight = 80;                  //Height of item
    private int ItemsInScrollbox = 0;               // a Counter for the Items in the Scrollbox
    void Start()
    {
        PopulateScrollBox(GetAllLevels());
    }
    private void PopulateScrollBox(List<LevelClass> LevelList)
    {
        //Looking for how many can be Displayed per Horizontal Line
        //And ensuring its never below 1
        int LineMax = max( ((int)((Scrollview.rect.width -20) / ButtonWidth)),1);
        //Resize Content Window for all Items
        int contentsize = (int) ( (ItemsInScrollbox + LevelList.Count) / LineMax );
        contentsize = contentsize * ButtonHeight;
        content.sizeDelta = new Vector2(content.rect.width, contentsize);
        for (int i = 0; i < LevelList.Count; i++)
        {
            //Calculating Line and Column for new Entry
            int column = ItemsInScrollbox % LineMax;
            int line = (int) ItemsInScrollbox / LineMax;
            //Calculating x and y Position of the Column and Line
            int ypos = line * ButtonHeight;
            int xpos = column *ButtonWidth;
            //Set Position of New Item and Apply Offset
            Vector3 pos = new Vector3(xpos+110,-ypos-10, SpawnPoint.position.z);
            String d = ItemsInScrollbox.ToString() +"  "+ xpos.ToString() + "  " + ypos.ToString();
            //instantiate item
            GameObject SpawnedItem = Instantiate(MapButton, pos, SpawnPoint.rotation);
            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            //Update Displayed Information with LevelClass
            SpawnedItem.GetComponentInChildren<MapLoader>().Level = LevelList[i];
            ItemsInScrollbox++;
        }
    }
    //just takes the bigger number, couldnt find the natural function to do the job
    private int max(int a, int b) {
        if (a > b) { return a; }
        else { return b; }
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