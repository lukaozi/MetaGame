using System.Runtime.CompilerServices;

namespace MetaGame
{
    internal class MoveNextRunner<TStateMachine> where TStateMachine : IAsyncStateMachine
    {
        public TStateMachine StateMachine;

        //[DebuggerHidden]
        public void Run()
        {
            StateMachine.MoveNext();
        }
    }
}