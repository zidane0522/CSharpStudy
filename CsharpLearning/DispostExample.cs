using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpLearning
{
    class DispostExample:IDisposable
    {
        private bool isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)//dispose
                {
                    //清理托管代码垃圾
                    //调用引用对象的Dispose
                }
                //清理非托管对象
            }
            isDisposed = true;
        }

        ~DispostExample()
        {
            Dispose(false);
        }

        public void SomeMethod()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("DispostExample");
            }
        }
    }
}
