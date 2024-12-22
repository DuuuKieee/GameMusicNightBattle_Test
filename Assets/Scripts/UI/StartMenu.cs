using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : UIMenu
{
    [SerializeField] private Button btnBattle;
    void OnClickBattleGameButton()
    {
        GameStateManager.Instance.StartBattle();
    }
    public override void Show()
    {
        base.Show();
        btnBattle.onClick.AddListener(OnClickBattleGameButton);
    }

    public override void Hide()
    {
        base.Hide();
        btnBattle.onClick.RemoveListener(OnClickBattleGameButton);

    }
}
