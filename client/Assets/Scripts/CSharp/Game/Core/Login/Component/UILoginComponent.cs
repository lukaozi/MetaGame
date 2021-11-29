using System;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

namespace MetaGame
{
    [ObjectSystem]
    public class UiLoginComponentSystem : AwakeSystem<UILoginComponent>
    {
        public override void Awake(UILoginComponent self)
        {
            self.Awake();
        }
    }

    public class UILoginComponent : Component
    {
        private GameObject account;
        private GameObject loginBtn;
        private GameObject ip;
        private GameObject port;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            loginBtn = rc.Get<GameObject>("LoginBtn");
            loginBtn.GetComponent<Button>().onClick.Add(OnLogin);
            this.account = rc.Get<GameObject>("Account");
            this.ip = rc.Get<GameObject>("IP");
            this.port = rc.Get<GameObject>("Port");

            var ipStr = PlayerPrefs.GetString("server_ip");
            if (!string.IsNullOrEmpty(ipStr))
            {
                this.ip.GetComponent<InputField>().text = ipStr;
            }
            
            var portStr = PlayerPrefs.GetString("server_port");
            if (!string.IsNullOrEmpty(ipStr))
            {
                this.port.GetComponent<InputField>().text = portStr;
            }
        }

        public void OnLogin()
        {
            var account = this.account.GetComponent<InputField>().text;
            var ip = this.ip.GetComponent<InputField>().text;
            var port = this.port.GetComponent<InputField>().text;

            GlobalConst.GlobalProto.Address = ip + ":" + port;
            
            PlayerPrefs.SetString("server_ip", ip);
            PlayerPrefs.SetString("server_port", port);

            LoginMgr.OnLoginAsync(account).Coroutine();
            
            this.Close();
        }

        public void Close()
        {
            GameApi.CloseUI(UIType.UILogin);
        }
    }
}