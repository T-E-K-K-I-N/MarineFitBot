using MarineFitBot.Domain.Entities;
using MarineFitBot.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Telegram.Bot.Types;


namespace MarineFitBot.WebApi.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class UserController(IUsersRepository usersRepository, ILogger<UserController> logger) : ControllerBase
    {
        // GET: api/<UserController>/<GetAllUsers>
        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns>Все пользователи</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<UserEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UserEntity>>> GetAllUsers()
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                var result = await usersRepository.GetAllAsync(cts.Token);

                if (result == null)
                    return NotFound(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(GetAllUsers));
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UserController>/<GetById>/5
        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Пользователь</returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserEntity>> GetById(Guid id)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                var result = await usersRepository.GetByIdAsync(id, cts.Token);

                if (result == null)
                    return NotFound(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(GetById));
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UserController>/<GetByTelegramName>/exampleName
        /// <summary>
        /// Получить пользователя по имени(логину) Telegram
        /// </summary>
        /// <param name="telegramName">Имя(логин) Telegram</param>
        /// <returns>Пользователь</returns>
        [HttpGet("{telegramName}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserEntity>> GetByTelegramName(string telegramName)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                var result = await usersRepository.GetByTelegramNameAsync(telegramName, cts.Token);

                if (result == null)
                    return NotFound(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(GetByTelegramName));
                return BadRequest(ex.Message);
            }
        }


        // POST api/<UserController>/<AddUser>
        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        /// <returns>Добавленный пользователь</returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserEntity>> AddUser([FromBody] UserEntity user)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                var result = await usersRepository.CreateAsync(user, cts.Token);

                if (result == null)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(AddUser));
                return BadRequest(ex.Message);
            }
        }

        // Patch api/<UserController>/<UpdateUser>
        /// <summary>
        /// Обновить пользователя
        /// </summary>
        /// <param name="user">Модель обновляемого пользователя</param>
        /// <returns>Обновленный пользователь</returns>
        [HttpPatch]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserEntity>> UpdateUser([FromBody] UserEntity user)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                var result = await usersRepository.UpdateAsync(user, cts.Token);

                if (result == null)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(UpdateUser));
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<UserController>/<DeleteUser>/5
        /// <summary>
        /// Удалить пользователя 
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteUser(Guid id)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                await usersRepository.DeleteAsync(id, cts.Token);

                return Ok("Пользователь удален.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(DeleteUser));
                return BadRequest(ex.Message);
            }
        }
    }
}
