using System;
using MetaGame;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginMgr:Singleton<LoginMgr>
{
    public void Start()
    {
        UI UILogin = CreateUILogin();
        Game.Scene.GetComponent<UIComponent>().Add(UILogin);
    }
    
    public static UI CreateUILogin()
    {
        try
        {
            ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
            resourcesComponent.LoadBundle(UIType.UILogin.StringToAB());
            GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.UILogin.StringToAB(), UIType.UILogin);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

            UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.UILogin, gameObject, false);

            ui.AddComponent<UILoginComponent>();
            return ui;
        }
        catch (Exception e)
        {
            Log.Error(e);
            return null;
        }
    }
    
    public static async ETVoid OnLoginAsync(string account)
    {
        try
        {
            if (string.IsNullOrEmpty(GlobalConst.GlobalProto.Address))
            {
                Log.Msg("先输入服务器ip和端口");
                return;
            }
            
            //场景跳转
            SceneManager.LoadSceneAsync("MainScene");
            
            // 创建一个ETModel层的Session
            Session session = Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConst.GlobalProto.Address);
            SMLogin msg = (SMLogin)await session.Call(new CMLogin() {msgId = OuterOpcode.CMLogin, Account = account});
            Log.Msg("登录返回");
            Log.Msg(msg);

//            // 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
//            Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
//            R2C_Login r2CLogin = (R2C_Login) await realmSession.Call(new C2R_Login() { Account = account, Password = "111111" });
//            realmSession.Dispose();
//
//            // 创建一个ETModel层的Session,并且保存到ETModel.SessionComponent中
//            ETModel.Session gateSession = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(r2CLogin.Address);
//            ETModel.Game.Scene.AddComponent<ETModel.SessionComponent>().Session = gateSession;
//				
//            // 创建一个ETHotfix层的Session, 并且保存到ETHotfix.SessionComponent中
//            Game.Scene.AddComponent<SessionComponent>().Session = ComponentFactory.Create<Session, ETModel.Session>(gateSession);
//				
//            G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await SessionComponent.Instance.Session.Call(new C2G_LoginGate() { Key = r2CLogin.Key });
//
//            Log.Info("登陆gate成功!");
//
//            // 创建Player
//            Player player = ETModel.ComponentFactory.CreateWithId<Player>(g2CLoginGate.PlayerId);
//            PlayerComponent playerComponent = ETModel.Game.Scene.GetComponent<PlayerComponent>();
//            playerComponent.MyPlayer = player;
//
//            Game.EventSystem.Run(EventIdType.LoginFinish);
//
//            // 测试消息有成员是class类型
//            G2C_PlayerInfo g2CPlayerInfo = (G2C_PlayerInfo) await SessionComponent.Instance.Session.Call(new C2G_PlayerInfo());
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }
    
}