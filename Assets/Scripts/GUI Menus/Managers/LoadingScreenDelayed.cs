using UnityEngine;
using System.Collections;

public class LoadingScreenDelayed : MonoBehaviour
{
    private static volatile LoadingScreenDelayed instance;
    private static object syncLock = new Object();

    //public GameObject LoadingPicture;

    public static LoadingScreenDelayed Instance
    {
        get { 
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = ((GameObject)GameObject.Instantiate(Resources.Load("LoadSceneManager"))).GetComponent<LoadingScreenDelayed>();
                    }
                }

            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance == this)
            return;

        if (instance == null)
        {
            lock (syncLock)
            {
                if (instance == null)
                {
                    instance = this;
                    DontDestroyOnLoad(gameObject);
                }
                else
                    Destroy(gameObject);

            }
        }
        else
            Destroy(gameObject);

    }

    public float delayTimer = 5.0f;
    AsyncOperation loadingOperation;


    // Use this for initialization
    //IEnumerator Start()
    //{
    //    yield return new WaitForSeconds(delayTimer);

        
    //}

    // Update is called once per frame
    void Update()
    {
        if (loadingOperation != null)
        {
            // TODO:  Update Progress Bar

            if (loadingOperation.isDone)
            {
                count++;
                loadingOperation = null;
                if (lvl != "" || lvlIndex != -1)
                    LLevel();
                //loading2 = Application.LoadLevelAsync("SplashScreen");
                //loadingOperation.allowSceneActivation = true;
            }
        }
    }

    AsyncOperation loading2;

    private string lvl = "";
    public int count = 0;
    public void LoadingLevels(string level)
    {
        lvl = level;
        //count++;
        loadingOperation = Application.LoadLevelAsync("SplashScreen");
        // TODO: Instantiate loading canvas
        //Instantiate(LoadingPicture);

        //Invoke("LLevel", 5.0f);

        //loadingOperation.allowSceneActivation = false;    
    }

    private int lvlIndex = -1;
    public void LoadingLevels(int levelIndex)
    {
        lvlIndex = levelIndex;
        loadingOperation = Application.LoadLevelAsync("SplashScreen");  
    }

    private void LLevel()
    {
        if (lvl != "")
        {
            loadingOperation = Application.LoadLevelAsync(lvl);
            lvl = "";
            lvlIndex = -1;
            Pause();
        }
        else if (lvlIndex != -1)
        {
            loadingOperation = Application.LoadLevelAsync(lvlIndex);
            lvl = "";
            lvlIndex = -1;
            Pause();
        }
        lvl = "";
        lvlIndex = -1;
    }

    private IEnumerator Pause()
    {
        yield return loadingOperation;
    }

    public void OnDestroy()
   {
       if (instance == this)
           instance = null;
   }
}
