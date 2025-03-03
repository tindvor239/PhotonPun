using SimpleUI;
using Singletons;

public class GameplayController : ScopedSingleton<GameplayController>
{
    private void Start()
    {
        PanelManager.Instance.ShowPanelFromResources<CharacterCustomizePanel>(UIName.CHARACTER_CUSTOMIZE_UI);
    }
}
