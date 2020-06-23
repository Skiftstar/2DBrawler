using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelClass
{
    public Image image;
    public string Name;
    public Text Description;
    public DirectoryInfo LevelPath;
    public LevelClass(Image i,string name,Text Description,DirectoryInfo LevelPath) {
        this.image = i;
        this.Name = name;
        this.Description = Description;
        this.LevelPath = LevelPath;
    }
}
