using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfService1
{
    [ServiceContract]
    public class Service1 
    {
        [OperationContract]
        public async Task<string> GetDataAsync(int value)
        {
            await Task.Delay(1000);
            return (value + value).ToString();
        }
    }
}
