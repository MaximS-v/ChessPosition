using System;

namespace ChessPosition
{
    /// <summary>
    /// Получает от пользователя две строки по два символа 
    /// - начальная позиция на шахматной доске и конечная.
    /// Выводит на консоль информацию о корректности данного хода
    /// для нижеперечисленных фигур
    /// </summary>
    internal class Program
    {
        private static readonly string[] ChessmenString =
        {
            "слона",
            "коня",
            "ладьи",
            "ферзя",
            "короля"
        };

        enum Chessmen
        {
            Bishop = 0,
            Knight = 1,
            Rook = 2,
            Queen = 3,
            King = 4
        }

        /// <summary>
        /// Определяет корректность хода для указанной фигуры
        /// </summary>
        /// <param name="startTurn"></param>
        /// <param name="endTurn"></param>
        /// <param name="chessmen"></param>
        /// <returns></returns>
        private static bool IsStepCorrect(string startTurn, string endTurn, Chessmen chessmen)
        {
            switch (chessmen)
            {
                case Chessmen.Bishop:
                    return CalcDeltaHoriz(startTurn, endTurn) == CalcDeltaVert(startTurn, endTurn);
                case Chessmen.Knight:
                    return (CalcDeltaHoriz(startTurn, endTurn) == 1
                        && CalcDeltaVert(startTurn, endTurn) == 2)
                        || (CalcDeltaHoriz(startTurn, endTurn) == 2
                        && CalcDeltaVert(startTurn, endTurn) == 1);
                case Chessmen.Rook:
                    return CalcDeltaHoriz(startTurn, endTurn) == 0
                        || CalcDeltaVert(startTurn, endTurn) == 0;
                case Chessmen.Queen:
                    return IsStepCorrect(startTurn, endTurn, Chessmen.Rook)
                        || IsStepCorrect(startTurn, endTurn, Chessmen.Bishop);
                case Chessmen.King:
                    return CalcDeltaHoriz(startTurn, endTurn) <= 1
                        && CalcDeltaVert(startTurn, endTurn) <= 1;
            }
            return true;
        }

        /// <summary>
        /// Направление для вычисления разницы между началом хода и концом
        /// </summary>
        enum Direction
        {
            Horizontal = 0,
            Vertical = 1
        }

        /// <summary>
        /// Вычисляет разницу в заданном направлении между началом хода и концом
        /// </summary>
        /// <param name="startTurn">начало хода</param>
        /// <param name="endTurn">конец хода</param>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static int CalcDelta(string startTurn, string endTurn, Direction dir)
        {
            var index = (int)dir;
            return Math.Abs(startTurn[index] - endTurn[index]);
        }

        /// <summary>
        /// Вычисляет разницу по горизонтали между началом хода и концом
        /// </summary>
        /// <param name="startTurn">начало хода</param>
        /// <param name="endTurn">конец хода</param>
        /// <returns></returns>
        private static int CalcDeltaHoriz(string startTurn, string endTurn)
        {
            return CalcDelta(startTurn, endTurn, Direction.Horizontal);
        }

        /// <summary>
        /// Вычисляет разницу по вертикали между началом хода и концом
        /// </summary>
        /// <param name="startTurn">начало хода</param>
        /// <param name="endTurn">конец хода</param>
        /// <returns></returns>
        private static int CalcDeltaVert(string startTurn, string endTurn)
        {
            return CalcDelta(startTurn, endTurn, Direction.Vertical);
        }

        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ');
            if (input.Length == 2 && input[0].Length == 2 && input[1].Length == 2)
            {
                var startTurn = input[0];
                var endTurn = input[1];
                if (!String.Equals(startTurn, endTurn))
                {
                    foreach (Chessmen chessmen in Enum.GetValues(typeof(Chessmen)))
                    {
                        var neg = IsStepCorrect(startTurn, endTurn, chessmen) ? "" : "не";
                        Console.WriteLine("Для {0} ход {1}корректный",
                            ChessmenString[(int)chessmen], neg);
                    }
                }
                else
                    Console.WriteLine("Клетки начала и конца хода совпадают");

            }
            else
                Console.WriteLine("Введенные данные некорректны");
        }
    }
}
