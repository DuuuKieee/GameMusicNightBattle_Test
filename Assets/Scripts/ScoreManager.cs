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

            Instantiate(fxPrefabs[0], new Vector3(0, 3, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(fxPrefabs[1], new Vector3(0, 3, 0), Quaternion.identity);
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
        Instantiate(fxPrefabs[2], new Vector3(0, 3, 0), Quaternion.identity);
        // Instance.missSFX.Play();    
    }
}
