using AutoMapper;
using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace InvtryMgtSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IMapper _mapper;

        public TransferController(ITransferRepository transferRepository, IMapper mapper)
        {
            _transferRepository = transferRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult GetTransfers()
        {
            var transfers = _mapper.Map<List<TransferDto>>(_transferRepository.GetTransfers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(transfers);
        }

        [HttpGet("{transferId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult GetTransfer(int transferId)
        {
            if (_transferRepository.TransferExists(transferId))
            {
                return NotFound();
            }

            var transfer = _mapper.Map<TransferDto>(_transferRepository.GetTransfer(transferId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transfer);
        }
        [HttpGet("{transferId}/store")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult GetTransferByStore(int  storeId)
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

        public IActionResult CreateTransfer([FromBody]TransferDto createTransfer)
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
            var transferMap = _mapper.Map<Transfer>(createTransfer);

            if (!_transferRepository.CreateTransfer(transferMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
            }
            return Ok("Successfully Created");
        }

        [HttpPatch("transferId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateTransfer(int transferId, [FromForm] TransferDto updateTransfer)
        {
            if (updateTransfer == null)
            {
                return BadRequest(ModelState);
            }
            if (transferId == 0)
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
            var transferMap = _mapper.Map<Transfer>(updateTransfer);
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

        public IActionResult DeleteTransfer(int transferId, [FromBody]TransferDto deleteTransfer)
        {
            if (deleteTransfer == null)
            {
                return BadRequest(ModelState);
            }
            if (transferId == 0)
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
