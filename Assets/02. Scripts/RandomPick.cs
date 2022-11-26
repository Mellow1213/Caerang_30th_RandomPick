using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.IO;
using System.Linq;

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


    [SerializeField] bool[] isPickedNumbers;
    [SerializeField] bool[] isPickedGoods;

    private void Awake()
    {
        List<Dictionary<string, object>> goods = CSVReader.Read("Goods");
        isPickedGoods = Enumerable.Repeat<bool>(false, goods.Count).ToArray<bool>();
        Debug.Log("isPicked.Length = " + isPickedGoods.Length);


        List<Dictionary<string, object>> nums = CSVReader.Read("Numbers");
        isPickedNumbers = Enumerable.Repeat<bool>(false, nums.Count).ToArray<bool>();
        Debug.Log("isPicked.Length = " + isPickedNumbers.Length);
    }


    string RandomGoods()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Goods");
        int temp;
        /*
        for (var i = 0; i < data.Count; i++)
        {
            print("goods " + i + " : " + data[i]["goods"]);
        }
        Debug.Log(data[Random.Range(0, data.Count)]["goods"]);
        */


        bool flag;
        while (true)
        {
            flag = true;
            Random.InitState(System.DateTime.Now.Millisecond);
            temp = Random.Range(0, data.Count);
            if (!isPickedGoods[temp])
            {
                flag = false;
                break;
            }
            
            for(int i =0; i<data.Count; i++)
            {
                if (!isPickedGoods[i])
                {
                    flag = false;
                }
            }

            if (flag) break;
        }
        isPickedGoods[temp] = true;
        if (flag)
            return "∏µÁ ±¬¡Ó ªÃ»˚";
        Debug.Log("±¬¡Ó : " + temp);
        return data[temp]["goods"].ToString();
    }

    string RandomNumbers()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Numbers");
        int temp;
        /*
        for (var i = 0; i < data.Count; i++)
        {
            print("numbers " + i + " : " + data[i]["numbers"]);
        }
        Debug.Log(data[Random.Range(0, data.Count)]["numbers"]);
        */
        bool flag;
        while (true)
        {
            flag = true;
            Random.InitState(System.DateTime.Now.Millisecond);
            temp = Random.Range(0, data.Count);
            if (!isPickedNumbers[temp])
            {
                flag = false;
                break;
            }
            for (int i = 0; i < data.Count; i++)
            {
                if (!isPickedNumbers[i])
                {
                    flag = false;
                }
            }
            if (flag) break;
        }
        isPickedNumbers[temp] = true;
        if (flag)
            return "∏µÁ «–π¯ ªÃ»˚";
        Debug.Log("±¬¡Ó : " + temp);
        return data[temp]["numbers"].ToString();
    }
    // Update is called once per frame
    void Update()
    {
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
            GoodTxt.DOText("[¥Á√∑ ªÛ«∞] : " + RandomGoods(), 2f);
            cnt++;
        }
        else
        {
            for (int i = 0; i < RandomNumberTxt.Length; i++)
                RandomNumberTxt[i].GetComponent<DoScramble>().setDoScramble(true);

            cnt = 0;
            number = ""; GoodTxt.text = "[¥Á√∑ ªÛ«∞] ";
        }
    }
}
