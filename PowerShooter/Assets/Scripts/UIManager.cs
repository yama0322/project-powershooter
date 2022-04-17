using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 時間、スコア等のUI処理をここで行う
/// </summary>
public class UIManager : MonoBehaviour
{
    GameObject ballCreateObject;
    BallCreate ballCreateScript;

    [Header("制限時間")]
    public float gameTime;

    [SerializeField] GameObject scoreText = null;
    [SerializeField] GameObject timerText = null;
    [SerializeField] GameObject finishText;
    [SerializeField] GameObject finishUIImg;
    public GameObject goalImg;
    public GameObject sumText = null;
    public int getScoreNum;

    [SerializeField] AudioClip sound1;
    AudioSource audioSource;

    bool isgame;

    // Start is called before the first frame update
    void Start()
    {
        ballCreateObject = GameObject.Find("GameManager");
        ballCreateScript = ballCreateObject.GetComponent<BallCreate>();

        audioSource = GetComponent<AudioSource>();

        sumText.SetActive(false);
        finishText.SetActive(false);
        finishUIImg.SetActive(false);
        goalImg.SetActive(false);

        isgame = false;
    }

    /// <summary>
    /// ゲーム終了時の処理
    /// </summary>
    /// <returns></returns>
    IEnumerator gameStop()
    {
        PlayerPrefs.SetInt("NowScore", ballCreateScript.count);

        // 一回だけ音を鳴らす
        if (!isgame)
        {
            audioSource.PlayOneShot(sound1);
            isgame = true;
        }

        // ゲーム終了時のアニメーションが入る
        finishText.SetActive(true);
        finishUIImg.SetActive(true);

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Soccer_Result");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTime <= 0f)
        {
            StartCoroutine(gameStop());
        }
        else if (gameTime >= 0f)
        {
            gameTime -= Time.deltaTime;
        }

        Text ScoreText = scoreText.GetComponent<Text>();
        ScoreText.text = ballCreateScript.count + "点";
        Text TimerText = timerText.GetComponent<Text>();
        TimerText.text = gameTime.ToString("f1") + "秒";
        Text scoresumText = sumText.GetComponent<Text>();
        scoresumText.text = "+" + getScoreNum + "点";
    }
}