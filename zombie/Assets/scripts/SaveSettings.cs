
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{
    public Slider _sensivity;
    public Slider _smoothTime;
    // Start is called before the first frame update
    private void Start()
    {
        _sensivity.value = PlayerPrefs.GetFloat("sensivity", 200);
        _smoothTime.value = PlayerPrefs.GetFloat("smoothTime", 0.020f);
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("sensivity",_sensivity.value);
        PlayerPrefs.SetFloat("smoothTime",_smoothTime.value);
    }
    public void Delete()
    {
        PlayerPrefs.DeleteAll();
        _sensivity.value = PlayerPrefs.GetFloat("sensivity", 200);
        _smoothTime.value = PlayerPrefs.GetFloat("smoothTime", 0.020f);
    }
}
