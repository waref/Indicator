using Depences.Application.IMangers;
using Depences.Domain.Models;
using DepencesApi.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
namespace Depences.Controllers;

[ApiController]
[Route("[controller]")]

public class DeponcesController : ControllerBase
{
    
    private readonly ILogger<DeponcesController> _logger;
    private readonly IDepenceManager<Depence> _depenceManager;

    public DeponcesController(ILogger<DeponcesController> logger, IDepenceManager<Depence> depenceManager)
    {
        _logger = logger;
        _depenceManager = depenceManager;
    }
    #region Get
    /// <summary>
    /// Get All Depences with sorting possibility
    /// </summary>
    /// <param name="sortExpression">Name of the property the sort in Depence</param>
    /// <returns><see cref="IActionResult"/>></returns>
    [SwaggerOperation(Summary = "Lister les depences avec tri",Description = "Tri selon le nom de propriété dans le champ: sortExpression , pour ordre décroissant, ajouter - au début exemple : -NatureId")]
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAll([FromQuery] string? sortExpression = null)
    {
        //sortExpression accept this pattern : - for descending , without for ascending then the property name , it accepts multiple ordering with , separation
        //Exemple for this service you can add "-natureId" ti sort Depences with descending natureId
        _logger.Log(LogLevel.Information, "Start Get DeponcesController");
        var depencesList = await _depenceManager.GetDepenceAsync(sortExpression);
        return Ok(depencesList);
    }
    [HttpGet]
    [Route("[action]")]
    [SwaggerOperation(Summary = "Lister les depences sans tri",Description ="Pour une consultation rapide")]
    public async Task<IActionResult> GetAllNoFilterNoSort()
    {
        _logger.Log(LogLevel.Information, "Start Get DeponcesController");
        var depencesList = await _depenceManager.GetDepenceAsync();
        return Ok(depencesList);
    }
    #endregion

    #region Post
    [HttpPost]
    [Route("[action]")]
    [SwaggerOperation(Summary = "Enregistrer une dépence",Description = "Voir tables préconfigurés dans la BDD pour la Nature de dépence, User et currency")]
    public async Task<IActionResult> Save([FromServices] DepencesBusiness depencesBusiness,
                                        [FromBody] Depence depence,
                                        CancellationToken cancellationToken = default)
    {
        ExecutionResult reuluts =await  depencesBusiness.Save(depence, cancellationToken);
        return Ok(reuluts);
    }
    #endregion

}