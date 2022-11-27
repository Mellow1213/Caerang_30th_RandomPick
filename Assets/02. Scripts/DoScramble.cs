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

    float timer = 0f;
    void Scramble()
    {
        if (doScramble)
        {
            timer += Time.deltaTime;
            if (timer > 0.01f)
            {
                timer = 0f;
                _text.text = Random.Range(0, 10).ToString();
            }
        }
        else
        {
        }
    }

}
