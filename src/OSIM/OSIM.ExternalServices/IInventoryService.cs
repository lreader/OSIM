using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OSIM.ExternalServices
{
    [ServiceContract]
    public interface IInventoryService
    {
        [OperationContract]
        string[] GetItemTypes();
    }
}
