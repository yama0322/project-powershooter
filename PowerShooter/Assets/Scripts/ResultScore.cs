using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    [SerializeField] GameObject resultScoreText = null;

    // Start is called before the first frame update
    void Start()
    {
        Text ScoreText = resultScoreText.GetComponent<Text>();
        ScoreText.text = PlayerPrefs.GetInt("NowScore") + "点";
    }
}
