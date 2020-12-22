using System;
using Snake.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Snake.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameManager _gameManager;

        public GameController(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        /// <summary>
        /// Создает новое игровое поле
        /// </summary>
        /// <returns>id созданного поля</returns>
        /// <response code="200">Идентификатор созданной поля</response>
        [Route("api/gameboard")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public ActionResult GetGameboard()
        {
            Guid i = _gameManager.CreateNewGameBoard(new Size(20, 20), 3000);
            return Ok(i);
        }

        /// <summary>
        /// Возвращает информацию о текущем состоянии игрового поля
        /// </summary>
        /// <param name="id">Идентификатор доски</param>
        /// <returns>Текущее состояние игрового поля</returns>
        /// <response code="200" cref="IGameBoard">Состояние игрового поля</response>
        /// <response code="404">Не найдено соответствующее поле</response>
        [Route("api/gameboard/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IGameBoard))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetGameboard([FromRoute]Guid id)
        {
            IGameBoard gameBoard = _gameManager.GetGameBoard(id);
            if (gameBoard == null)
                return NotFound();
            return Ok(gameBoard);
        }


        /// <summary>
        /// Изменяет направление движения змейки.
        /// Поворот на 180 градусов вернет ошибку.
        /// Поворот в текущее направление допустим.
        /// </summary>
        /// <param name="id">Идентификатор доски</param>
        /// <param name="newSnakeDirection">Новое направление движения змейки</param>
        /// <response code="200">Направление змейки было успешно изменено</response>
        /// <response code="400">Некорректно задано направление</response> 
        /// <response code="404">Не найдено соответствующее поле</response>
        [Route("api/gameboard/{id}")]
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PatchDirection([FromRoute]Guid id, [FromBody] SnakeDirection newSnakeDirection)
        {
            IGameBoard gameBoard = _gameManager.GetGameBoard(id);
            if (gameBoard == null)
                return NotFound();

            int resCode = gameBoard.ChangeSnakeDir(newSnakeDirection);

            if (resCode != 0)
                return BadRequest();
            return Ok();
        }
    }
}
