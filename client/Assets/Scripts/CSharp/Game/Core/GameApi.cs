
using MetaGame;

public class GameApi
{
    public static void CloseUI(string uiType)
    {
        Game.Scene.GetComponent<UIComponent>().Remove(uiType);
        Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(uiType.StringToAB());
    }
}