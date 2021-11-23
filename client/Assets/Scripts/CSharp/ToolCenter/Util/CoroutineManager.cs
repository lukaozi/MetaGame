using System.Collections;
using UnityEngine;

namespace Core.Managers
{
    public class CoroutineManager
    {
        static private MonoBehaviour gameEntry;
        
        public static  bool IsInit
        {
            get { return gameEntry != null; }
        }

        static public void Init(MonoBehaviour component)
        {
            gameEntry = component;
        }

        static public Coroutine StartCoroutine(IEnumerator routine)
        {
            return gameEntry.StartCoroutine(routine);
        }

        static public void StopCoroutine(IEnumerator routine)
        {
            gameEntry.StopCoroutine(routine);
        }

        static public IEnumerator WaitHandler(System.Action<System.Action> handler)
        {
            bool isDone = false;
            handler(() =>
            {
                if (!isDone)
                {
                    isDone = true;
                }
                else
                {
                    Debug.LogError("WaitHandler 不应该回调两次");
                }
            });

            while (!isDone)
            {
                yield return null;
            }
        }

    }
}