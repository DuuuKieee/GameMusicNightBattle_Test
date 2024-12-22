using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownEffect : MonoBehaviour
{
    public Sprite[] countdownSprites;
    public AudioClip[] countdownSounds; 
    public Image mainImage; 
    public Image effectImage; 
    public AudioSource audioSource; 
    public float duration = 1.0f; 

    private void SetActiveCountdown(bool active)
    {
        mainImage.gameObject.SetActive(active);
        effectImage.gameObject.SetActive(active);
    }

    public void Initialize()
    {
        if (countdownSprites.Length == 0 || mainImage == null || effectImage == null || audioSource == null)
        {
            Debug.LogError("Hãy đảm bảo tất cả các thành phần cần thiết được gắn kết trong Inspector.");
            return;
        }
        SetActiveCountdown(false);
    }

    public IEnumerator PlayCountdown()
    {
        SetActiveCountdown(true);
        for (int i = 0; i < countdownSprites.Length; i++)
        {
            mainImage.sprite = countdownSprites[i];
            mainImage.color = new Color(1, 1, 1, 1);

            effectImage.sprite = countdownSprites[i];
            effectImage.transform.localScale = Vector3.one;
            effectImage.color = new Color(1, 1, 1, 1);

            // Phát âm thanh nếu tồn tại
            if (i < countdownSounds.Length && countdownSounds[i] != null)
            {
                audioSource.PlayOneShot(countdownSounds[i]);
            }

            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;

                effectImage.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.5f, t);

                effectImage.color = new Color(1, 1, 1, 1 - t);

                yield return null;
            }

            effectImage.color = new Color(1, 1, 1, 0);
        }
        SetActiveCountdown(false);
    }
}
