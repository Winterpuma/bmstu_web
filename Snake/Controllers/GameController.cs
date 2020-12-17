using Snake_SB2020.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Snake_SB2020.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        /// <summary>
        /// Возвращает информацию о текущем состоянии игрового поля
        /// </summary>
        /// <returns>Текущее состояние игрового поля</returns>
        /// <response code="200" cref="IGameBoard">Состояние игрового поля</response>
        [Route("api/gameboard")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IGameBoard))]
        public ActionResult GetGameboard()
        {
            IGameBoard gameBoard = GameManager.Instance.GetGameBoard();
            return Ok(gameBoard);
        }


        /// <summary>
        /// Изменяет направление движения змейки.
        /// Поворот на 180 градусов вернет ошибку.
        /// Поворот в текущее направление допустим.
        /// </summary>
        /// <param name="newSnakeDirection">Новое направление движения змейки</param>
        /// <response code="200">Направление змейки было успешно изменено</response>
        /// <response code="400">Некорректно задано направление</response>   
        [Route("api/direction")]
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PatchDirection([FromBody] SnakeDirection newSnakeDirection)
        {
            IGameBoard gameBoard = GameManager.Instance.GetGameBoard();
            int resCode = gameBoard.ChangeSnakeDir(newSnakeDirection);

            if (resCode != 0)
                return BadRequest();
            return Ok();
        }
    }
}
