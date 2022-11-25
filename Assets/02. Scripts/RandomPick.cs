using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.IO;

public class RandomPick : MonoBehaviour
{
    public TextMeshProUGUI[] RandomNumberTxt;
    public TextMeshProUGUI GoodTxt;
    public string[] StudentNumber;

    int cnt = 0;
    string number = "";

    float timer = 0f;
    public float[] eventTime;
    bool doTimer = false;
    private void Awake()
    {
    }

    string RandomGoods()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Goods");
        /*
        for (var i = 0; i < data.Count; i++)
        {
            print("goods " + i + " : " + data[i]["goods"]);
        }
        Debug.Log(data[Random.Range(0, data.Count)]["goods"]);
        */
        return data[Random.Range(0, data.Count)]["goods"].ToString();
    }

    string RandomNumbers()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Numbers");
        /*
        for (var i = 0; i < data.Count; i++)
        {
            print("numbers " + i + " : " + data[i]["numbers"]);
        }
        Debug.Log(data[Random.Range(0, data.Count)]["numbers"]);
        */
        return data[Random.Range(0, data.Count)]["numbers"].ToString();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(RandomNumbers());
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickRandomNumber();
        }
        if (doTimer)
        {
            timer += Time.deltaTime;
        }
    }

    void PickRandomNumber()
    {
        Debug.Log("cnt = " + cnt);
        if (cnt == 0)
        {
            number = RandomNumbers();
            Debug.Log("ªÃ¿∫ «–π¯" + number);
        }

        if (cnt < 8)
        {
            RandomNumberTxt[cnt].GetComponent<DoScramble>().setDoScramble(false);
            RandomNumberTxt[cnt].GetComponent<DoScramble>().setString(number[cnt]);
            cnt++;
        }
        else if(cnt == 8)
        {
            GoodTxt.text = "¥Á√∑ º±π∞ : " + RandomGoods();
            cnt++;
        }
        else
        {
            for (int i = 0; i < RandomNumberTxt.Length; i++)
                RandomNumberTxt[i].GetComponent<DoScramble>().setDoScramble(true);

            cnt = 0;
            number = ""; GoodTxt.text = "";
        }
    }
}
