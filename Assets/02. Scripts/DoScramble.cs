using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoScramble : MonoBehaviour
{
    TextMeshProUGUI _text;

    bool doScramble = true;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        Scramble();
    }
    
    public void setDoScramble(bool flag)
    {
        doScramble = flag;
    }

    public void setString(char num)
    {
        _text.text = num.ToString();
    }

    
    void Scramble()
    {
        if (doScramble)
        {
            _text.text = Random.Range(0, 10).ToString();
        }
    }

}
