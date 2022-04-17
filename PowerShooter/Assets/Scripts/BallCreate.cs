using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サッカーボールの生成処理
/// </summary>
public class BallCreate : MonoBehaviour
{
    [SerializeField] GameObject bollPrefab;
    [SerializeField] GameObject forwardPos;

    public bool ballNow;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(bollPrefab, forwardPos.transform.position, forwardPos.transform.rotation);
        ballNow = true;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 蹴ったボールが消えたらすぐに新しいボールを生成
        // ボールは蹴った後2秒で消える
        if (!ballNow)
        {
            Instantiate(bollPrefab, forwardPos.transform.position, forwardPos.transform.rotation);
            ballNow = true;
            Debug.Log("生成");
        }
    }
}
