using System.Collections.Generic;

namespace Accounting.TrackingService
{
    public interface IAccountOperationTrackingService
    {
        List<AccountOperationInfo> GetOperations(); 
    }
}
