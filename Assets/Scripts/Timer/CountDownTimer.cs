using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    //Change time based on level
    [SerializeField] float startTime = 5f;
    
    float timer1 = 0f;

    [SerializeField] Slider slider1;
    [SerializeField] TextMeshProUGUI timerText1;

    private float quarterHealth = .25f;
    private float halfHealth = .5f;
    private Color halfColor = new Color(1.0f, 0.64f, 0.0f);

    public GameMaster gm;

    void Start()
    {        
            StartCoroutine(Timer1());   
    }

    private IEnumerator Timer1()
    {

        timer1 = startTime;
        do
        {
            timer1 -= Time.deltaTime;

            slider1.value = timer1 / startTime;

            //Change slider color based on time left
            if(slider1.value <= halfHealth)
            {
                slider1.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = halfColor;
            }
            if(slider1.value <= quarterHealth)
            {
                slider1.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red;
            }

            if(timer1 < 0.0f)
            {
                StartCoroutine(gm.RestartLevel());
            }

            FormatText1();

            yield return null;
        }
        while (timer1 > 0);
    }

    private void FormatText1()
    {
        int minutes = (int)(timer1 / 60) % 60;
        float seconds = (timer1 % 60);
        string minsLeft = minutes + "m ";

        if(seconds < 0)
        {
            seconds = 0;
        }

        string secondsString = seconds.ToString("F2") + "s ";

        if(minutes < 1)
        
        {
            minsLeft = "  ";
        }
        
        timerText1.text = minsLeft + secondsString;
    }



}
