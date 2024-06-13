
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
