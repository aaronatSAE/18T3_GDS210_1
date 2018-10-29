using UnityEngine;
using UnityEngine.UI;

public class TTS_Button : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void OnMouseOver()
    {
        if(this.enabled == true)
        {
            GameLoader.GameInstance.Speak(transform.GetChild(0).GetComponent<Text>().text);
        }
    }
}