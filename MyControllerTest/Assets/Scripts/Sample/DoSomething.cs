// Unityでシリアル通信、Arduinoと連携する雛形

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class DoSomething : MonoBehaviour
{
    // 制御対象のオブジェクト用に宣言しておいて、Start関数内で名前で検索
    GameObject targetObject;
    Player targetScript; // UnityプロジェクトにPlayerオブジェクトがいる前提

    GameObject targetUnitychan;
    ThirdPersonUserControl targetUnitychanScript;

    // シリアル通信のクラス、クラス名は正しく書くこと
    public SerialHandler serialHandler;


  void Start()
    {
        // 制御対象のオブジェクトを取得
        targetObject = GameObject.Find("Player");
        // 制御対象に関連付けられたスクリプトを取得、一般的にはこのスクリプトのメンバにアクセスして処理をすることが多いと思うので。
        // 大文字、小文字を区別するので、player.csを作ったのなら「p」layer。
        targetScript = targetObject.GetComponent<Player>();

        // 信号受信時に呼ばれる関数としてOnDataReceived関数を登録
        serialHandler.OnDataReceived += OnDataReceived;

        targetUnitychan = GameObject.Find("unitychan");
        targetUnitychanScript = targetUnitychan.GetComponent<ThirdPersonUserControl>();
    }

    void Update()
    {
        //文字列を送信するなら例えばココ
        //serialHandler.Write("hogehoge");

        if (targetScript.jklPress[0])
        {
            targetScript.jklPress[0] = false;
            if (targetScript.jklToggle[0])
            {
                // LED ON
                serialHandler.Write("a");
            }
            else
            {
                // LED OFF
                serialHandler.Write("b");
            }
        }

        if (targetScript.jklPress[1])
        {
            targetScript.jklPress[1] = false;
            if (targetScript.jklToggle[1])
            {
                // LED ON
                serialHandler.Write("c");
            }
            else
            {
                // LED OFF
                serialHandler.Write("d");
            }
        }

        if (targetScript.jklPress[2])
        {
            targetScript.jklPress[2] = false;
            if (targetScript.jklToggle[2])
            {
                // LED ON
                serialHandler.Write("e");
            }
            else
            {
                // LED OFF
                serialHandler.Write("f");
            }
        }
    }

    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        
        // ここでデコード処理等を記述
        targetScript.debugText.text = message;

        if (message == null)
            return;

        // 受け取ったデータを数値に変換 
        if (message[0] == 'S' && message[message.Length - 1] == 'E')
        {
            #region 必要に応じて変更すべき箇所

            targetScript.count++;
            if (targetScript.count > 9999)
            {
                targetScript.count = 0;
            }
            targetScript.debugText.text = targetScript.count.ToString() + ' ' + message;

            string receivedData;
            int t;

            // ボリューム
            // 必要な文字部分のバイト数（範囲）は常に把握する
            receivedData = message.Substring(1, 4); // x文字目からy文字数を変換
            // 必要な文字部分を抽出したら、データ形式に合わせてデコード、例えば以下のように。
            int.TryParse(receivedData, out t);
            targetScript.vol = t;

            // sw1
            receivedData = message.Substring(5, 1);
            int.TryParse(receivedData, out t);
            targetScript.sw1 = t;

            // sw2
            receivedData = message.Substring(6, 1);
            int.TryParse(receivedData, out t);
            targetScript.sw2 = t;

            // sw3
            receivedData = message.Substring(7, 1);
            int.TryParse(receivedData, out t);
            targetScript.sw3 = t;

            if (targetScript.sw1 == 0)
            {
                targetScript.myKeyEvent[0] = true;
            }

            if (targetScript.sw2 == 0)
            {
                targetScript.myKeyEvent[1] = true;
            }

            if (targetScript.vol > 800)
            {
                targetUnitychanScript.m_H = 1.0f;
            }
            else if (targetScript.vol < 200)
            {
                targetUnitychanScript.m_H = -1.0f;
            }
            else
            {
                // 0.001fは充分小さい数、0.0078125fは1/2^nで表せる数
                if (targetUnitychanScript.m_H > 0.001f)
                {
                    targetUnitychanScript.m_H -= 0.0078125f;
                }
                else if (targetUnitychanScript.m_H < -0.001f)
                {
                    targetUnitychanScript.m_H += 0.0078125f;
                }
            }

            if (targetScript.sw3 == 0)
            {
                if(!targetUnitychanScript.m_Jump)
                {
                    targetUnitychanScript.m_Jump = true;
                }
            }

            #endregion
        }
    }
}
