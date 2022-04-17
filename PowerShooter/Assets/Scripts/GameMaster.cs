using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シングルトン処理
/// </summary>
public class GameMaster : MonoBehaviour
{
    static public GameMaster instance;

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
