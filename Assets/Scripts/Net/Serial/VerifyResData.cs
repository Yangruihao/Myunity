using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Net.Serial
{
    /**
     *  验证返回的数据格式
     */ 
     [Serializable]
    class VerifyResData
    {
        public string Ip;
        public string Browser;
        public string token;
        public bool re;
    }
    [Serializable]
    class RestartResData
    {
        public string Body;
    }
    [Serializable]
    class GetUsedTime
    {
        public string Code;
        public string Body;
        public string Msg;
    }
}
