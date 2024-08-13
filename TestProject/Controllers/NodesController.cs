using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TestProject.Models;
using TestProject.Services.Abstractions;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodesController : ControllerBase
    {
        private readonly INodesService _nodesService;
        private readonly INodesConverter _nodesConverter;
        private readonly IValidator<NodeModel> _validator;

        public NodesController(INodesService nodesService, INodesConverter nodesConverter, IValidator<NodeModel> validator)
        {
            _nodesService = nodesService;
            _nodesConverter = nodesConverter;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IEnumerable<NodeModel>> GetAsync()
        {
            var nodes = await _nodesService.GetAllAsync(false);
            return _nodesConverter.ConvertEntityToModel(nodes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var node = await _nodesService.GetByIdAsync(id);
            if (node == null)
            {
                return NotFound();
            }
            return Ok(_nodesConverter.ConvertEntityToModel(node));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] NodeModel value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var result = await _validator.ValidateAsync(value);

            if (!result.IsValid)
            {
                return BadRequest(result.ToDictionary());
            }

            var node = await _nodesService.AddAsync(_nodesConverter.ConvertModelToEntity(value));
            return Ok(_nodesConverter.ConvertEntityToModel(node));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] NodeModel updateValue)
        {
            if (updateValue == null || updateValue.Id != id)
            {
                return BadRequest();
            }

            var result = await _validator.ValidateAsync(updateValue);

            if (!result.IsValid)
            {
                return BadRequest(result.ToDictionary());
            }

            var node = await _nodesService.UpdateAsync(_nodesConverter.ConvertModelToEntity(updateValue));
            if (node == null)
            {
                return NotFound();
            }

            return Ok(node);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _nodesService.DeleteAsync(id);
            return Ok();
        }
    }
}
