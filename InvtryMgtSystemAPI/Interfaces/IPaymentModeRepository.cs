using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Interfaces
{
    interface IPaymentModeRepository
    {
        ICollection<PaymentMode> GetPaymentModes();
    }
}
