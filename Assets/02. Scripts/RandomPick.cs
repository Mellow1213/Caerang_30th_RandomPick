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

    public AudioClip spinStart;
    public GameObject spin;
    public AudioClip spinEnd;
    public AudioClip goods;

    AudioSource _audioSource;


    int cnt = 0;
    string number = "";

    float timer = 0f;
    public float[] eventTime;
    bool doTimer = false;


    [SerializeField] bool[] isPickedNumbers;
    [SerializeField] bool[] isPickedGoods;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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

            for (int i = 0; i < data.Count; i++)
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
            return "¸ðµç ±ÂÁî »ÌÈû";
        Debug.Log("±ÂÁî : " + temp);
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
            return "¸ðµç ÇÐ¹ø »ÌÈû";
        Debug.Log("±ÂÁî : " + temp);
        return data[temp]["numbers"].ToString();
    }

    float timeSet = 0.5f;
    // Update is called once per frame
    void Update()
    {
        Debug.Log( "doTimer = "+doTimer);
        Debug.Log("cnt = "+cnt);
        Debug.Log("timer = " +timer);

        if (Input.GetKeyDown(KeyCode.Space) && !doTimer)
        {
            doTimer = true;
        }
        if (doTimer)
        {
            timer += Time.deltaTime;
        }
        if (timer >= timeSet)
        {
            timer = 0f;
            PickRandomNumber();
        }
        if (cnt == 6)
            timeSet = 1f;
        else if (cnt == 7)
            timeSet = 2f;
        else
            timeSet = 0.5f;
    }

    void PickRandomNumber()
    {
        if (cnt == 0)
        {
            spin.SetActive(true);
            _audioSource.PlayOneShot(spinStart);
            number = RandomNumbers();
            Debug.Log("»ÌÀº ÇÐ¹ø" + number);
        }

        if (cnt < 8)
        {
            RandomNumberTxt[cnt].GetComponent<DoScramble>().setDoScramble(false);
            RandomNumberTxt[cnt].GetComponent<DoScramble>().setString(number[cnt]);
            cnt++;
        }
        else if (cnt == 8)
        {
            spin.SetActive(false);
            _audioSource.PlayOneShot(spinEnd);
            doTimer = false;
            cnt++;
        }
        else if (cnt == 9)
        {
            _audioSource.PlayOneShot(goods);
            //GoodTxt.DOText("" + RandomGoods(), 2f);
            GoodTxt.text = ""+RandomGoods();
            doTimer = false;
            cnt++;
        }
        else if(cnt == 10)
        {
            doTimer = false;
            for (int i = 0; i < RandomNumberTxt.Length; i++)
                RandomNumberTxt[i].GetComponent<DoScramble>().setDoScramble(true);

            number = ""; GoodTxt.text = "???";
            cnt++;
            spin.SetActive(true);

        }
        else
        {
            cnt = 0;
        }
    }
}
