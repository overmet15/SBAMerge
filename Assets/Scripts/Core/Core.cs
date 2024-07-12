using GameJolt.API;
using UnityEngine;
public class Core : MonoBehaviour
{
    #region fields and Proporties
    public static Core instance;
    [Header("Parts/Components")] //ToDo: Move data loading stuff here (Maybe)
    [SerializeField] private AchivementNotifier achivementNotifier;
    [SerializeField] private AutologinNotifier autologinNotifier;
    [SerializeField] private DataManagement dataManagement;
    [SerializeField] private LeaderboardWindow leaderboardWindow;
    [SerializeField] private TrophyPanel trophyPanel;
    [SerializeField] private SignInPanel signInPanel;
    [SerializeField] private Animator loadingAnimator;
    public bool signedIn;

    #endregion
    void Awake()
    {
        if (instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(gameObject);
#if !UNITY_ANDROID
            DiscordManager.Init();
#endif
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
#if !UNITY_ANDROID
        DiscordManager.Update();
#endif
        signedIn = GameJoltAPI.Instance.HasSignedInUser;
    }
    private void Start()
    {
        GameJoltAPI.Instance.AutoLoginEvent.AddListener(res => { autologinNotifier.OnAutologin(res); ToggleLoadingIndicator(false); });
    }
    void NotifyAchivement(int id) { achivementNotifier.QueueNotify(id); }
    public void SignIn() { if (!signedIn) signInPanel.ToggleWindow(); }
    public void OnSignIn() { autologinNotifier.OnAutologin(AutoLoginResult.Success); GamejoltManager.instance.ResetAchivementCounters(); }
    public void SignOut() { if (signedIn) GameJoltAPI.Instance.CurrentUser.SignOut(); }
    public void UnlockAchivement(int id)
    {
        ToggleLoadingIndicator(true);
        if (signedIn)
        {
            Trophies.TryUnlock(id, callback => {
                if (callback == TryUnlockResult.Failure)
                {
                    ToggleLoadingIndicator(false);
                    Debug.Log("Unable To Unlock (failure)");
                }
                else if (callback == TryUnlockResult.AlreadyUnlocked)
                {
                    ToggleLoadingIndicator(false); //NotifyAchivement(id);
                }
                else if (callback == TryUnlockResult.Unlocked)
                {
                    ToggleLoadingIndicator(false); NotifyAchivement(id);
                }
            });
        }
        else
        {
            Debug.Log("Unable To Unlock (Not Signed In)");
            ToggleLoadingIndicator(false);
        }
    }
    public void OpenDataManager() { dataManagement.ToggleWindow(); }
    public void ShowLeaderboard() { leaderboardWindow.ToggleWindow(); leaderboardWindow.SetUp(886999); }
    public void ShowTrophies() {  trophyPanel.GetItems(); }
    public void ToggleLoadingIndicator(bool needed)
    {
        loadingAnimator.gameObject.SetActive(SaveValues.instance.optionsData.loadingIndicatorNeeded); 
        loadingAnimator.SetBool("Bool",needed); 
    }
}
