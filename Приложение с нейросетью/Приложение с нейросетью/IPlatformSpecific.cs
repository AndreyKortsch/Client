using System;
using System.Collections.Generic;
using System.Text;

namespace Приложение_с_нейросетью
{
    public interface IPlatformSpecific
    {
        string GetResourcePath(string resourceName);
    }
}
