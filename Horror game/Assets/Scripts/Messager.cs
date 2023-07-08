using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Messager : MonoBehaviour
{
    private float seconds;
    private bool isRunning;

    public GameObject messagePanel;

    public void ShowMessage(string message, int seconds)
    {
        messagePanel.SetActive(true);
        messagePanel.GetComponent<Text>().text = message;

        this.seconds = seconds;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            if (seconds > 0)
            {
                seconds -= Time.deltaTime;
            }
            else
            {
                seconds = 0;
                isRunning = false;
                messagePanel.SetActive(false);
            }
        }
    }
}
