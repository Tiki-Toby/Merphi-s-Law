using Assets.Scripts.Data;
using Assets.Scripts.GameLogic;
using Assets.Scripts.InteractableObjects;
using Assets.Scripts.Items;
using Assets.Scripts.Player;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private static GameHandler _instance;
    public static GameHandler Instance => _instance;
    [Header("Global Settings")]
    public float ItemValidDistance;
    public Color ColorOnInteractable;
    public Color ColorOnFixable;

    public Transform player { get; private set; }
    public SceneHandler sceneManager { get; private set; }
    public UIManager uiManager { get; private set; }
    public AudioSourceManager audioManager { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        InitManagers();
        uiManager = FindObjectOfType<UIManager>();
        audioManager = FindObjectOfType<AudioSourceManager>();
        StartFirstLevel(true);
        uiManager.Init(true);
    }
    public void EndAction()
    {
        SessionManager.Instance.EndAction();
        ObjectData breakingObject = sceneManager.BreakingObject();
        uiManager.EndGameAction(breakingObject);
    }
    public void PutItem(ItemData item)
    {
        SessionManager.Instance.PutItem(item);
        audioManager.PlayUseSound();
        uiManager.PutItem(item);
    }
    public void RemoveItem(ItemData item)
    {
        uiManager.RemoveItem(SessionManager.Instance.CurrentInvPosition);
        SessionManager.Instance.RemoveItem(item);
        uiManager.SetCurrentItem();
    }
    public void SetPause(bool isPause)
    {
        SessionManager.Instance.SetPause(isPause);
    }
    public void SwitchPause()
    {
        SessionManager.Instance.SwitchPause();  
    }
    public void StartFirstLevel(bool isPause)
    {
        SessionManager.Instance.FirstLevel();
        InitializeLevel(SessionManager.Instance.CurrentLevel, isPause);
    }
    public void RestartLevel(bool isPause)
    {
        InitializeLevel(SessionManager.Instance.CurrentLevel, isPause);
    }
    public void StartNextLevel(bool isPause)
    {
        SessionManager.Instance.NextLevel();
        InitializeLevel(SessionManager.Instance.CurrentLevel, isPause);
    }
    public void InitializeLevel(int lvl, bool isPause)
    {
        LevelPreset[] levelPresets = Resources.LoadAll<LevelPreset>(Paths.Levels);
        foreach (LevelPreset levelPreset in levelPresets)
            if (levelPreset.lvl == lvl)
            {
                SessionManager.Instance.InitSession(levelPreset);
                SessionManager.Instance.SetPause(isPause);
                if (!levelPreset.sceneName.Equals(SceneManager.GetActiveScene().name))
                    StartCoroutine(LoadScene(levelPreset, isPause));
                else
                {
                    sceneManager.Init(levelPreset);
                }
            }
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void InitManagers()
    {
        if (sceneManager != null) Destroy(sceneManager.gameObject);
        sceneManager = FindObjectOfType<SceneHandler>();
        if (player != null) Destroy(player.gameObject);
        player = FindObjectOfType<MovementHandler>().transform;
    }
    private IEnumerator LoadScene(LevelPreset levelPreset, bool isPause)
    {
        SceneManager.LoadScene(levelPreset.sceneName);
        yield return null;
        InitManagers();
        sceneManager.Init(levelPreset);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            EndAction();
        else if (Input.GetKeyDown(KeyCode.Q))
            SwitchPause();
        else if (Input.GetKeyDown(KeyCode.Space))
            InitializeLevel(0, false);
        else if (Input.mouseScrollDelta.y != 0)
        {
            SessionManager.Instance.AddNextItem((int)Input.mouseScrollDelta.y);
            uiManager.SetCurrentItem();
        }
        else
        {
            for (int i = 49; i < 58; i++)
                if (Input.GetKeyDown((KeyCode)i))
                {
                    Debug.Log("AAAAAAAAAa");
                    SessionManager.Instance.SetCurrentItem(i - 49);
                    uiManager.SetCurrentItem();
                }
        }
    }
}
