using GameJolt.API;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    public void ToggleWindow() { GetComponent<Animator>().SetTrigger("Go"); }
    public void SaveData()
    {
        DataStore.Set("bombBuffCount", SaveValues.instance.gameData.bombBuffCount.ToString(), false);
        DataStore.Set("shakeBuffCount", SaveValues.instance.gameData.shakeBuffCount.ToString(), false);
        DataStore.Set("slowMoBuffCount", SaveValues.instance.gameData.slowMoBuffCount.ToString(), false);
        DataStore.Set("coinCount", SaveValues.instance.gameData.coinCount.ToString(), false);
        ToggleWindow();
    }

    public void LoadData()
    {
        ToggleWindow();
        Core.instance.ToggleLoadingIndicator(true);
        Manager.Instance.canPlay = false;
        DataStore.Get("bombBuffCount", false, value => {
            SaveValues.instance.gameData.bombBuffCount = int.Parse(value);
            DataStore.Get("shakeBuffCount", false, value => {
                SaveValues.instance.gameData.shakeBuffCount = int.Parse(value);
                DataStore.Get("slowMoBuffCount", false, value => {
                    SaveValues.instance.gameData.slowMoBuffCount = int.Parse(value);
                    DataStore.Get("coinCount", false, value => {
                        SaveValues.instance.gameData.coinCount = int.Parse(value);
                        Core.instance.ToggleLoadingIndicator(false);
                        LoadingManager.instance.RestartAsync();
                    });
                });
            });
        });
    }
}
