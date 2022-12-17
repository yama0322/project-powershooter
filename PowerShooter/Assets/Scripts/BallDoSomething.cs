using UnityEngine;

public class BallDoSomething : MonoBehaviour
{
    GameObject targetObject;
    BallManager targetScript;

    // シリアル通信のクラス
    public SerialHandler serialHandler;

    void Start()
    {
        targetObject = GameObject.Find("GameManager");
        targetScript = targetObject.GetComponent<BallManager>();

        // 信号受信時に呼ばれる関数としてOnDataReceived関数を登録
        serialHandler.OnDataReceived += OnDataReceived;
    }

    //arduinoからの値の読み取りを行う
    void OnDataReceived(string message)
    {
        if (message == null)
            return;

        // 取得した文字列の始めが'S'で終わりが'E'の場合読み取りを行う
        if (message[0] == 'S' && message[message.Length - 1] == 'E')
        {
            string receivedData;

            // x
            receivedData = message.Substring(1, 3); // x文字目からy文字数を変換
            int.TryParse(receivedData, out targetScript.x);

            // y
            receivedData = message.Substring(4, 3);
            int.TryParse(receivedData, out targetScript.y);

            // z
            receivedData = message.Substring(7, 3);
            int.TryParse(receivedData, out targetScript.z);

            // sw1
            receivedData = message.Substring(10, 1);
            int.TryParse(receivedData, out targetScript.sw1);

            // sw2
            receivedData = message.Substring(11, 1);
            int.TryParse(receivedData, out targetScript.sw2);

            if (targetScript.sw1 == 0)
            {
                targetScript.myKeyEvent[0] = true;
            }

            if (targetScript.sw2 == 0)
            {
                targetScript.myKeyEvent[1] = true;
            }
        }
    }
}
