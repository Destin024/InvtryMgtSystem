using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;

namespace InvtryMgtSystemAPI.Interfaces
{
    public interface ITransferRepository
    {
        ICollection<Transfer> GetTransfers();

        Transfer GetTransfer(Guid transferId);

        ICollection<Transfer> GetTransferByStore(Guid transferId);

        bool TransferExists(Guid transferId);

        bool CreateTransfer(Transfer transfer);
        bool UpdateTransfer(Transfer transfer);
        bool DeleteTransfer(Transfer transfer);

        bool save();

    }
}
