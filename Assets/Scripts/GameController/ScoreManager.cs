using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public List<GameObject> fxPrefabs;
    [SerializeField] Slider hpSlider;
    void Start()
    {
        Instance = this;
        ObjectPool.Instance.InitializePool("PerfectPopup", fxPrefabs[0], 3);
        ObjectPool.Instance.InitializePool("GoodPopup", fxPrefabs[1], 3);
        ObjectPool.Instance.InitializePool("MissPopup", fxPrefabs[2], 3);
    }
    public void RestartHP()
    {
        hpSlider.value = hpSlider.maxValue / 2;
    }
    public void Hit(bool _isPerfect)
    {
        hpSlider.value += 1;
        if (_isPerfect)
        {
            GameObject popup = ObjectPool.Instance.GetObject("PerfectPopup");
            popup.transform.position = new Vector3(0, 3, 0);
        }
        else
        {
            GameObject popup = ObjectPool.Instance.GetObject("GoodPopup");
            popup.transform.position = new Vector3(0, 3, 0);
        }
        // Instance.hitSFX.Play();
    }
    public void Miss()
    {
        hpSlider.value -= 1;
        if (hpSlider.value <= 0) 
        {
            GameStateManager.Instance.ChaneStateGame(GameState.End);
        }
        GameObject popup = ObjectPool.Instance.GetObject("MissPopup");
        popup.transform.position = new Vector3(0, 3, 0);
        // Instance.missSFX.Play();    
    }
}
