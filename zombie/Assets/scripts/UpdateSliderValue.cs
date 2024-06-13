
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSliderValue : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeText(TMP_Text text)
    {
        Slider slider = GetComponent<Slider>();
        if (slider.value < 1)
        {
            text.text = slider.value.ToString("F3");
        }
        else
        {
            text.text = ((int)slider.value).ToString();
        }
    }
}
