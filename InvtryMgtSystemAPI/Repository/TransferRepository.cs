using AutoMapper;
using InvtryMgtSystemAPI.Data;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
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

        public bool CreateTransfer(Transfer transfer)
        {
             _context.Add(transfer);
            return save();
        }

        public bool DeleteTransfer(Transfer transfer)
        {
             _context.Remove(transfer);
            return save();

        }

        public Transfer GetTransfer(int transferId)
        {
            return _context.Transfers.Where(t=>t.TransferId==transferId).FirstOrDefault();
        }

        public ICollection<Transfer> GetTransferByStore(int transferId)
        {
            return _context.Transfers.Where(t => t.TransferId == transferId).Include(s => s.Store).ToList();
        }

        public ICollection<Transfer> GetTransfers()
        {
            return _context.Transfers.ToList();
        }

        public bool save()
        {
            var saved=_context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TransferExists(int transferId)
        {
            return _context.Transfers.Where(t=>t.TransferId == transferId).Any();
        }

        public bool UpdateTransfer(Transfer transfer)
        {
             _context.Update(transfer);
            return save();

        }
    }
}
