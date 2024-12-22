using UnityEngine;

public abstract class UIMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    public virtual void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public virtual void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }


}
