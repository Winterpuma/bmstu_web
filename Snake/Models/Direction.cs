using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Snake.Models
{
    public class SnakeDirection
    {
        [EnumDataType(typeof(EnumDirection))]
        public EnumDirection Direction { get; set; }
    }

    public static class SnakeDirectionExtension
    {
        /// <summary>
        /// Проверяет корретно ли задано направление
        /// </summary>
        public static bool IsDirectionOk(this SnakeDirection dir)
        {
            EnumDirection enumVal = dir.Direction;
            if (enumVal == EnumDirection.Left || enumVal == EnumDirection.Right ||
                enumVal == EnumDirection.Top || enumVal == EnumDirection.Bottom)
                return true;

            return false;
        }

        /// <summary>
        /// Проверяет обратное ли текущему переданное направление
        /// (лево/право) || (верх/низ) 
        /// </summary>
        /// <returns>true, если обратное</returns>
        public static bool IsDirectionsContrary(this SnakeDirection dir, SnakeDirection dirToCheckWith)
        {
            int curDX = dir.GetDirectionDX();
            int otherDX = dirToCheckWith.GetDirectionDX();
            int curDY = dir.GetDirectionDY();
            int otherDY = dirToCheckWith.GetDirectionDY();

            if (curDX == otherDX && curDY == otherDY) // направление совпадает
                return false;

            int sumX = otherDX + curDX;
            int sumY = otherDY + curDY;

            if (sumX == 0 || sumY == 0) // обратное
                return true;

            return false;
        }
        
        /// <summary>
        /// Возвращает смещение по координате X для текущего направления
        /// </summary>
        public static int GetDirectionDX(this SnakeDirection dir)
        {
            switch (dir.Direction)
            {
                case EnumDirection.Left:
                    return -1;
                case EnumDirection.Right:
                    return 1;
                default:
                    return 0;
            }
        }
        
        /// <summary>
        /// Возвращает смещение по координате Y для текущего направления
        /// </summary>
        public static int GetDirectionDY(this SnakeDirection dir)
        {
            switch (dir.Direction)
            {
                case EnumDirection.Top:
                    return -1;
                case EnumDirection.Bottom:
                    return 1;
                default:
                    return 0;
            }
        }
    }
    

    /// <summary>
    /// Left, Right, Top, Bottom
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnumDirection
    {
        Left,
        Right,
        Top,
        Bottom
    }
}