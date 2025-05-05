using Microsoft.AspNetCore.Mvc;     // Necesar pentru [ApiController], [Route], ControllerBase, ActionResult, etc.
using SimpleBlog.Core.Dtos;       // Necesar pentru UserWithPostsDto
using SimpleBlog.Core.Interfaces; // Necesar pentru IUserService
using System.Threading.Tasks;       // Necesar pentru Task

namespace SimpleBlog.Api.Controllers
{
    [ApiController] // Indică faptul că aceasta este o clasă API Controller (activează comportamente specifice API)
    [Route("api/[controller]")] // Definește ruta de bază pentru acest controller: /api/users
                                // [controller] este un placeholder care ia numele clasei fără "Controller" (deci "Users")
    public class UsersController : ControllerBase // Clasa de bază comună pentru controllere API (fără suport View)
    {
        private readonly IUserService _userService; // Injectăm serviciul

        // Constructor pentru Dependency Injection
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET /api/users/{id}
        [HttpGet("{id}")] // Definește că această metodă răspunde la cereri HTTP GET pe ruta specificată
                          // "{id}" este un parametru de rută, valoarea sa va fi legată de parametrul 'id' al metodei
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserWithPostsDto))] // Documentație Swagger: ce returnează la succes (200)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Documentație Swagger: ce returnează dacă nu găsește (404)
        public async Task<ActionResult<UserWithPostsDto>> GetUserWithPosts(int id)
        {
            // Apelăm serviciul pentru a obține datele (DTO-ul)
            var userDto = await _userService.GetUserWithPostsAsync(id);

            // Verificăm dacă serviciul a returnat un utilizator
            if (userDto == null)
            {
                // Dacă nu, returnăm un răspuns HTTP 404 Not Found
                return NotFound(); // Metodă helper din ControllerBase
            }

            // Dacă utilizatorul a fost găsit, returnăm un răspuns HTTP 200 OK
            // cu obiectul DTO serializat automat ca JSON în corpul răspunsului.
            return Ok(userDto); // Metodă helper din ControllerBase
        }

        // Aici am putea adăuga alte metode (endpoint-uri) pentru Users,
        // de exemplu:
        // [HttpGet] // GET /api/users - pentru a lua toți utilizatorii
        // public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers() { ... }

        // [HttpPost] // POST /api/users - pentru a crea un user nou
        // public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto newUser) { ... }
    }
}