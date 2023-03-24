using AutoMapper;
using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public StoreController(IStoreRepository storeRepository,IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetStore()
        {
            var stores = _mapper.Map<List<StoreDto>>(_storeRepository.GetStores());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(stores);
        }
        [HttpGet("storeId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult GetStore(int storeId)
        {
            var store = _mapper.Map<StoreDto>(_storeRepository.GetStore(storeId));

            if (storeId == 0)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(store);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]

        public IActionResult CreateStore([FromBody]StoreDto createStore)
        {
            if (createStore == null)
            {
                return BadRequest(ModelState);
            }
            var store = _storeRepository.GetStores()
                .Where(s => s.Name.Trim().ToUpper() == createStore.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (store != null)
            {
                ModelState.AddModelError("", "Store Already Exists");
                return StatusCode(400, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var storeMap = _mapper.Map<Store>(createStore);

            if (!_storeRepository.CreateStore(storeMap))
            {
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Saved");
        }
        [HttpPut("{storeid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult UpdateStore(int storeId, [FromBody]StoreDto updateStore)
        {
            if (updateStore == null)
            {
                return BadRequest(ModelState);
            }
            if (storeId == null)
            {
                return BadRequest(ModelState);
            }
            if (!_storeRepository.StoreExists(storeId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var storeMap = _mapper.Map<Store>(updateStore);
            if (!_storeRepository.UpdateStore(storeMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating store");
            }
            return NoContent();
        }

        [HttpDelete("{storeId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteStore(int storeId)
        {
            if (!_storeRepository.StoreExists(storeId))
            {
                return NotFound();
            }
            var userToDelete = _storeRepository.GetStore(storeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_storeRepository.DeleteStore(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting store");
                return StatusCode(400, ModelState);
            }
            return NoContent();
        }
    }
}
