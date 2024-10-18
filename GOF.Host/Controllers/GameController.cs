using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using GOF.Domain.Interfaces.Services;
using GOF.Domain.Models.ErrorDetailModel.Response;
using GOF.Domain.Models.GameModel.Request;
using GOF.Domain.Models.GameModel.Response;
using GOF.Domain.Models.GameStageModel.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GOF.Host.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/games")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IMapper _mapper;
        private readonly IGameService _service;
        public GameController(ILogger<GameController> logger, IMapper mapper, IGameService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// Get All Gales
        /// </summary>
        /// <returns>The Games list</returns>
        /// <response code="200">Success - The request has succeeded.</response>
        /// <response code="400">Bad Request – This means that client-side input fails validation.</response>
        /// <response code="403">Forbidden – This means the user is authenticated, but it’s not allowed to access a resource.</response>
        /// <response code="412">Precondition Failed - The client has indicated preconditions in its headers which the server does not meet.</response>
        /// <response code="422">Unprocessable Entity - The request was well-formed but was unable to be followed due to semantic errors.</response>
        /// <response code="500">Internal Server Error - The server has encountered a situation it doesn't know how to handle.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetGameResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetailResponse), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ErrorDetailResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorDetailResponse), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<GetGameResponse>>(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Game of Life {id}");
        }

        /// <summary>
        /// Create Game
        /// </summary>
        /// <param name="model">Game to create</param>
        /// <returns>The Game</returns>
        /// <response code="201">Created - The request has succeeded and a new resource has been created as a result of it. This is typically the response sent after a POST request, or after some PUT requests.</response>
        /// <response code="400">Bad Request – This means that client-side input fails validation.</response>
        /// <response code="403">Forbidden – This means the user is authenticated, but it’s not allowed to access a resource.</response>
        /// <response code="412">Precondition Failed - The client has indicated preconditions in its headers which the server does not meet.</response>
        /// <response code="422">Unprocessable Entity - The request was well-formed but was unable to be followed due to semantic errors.</response>
        /// <response code="500">Internal Server Error - The server has encountered a situation it doesn't know how to handle.</response>
        [HttpPost]
        [ProducesResponseType(typeof(PostGameResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDetailResponse), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ErrorDetailResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorDetailResponse), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult> PostCreate([FromBody] PostGameRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateAsync(_mapper.Map<Domain.Entities.GameEntity>(model));

                return Ok(_mapper.Map<PostGameResponse>(result));
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get Next Game Stage
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <returns>The Game Stage</returns>
        /// <response code="201">Created - The request has succeeded and a new resource has been created as a result of it. This is typically the response sent after a POST request, or after some PUT requests.</response>
        /// <response code="400">Bad Request – This means that client-side input fails validation.</response>
        /// <response code="403">Forbidden – This means the user is authenticated, but it’s not allowed to access a resource.</response>
        /// <response code="412">Precondition Failed - The client has indicated preconditions in its headers which the server does not meet.</response>
        /// <response code="422">Unprocessable Entity - The request was well-formed but was unable to be followed due to semantic errors.</response>
        /// <response code="500">Internal Server Error - The server has encountered a situation it doesn't know how to handle.</response>
        [HttpGet("{id}/next")]
        [ProducesResponseType(typeof(GetGameStageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetailResponse), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ErrorDetailResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorDetailResponse), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult> GetNext([FromRoute][Required] Guid id)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.GetNextAsync(id, 1);

                if (result == null)
                    return NotFound(new { id });
                else
                    return Ok(_mapper.Map<GetGameStageResponse>(result));
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Game of Life {id}");
        }
    }
}