using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : UIMenu
{
    [SerializeField] private Button btnRetry;
    void OnClickRetryButton()
    {
        GameStateManager.Instance.StartBattle();
    }
    public override void Show()
    {
        base.Show();
        btnRetry.onClick.AddListener(OnClickRetryButton);
    }

    public override void Hide()
    {
        base.Hide();
        btnRetry.onClick.RemoveListener(OnClickRetryButton);

    }
}
