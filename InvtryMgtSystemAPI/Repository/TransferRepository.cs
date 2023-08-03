using AutoMapper;
using InvtryMgtSystemAPI.Data;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvtryMgtSystemAPI.Repository
{
    public class TransferRepository :ITransferRepository
    {
        private readonly DataInvntryContext _context;
        private readonly IMapper _mapper;

        public TransferRepository(DataInvntryContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateTransfer(StockTransfer transfer)
        {
             _context.Add(transfer);
            return save();
        }

        public bool DeleteTransfer(StockTransfer transfer)
        {
             _context.Remove(transfer);
            return save();

        }

        public StockTransfer GetTransfer(Guid transferId)
        {
            return _context.StockTransfers.Where(t=>t.StockTransferId==transferId).FirstOrDefault();
        }

        public ICollection<StockTransfer> GetTransferByStore(Guid transferId)
        {
            return _context.StockTransfers.Where(t => t.StockTransferId == transferId).Include(s => s.Store).ToList();
        }

        public ICollection<StockTransfer> GetTransfers()
        {
            return _context.StockTransfers.ToList();
        }

        public bool save()
        {
            var saved=_context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TransferExists(Guid transferId)
        {
            return _context.StockTransfers.Where(t=>t.StockTransferId == transferId).Any();
        }

        public bool UpdateTransfer(StockTransfer transfer)
        {
             _context.Update(transfer);
            return save();

        }
    }
}
