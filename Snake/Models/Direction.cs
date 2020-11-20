namespace Snake_SB2020.Models
{
    public class SnakeDirection
    {
        public string Direction { get; set; }
    }

    public static class SnakeDirectionExtension
    {
        /// <summary>
        /// Проверяет корретно ли задано направление
        /// </summary>
        public static bool IsDirectionOk(this SnakeDirection dir)
        {
            string dirLower = dir.Direction.ToLower();
            if (dirLower == "left" || dirLower == "right" || 
                dirLower == "top" || dirLower == "bottom")
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
            switch (dir.Direction.ToLower())
            {
                case "left":
                    return -1;
                case "right":
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
            switch (dir.Direction.ToLower())
            {
                case "top":
                    return -1;
                case "bottom":
                    return 1;
                default:
                    return 0;
            }
        }
    }
}