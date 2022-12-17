using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移処理
/// </summary>
public class SceneChange : MonoBehaviour
{
    GameObject targetObject;
    BallManager targetScript;

    [SerializeField] GameObject fadeinImg;

    [SerializeField] AudioClip[] sound;
    AudioSource audioSource;

    bool isSoundOnce;

    [Header("移動するシーン名")]
    public string[] sceneName;

    [Header("シーン遷移のロード時間")]
    public float[] sceneRoadTime;

    void Start()
    {
        targetObject = GameObject.Find("GameManager");
        targetScript = targetObject.GetComponent<BallManager>();

        audioSource = GetComponent<AudioSource>();

        isSoundOnce = false;

        fadeinImg.SetActive(false);
    }

    /// <summary>
    /// シーン遷移処理(ロード時間, SE, 遷移したいシーン名)
    /// </summary>
    /// <param name="t"></param>
    /// <param name="s"></param>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator goScene(float t, AudioClip s, string sceneName)
    {
        // 一回だけ音を鳴らす
        if (!isSoundOnce)
        {
            audioSource.PlayOneShot(s);
            isSoundOnce = true;
        }
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(sceneName);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || targetScript.sw1 == 1)
        {
            StartCoroutine(goScene(sceneRoadTime[0], sound[0], sceneName[0]));
        }

        if (Input.GetKeyDown(KeyCode.L) || targetScript.sw2 == 1)
        {
            fadeinImg.SetActive(true);
            StartCoroutine(goScene(sceneRoadTime[1], sound[1], sceneName[1])); 
        }
    }
}
