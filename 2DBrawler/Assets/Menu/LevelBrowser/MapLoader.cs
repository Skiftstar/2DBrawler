using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapLoader : MonoBehaviour
{
    public Image PreviewImage;
    public Text PreviewName;
    public Text PreviewDescription;
    public LevelClass Level;
    private void Start()
    {
        PreviewImage = Level.image;
        PreviewName.text = Level.Name;
        PreviewDescription = Level.Description;

    }
    public void LoadLevel() {

        string scenePath = Level.LevelPath.Name;
        Debug.Log(scenePath);
        AssetBundle a;
        Scene s;
        AsyncOperation async = SceneManager.LoadSceneAsync(System.IO.Path.GetFileNameWithoutExtension(scenePath));
    }
}
