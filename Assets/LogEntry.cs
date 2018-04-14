using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogEntry : MonoBehaviour {
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        text.supportRichText = true;
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }
}
