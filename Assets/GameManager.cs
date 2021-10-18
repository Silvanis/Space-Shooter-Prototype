using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float worldMovementRate = 1.0f;
    public bool isWorldMoving = true;

    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            
            return GameManager.instance;
        }
    }
    private void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
            DontDestroyOnLoad(GameManager.instance);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnApplicationQuit()
    {
        GameManager.instance = null;
    }
}
