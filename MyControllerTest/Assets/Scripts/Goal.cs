using System.Collections;
using UnityEngine;

/// <summary>
/// サッカーボールがゴールした時の処理をここで行う
/// </summary>
public class Goal : MonoBehaviour
{
    GameObject UIObject;
    UIManager UIScript;

    GameObject bollCreateObject;
    BallCreate bollCreateScript;

    [SerializeField] AudioClip[] sound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        UIObject = GameObject.Find("GameManager");
        UIScript = UIObject.GetComponent<UIManager>();

        bollCreateObject = GameObject.Find("GameManager");
        bollCreateScript = bollCreateObject.GetComponent<BallCreate>();

        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ゴール時の加点処理(得点, SE)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    IEnumerator ScoreCountPlus(int x, AudioClip s)
    {
        UIScript.getScoreNum = x;
        UIScript.sumText.SetActive(true);
        UIScript.goalImg.SetActive(true);
        bollCreateScript.count += x;
        audioSource.PlayOneShot(s);

        yield return new WaitForSeconds(1);

        UIScript.sumText.SetActive(false);
        UIScript.goalImg.SetActive(false);
    }

    // ボールとゴールエリアの衝突処理
    // 衝突すると加点
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "boll")
        {
            switch(this.gameObject.tag)
            {
                case "100PointArea":
                    StartCoroutine(ScoreCountPlus(100, sound[0]));
                    audioSource.PlayOneShot(sound[3]); // 100点取った時は観客の歓声のSEを入れる
                    break;
                case "50PointArea":
                    StartCoroutine(ScoreCountPlus(50, sound[1]));
                    break;
                case "10PointArea":
                    StartCoroutine(ScoreCountPlus(10, sound[2]));
                    break;
                default:
                    break;
            }
        }
    }
}
