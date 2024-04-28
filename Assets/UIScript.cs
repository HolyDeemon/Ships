using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Image Vertiacal;
    public Image Horizontal;
    public Image Reload;
    public Text Score;
    public Text VertiacalTXT;
    public Text HorizontalTXT;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vertiacal.rectTransform.rotation = Quaternion.Euler(Vector3.forward * -CannonScript.Vertical);
        Horizontal.rectTransform.rotation = Quaternion.Euler(Vector3.forward * (-CannonScript.Horizontal + 90));
        Score.text = "SCORE: " + GameScript.Score;
        Reload.fillAmount = (CannonScript.ReloadTime - CannonScript.reload) / CannonScript.ReloadTime;

        VertiacalTXT.text = ((int)CannonScript.Vertical).ToString();
        HorizontalTXT.text = ((int)(-CannonScript.Horizontal)).ToString();
    }
}
