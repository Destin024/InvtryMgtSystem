using AutoMapper;
using InvtryMgtSystemAPI.Data;
using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvtryMgtSystemAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IMapper _mapper;
        private readonly DataInvntryContext _ctx;

        public TransferController(ITransferRepository transferRepository, IMapper mapper, DataInvntryContext ctx)
        {
            _transferRepository = transferRepository;
            _mapper = mapper;
            _ctx = ctx;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult GetTransfers()
        {
            var transfers = _mapper.Map<List<StockTransferDto>>(_transferRepository.GetTransfers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(transfers);
        }

        [HttpGet("{transferId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult GetTransfer(Guid transferId)
        {
            if (_transferRepository.TransferExists(transferId))
            {
                return NotFound();
            }

            var transfer = _mapper.Map<StockTransferDto>(_transferRepository.GetTransfer(transferId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transfer);
        }
        [HttpGet("{transferId}/store")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult GetTransferByStore(Guid  storeId)
        {
            if (!_transferRepository.TransferExists(storeId))
            {
                return NotFound();
            }

            var stores = _mapper.Map<List<StoreDto>>(_transferRepository.GetTransferByStore(storeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(stores);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateTransfer([FromBody]StockTransferDto createTransfer)
        {
            if (createTransfer == null)
            {
                return BadRequest();
            }

            var transfer = _transferRepository.GetTransfers()
                .Where(t => t.TransferQuantity == createTransfer.TransferQuantity).FirstOrDefault();
            if(transfer != null)
            {
                 ModelState.AddModelError("", "Transfer Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transferMap = _mapper.Map<StockTransfer>(createTransfer);


            if (!_transferRepository.CreateTransfer(transferMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
            }
            return Ok("Successfully Created");
        }

        [HttpPost("StockTransfer")]
        public IActionResult StockTransfer(StockTransferDto stockTransfer)
        {

            StockTransfer stockT = new StockTransfer();

            stockT.TransferQuantity = stockTransfer.TransferQuantity;
            stockT.ProductInventoryId = stockTransfer.ProductInventoryId;
            stockT.StoreId = stockTransfer.StoreId;
            stockT.UserId = stockTransfer.UserId;
            stockT.CreatedDate = DateTime.Now;
            stockT.StatusId = 1;


            var InventoryResult = _ctx.ProductInventories.Where(o => o.ProductInventoryId == stockT.ProductInventoryId).FirstOrDefault();
            var currentQuantity = InventoryResult.Quantity - stockT.TransferQuantity;
            InventoryResult.Quantity = currentQuantity;


            _ctx.StockTransfers.Add(stockT);
            _ctx.SaveChanges();

            var result = new
            {
                StatusCode = 200,
                Content = "Stock Transferred Successfully",
            };
            return Ok(result);
        }

        [HttpPut("transferId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateTransfer(Guid transferId, [FromForm] StockTransferDto updateTransfer)
        {
            if (updateTransfer == null)
            {
                return BadRequest(ModelState);
            }
            if (transferId == null)
            {
                return BadRequest(ModelState);
            }
            if (!_transferRepository.TransferExists(transferId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var transferMap = _mapper.Map<StockTransfer>(updateTransfer);
            if (!_transferRepository.UpdateTransfer(transferMap))
            {
                 ModelState.AddModelError("", "Something Went wrong while Updating Transfer");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{transferId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteTransfer(Guid transferId, [FromBody]StockTransferDto deleteTransfer)
        {
            if (deleteTransfer == null)
            {
                return BadRequest(ModelState);
            }
            if (transferId == null)
            {
                return BadRequest(ModelState);
            }
            if (!_transferRepository.TransferExists(transferId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var transferToDelete= _transferRepository.GetTransfer(transferId);

            if (!_transferRepository.DeleteTransfer(transferToDelete))
            {
                ModelState.AddModelError("","Something went wrong while deleting transfer");
                return StatusCode(422, ModelState);
            }
            

            return NoContent();

        }
    }
}
