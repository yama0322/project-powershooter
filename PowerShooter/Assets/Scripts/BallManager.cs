using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// arduinoから受け取った文字列の取得を行う
/// </summary>
public class BallManager : MonoBehaviour
{
    [Header("デバイスから取得した値")]
    public int sw1;
    public int sw2;
    public int x;
    public int y;
    public int z;

    public bool[] myKeyEvent;

    // Start is called before the first frame update
    void Start()
    {
        myKeyEvent = new bool[2];
        myKeyEvent[0] = false;
        myKeyEvent[1] = false;
    }

    void Update()
    {
        if(myKeyEvent[0])
        {
            myKeyEvent[0] = false;
        }
    }
}
