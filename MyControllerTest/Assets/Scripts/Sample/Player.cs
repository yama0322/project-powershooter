using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 移動処理
/// </summary>
public class Player : MonoBehaviour
{
    private float moveValue;

    [Header("画面テキスト")]
    public Text debugText;

    public int sw1;
    public int sw2;
    public int sw3;
    public int vol;

    public uint count;
    public bool[] myKeyEvent;

    public bool[] jklPress;
    public bool[] jklToggle;

    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        moveValue = 0.01f;
        //debugText.text = "dubug";
        sw1 = 1;
        sw2 = 1;
        sw3 = 1;
        vol = 400;

        myKeyEvent = new bool[2];
        myKeyEvent[0] = false;
        myKeyEvent[1] = false;

        jklPress = new bool[3];
        jklToggle = new bool[3];
        jklPress[0] = false;
        jklPress[1] = false;
        jklPress[2] = false;
        jklToggle[0] = false;
        jklToggle[1] = false;
        jklToggle[2] = false;

        startTime = -1;

        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || myKeyEvent[0])
        {
            myKeyEvent[0] = false;
            this.transform.position += new Vector3(moveValue, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || myKeyEvent[1])
        {
            myKeyEvent[1] = false;
            this.transform.position -= new Vector3(moveValue, 0f, 0f);
        }


        #region arduinoへの操作

        if (Input.GetKeyDown(KeyCode.J))
        {
            jklPress[0] = true;
            jklToggle[0] = !jklToggle[0];
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            jklPress[1] = true;
            jklToggle[1] = true;
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            jklPress[1] = true;
            jklToggle[1] = false;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            jklPress[2] = true;
            jklToggle[2] = true;
            startTime = 0;
        }

        if (startTime >= 0f)
        {
            startTime += Time.deltaTime;
            if (startTime > 3f)
            {
                jklPress[2] = true;
                jklToggle[2] = false;
                startTime = -1;
            }
        }

        #endregion
    }
}
