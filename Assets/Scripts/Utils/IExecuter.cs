using System;

namespace Assets.Scripts.Utils
{
    public interface IExecuter
    {
        public void Execute(Action callback);
    }
}
