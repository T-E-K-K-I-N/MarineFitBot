using MarineFitBot.Domain.Entities;
using MarineFitBot.Domain.Enums;
using MarineFitBot.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Telegram.Bot.Types;


namespace MarineFitBot.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrainingController(ITrainingsRepository trainingsRepository, ILogger<TrainingController> logger) : ControllerBase
    {
        // GET: api/<TrainingController>/<GetAllTrainings>
        /// <summary>
        /// Получить все тренировки
        /// </summary>
        /// <returns>Все тренировки</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<TrainingEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<TrainingEntity>>> GetAllTrainings()
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                var result = await trainingsRepository.GetAllAsync(cts.Token);

                if (result == null)
                    return NotFound(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(GetAllTrainings));
                return BadRequest(ex.Message);
            }
        }

        // GET api/<TrainingController>/<GetById>/5
        /// <summary>
        /// Получить тренировку по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор тренировки</param>
        /// <returns>Тренировка</returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TrainingEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TrainingEntity>> GetById(Guid id)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                var result = await trainingsRepository.GetByIdAsync(id, cts.Token);

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

        // GET api/<TrainingController>/<GetByUserId>/userId
        /// <summary>
        /// Получить тренировку по идентификатору пользателя
        /// </summary>
        /// <param name="userId">Идентификатор полтзователя</param>
        /// <returns>Тренировка</returns>
        [HttpGet("{userId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<TrainingEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<TrainingEntity>>> GetByUserId(Guid userId)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                var result = await trainingsRepository.GetByIdAsync(userId, cts.Token);

                if (result == null)
                    return NotFound(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(GetByUserId));
                return BadRequest(ex.Message);
            }
        }

        // GET api/<TrainingController>/<GetByStatus>/status
        /// <summary>
        /// Получить тренировки по статусу
        /// </summary>
        /// <param name="status">Статус тренировки</param>
        /// <returns>Тренировка</returns>
        [HttpGet("{status}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<TrainingEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<TrainingEntity>>> GetByStatus(string status = "Pending")
        {
            try
            {
                if (string.IsNullOrEmpty(status))
                    return BadRequest("Статус тренировки не может быть пустым или равен null.");

                CancellationTokenSource cts = new CancellationTokenSource();

                var trainingStatus = new TrainingStatus();

                switch(status)
                {
                    case "Pending":
                        trainingStatus = TrainingStatus.Pending;
                        break;

                    case "Confirmed":
                        trainingStatus = TrainingStatus.Confirmed;
                        break;

                    case "Declined":
                        trainingStatus = TrainingStatus.Declined;
                        break;
                }

                var result = await trainingsRepository.GetByStatusAsync(trainingStatus, cts.Token);

                if (result == null)
                    return NotFound(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(GetByUserId));
                return BadRequest(ex.Message);
            }
        }

        // POST api/<TrainingController>/<AddTraining>
        /// <summary>
        /// Добавить тренировку
        /// </summary>
        /// <param name="training">Модель тренировки</param>
        /// <returns>Добавленная тренировка</returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TrainingEntity>> AddTraining([FromBody] TrainingEntity training)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                var result = await trainingsRepository.CreateAsync(training, cts.Token);

                if (result == null)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(AddTraining));
                return BadRequest(ex.Message);
            }
        }

        // Patch api/<TrainingController>/<UpdateTraining>
        /// <summary>
        /// Обновить тренировку
        /// </summary>
        /// <param name="training">Модель обновляемой тренировки</param>
        /// <returns>Обновленная тренировка</returns>
        [HttpPatch]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TrainingEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TrainingEntity>> UpdateTraining([FromBody] TrainingEntity training)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                var result = await trainingsRepository.UpdateAsync(training, cts.Token);

                if (result == null)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(UpdateTraining));
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<TrainingController>/<DeleteTraining>/5
        /// <summary>
        /// Удалить тренировку 
        /// </summary>
        /// <param name="id">Идентификатор тренировки</param>
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteTraining(Guid id)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                await trainingsRepository.DeleteAsync(id, cts.Token);

                return Ok("Тренировка удалена.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(DeleteTraining));
                return BadRequest(ex.Message);
            }
        }
    }
}
