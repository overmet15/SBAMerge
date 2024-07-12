using UnityEngine;

public class GetVolumeFromSettings : MonoBehaviour
{
    private AudioSource ass;
    [SerializeField]
    private bool isMusic;
    void Awake()
    {
        ass = GetComponent<AudioSource>();
        ass.volume = 0;
    }

    // Update is called once per frame
    void Update() { GetVol(); }
    void GetVol()
    {
        ass.volume = SaveValues.instance.ReturnSoundValue(isMusic);
    }
}
