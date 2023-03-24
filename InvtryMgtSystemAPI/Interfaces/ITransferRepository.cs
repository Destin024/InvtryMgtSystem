using InvtryMgtSystemAPI.Models;
using System.Collections.Generic;

namespace InvtryMgtSystemAPI.Interfaces
{
    public interface ITransferRepository
    {
        ICollection<Transfer> GetTransfers();

        Transfer GetTransfer(int transferId);

        ICollection<Transfer> GetTransferByStore(int transferId);

        bool TransferExists(int transferId);

        bool CreateTransfer(Transfer transfer);
        bool UpdateTransfer(Transfer transfer);
        bool DeleteTransfer(Transfer transfer);

        bool save();

    }
}
