using System;
using Models;
using UnityEngine;
using UnityEngine.Events;
using Utility;
using Utility.Singleton;

namespace Management
{
    public class GameProcessManager : MonoSingletonManual<GameProcessManager>
    {

        public Process process;

        public enum Process
        {
            Draw,
            Prepare,
            Attack,
            End
        }
        
        protected override void Awake()
        {
            instance = this;
        }

        public void NextProcess()
        {
            switch (process)
            {
                case Process.Draw:
                    process = Process.Prepare;
                    break;
                case Process.Prepare:
                    process = Process.Attack;
                    break;
                case Process.Attack:
                    process = Process.End;
                    break;
                case Process.End:
                    RoundEnd();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void RoundEnd()
        {
            
        }

        public UnityAction onStart;

        public UnityAction onEnd;
        
        public UnityAction<Process> onProcessChanged;
    }
}