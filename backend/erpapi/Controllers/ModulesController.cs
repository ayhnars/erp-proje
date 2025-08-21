using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Services.Contrats;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class ModuleController : ControllerBase
{
    private readonly IModuleManager _moduleManager;
    private readonly IMapper _mapper;

    public ModuleController(IModuleManager moduleManager, IMapper mapper)
    {
        _moduleManager = moduleManager;
        _mapper = mapper;
    }

    [HttpGet("GetAllModules")]
    public async Task<IActionResult> GetAllModules()
    {
        var modules = await _moduleManager.GetAllModulesAsync();
        return Ok(modules);
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetModuleById(int id)
    {
        var module = await _moduleManager.GetModuleByIdAsync(id);
        if (module == null)
        {
            return NotFound();
        }
        return Ok(module);
    }

    [HttpPost("CreateModule")]
    public async Task<IActionResult> CreateModule([FromBody] ModuleDtoForCreate dto)
    {
        var module = _mapper.Map<Modules>(dto);
        await _moduleManager.CreateModuleAsync(module);
        return CreatedAtAction(nameof(GetModuleById), new { id = module.Id }, module);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateModule(int id, [FromBody] ModuleDtoForUpdate dto)
    {
        var module = _mapper.Map<Modules>(dto);

        await _moduleManager.UpdateModuleAsync(module);
        var updatedModule = await _moduleManager.GetModuleByIdAsync(id);
        return Ok(updatedModule);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteModule(int id)
    {
        await _moduleManager.DeleteModuleAsync(id);
        return Ok("Module deleted successfully");
    }
}
