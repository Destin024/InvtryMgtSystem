using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;

namespace InvtryMgtSystemAPI.Interfaces
{
    public interface ITransferRepository
    {
        ICollection<StockTransfer> GetTransfers();

        StockTransfer GetTransfer(Guid transferId);

        ICollection<StockTransfer> GetTransferByStore(Guid transferId);

        bool TransferExists(Guid transferId);

        bool CreateTransfer(StockTransfer transfer);
        bool UpdateTransfer(StockTransfer transfer);
        bool DeleteTransfer(StockTransfer transfer);

        bool save();

    }
}
