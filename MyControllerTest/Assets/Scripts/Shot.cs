using System.Collections;
using UnityEngine;

/// <summary>
/// シュートの処理
/// </summary>
/// 
public class Shot : MonoBehaviour
{
    GameObject targetObject;
    BallManager targetScript;

    GameObject ballCreateObject;
    BallCreate ballCreateScript;

    GameObject UIObject;
    UIManager UIScript;

    [SerializeField] AudioClip sound1;
    AudioSource audioSource;

    Rigidbody rb;

    private Vector3 force;

    bool isFirst;

    // Start is called before the first frame update
    void Start()
    {
        targetObject = GameObject.Find("GameManager");
        targetScript = targetObject.GetComponent<BallManager>();

        ballCreateObject = GameObject.Find("GameManager");
        ballCreateScript = ballCreateObject.GetComponent<BallCreate>();

        UIObject = GameObject.Find("GameManager");
        UIScript = UIObject.GetComponent<UIManager>();

        rb = this.GetComponent<Rigidbody>();
        isFirst = true;

        audioSource = GetComponent<AudioSource>();
    }
    
    /// <summary>
    /// ボールを蹴ってから消えるまでの処理(消えるまでの時間)
    /// </summary>
    /// <returns></returns>
    IEnumerator ballBeTimer(int x)
    {
        yield return new WaitForSeconds(x);
        ballCreateScript.ballNow = false;
        Destroy(this.gameObject);
    }

    void Update()
    {
        if (UIScript.gameTime >= 0) // 制限時間外はシュートできない
        {
            if (Input.GetKeyDown(KeyCode.W) || targetScript.y > 40)
            {
                // 3軸加速度センサーで取得したx,y,zの値をパワーに変換
                float forwardPower = 5.5f * targetScript.y / 30;
                float upPower = 4f * targetScript.y / 30;
                float sidePower = 5f * targetScript.x / 70;
                force = new Vector3(forwardPower, upPower, sidePower);
                if (isFirst)
                {
                    isFirst = false;
                    rb.AddForce(force, ForceMode.Impulse);
                    audioSource.PlayOneShot(sound1);
                    StartCoroutine(ballBeTimer(2));
                }
            }
        }
    }
}
