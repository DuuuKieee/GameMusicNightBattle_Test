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
    static int comboScore;
    void Start()
    {
        Instance = this;
        comboScore = 0;
    }
    public void Hit(bool _isPerfect)
    {
        hpSlider.value += 1;
        if (_isPerfect)
        {

            Instantiate(fxPrefabs[0], new Vector3(0, 4, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(fxPrefabs[1], new Vector3(0, 4, 0), Quaternion.identity);
        }
        Debug.Log(hpSlider.value);
        // Instance.hitSFX.Play();
    }
    public void Miss()
    {
        hpSlider.value -= 1;
        Instantiate(fxPrefabs[2], new Vector3(0, 4, 0), Quaternion.identity);
        Debug.Log(hpSlider.value);
        // Instance.missSFX.Play();    
    }
}
