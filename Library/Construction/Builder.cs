using System;

namespace Construction
{
    /// <summary>
    /// Класс для расчёта строительных материалов.
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// Рассчитывает количество рулонов обоев для оклейки комнаты.
        /// </summary>
        /// <param name="roomWidth">Ширина комнаты (м).</param>
        /// <param name="roomLength">Длина комнаты (м).</param>
        /// <param name="roomHeight">Высота комнаты (м).</param>
        /// <param name="windowHeight">Высота окна (м).</param>
        /// <param name="windowWidth">Ширина окна (м).</param>
        /// <param name="doorHeight">Высота дверного проёма (м).</param>
        /// <param name="doorWidth">Ширина дверного проёма (м).</param>
        /// <param name="rollWidth">Ширина рулона обоев (м).</param>
        /// <returns>Количество рулонов обоев, необходимое для данной комнаты.</returns>
        public int PasteWallpaper(double roomWidth, double roomLength, double roomHeight,
                                  double windowHeight, double windowWidth,
                                  double doorHeight, double doorWidth,
                                  double rollWidth)
        {
            // Проверка входных параметров
            if (roomWidth <= 0 || roomLength <= 0 || roomHeight <= 0)
                throw new ArgumentException("Размеры комнаты должны быть положительными");

            if (windowWidth <= 0 || windowHeight <= 0)
                throw new ArgumentException("Размеры окна должны быть положительными");

            if (doorWidth <= 0 || doorHeight <= 0)
                throw new ArgumentException("Размеры двери должны быть положительными");

            if (rollWidth != 0.5 && rollWidth != 1.0)
                throw new ArgumentException("Ширина рулона должна быть 0.5 м или 1.0 м");

            // Площадь стен комнаты (периметр * высота)
            double wallsArea = 2 * (roomWidth + roomLength) * roomHeight;

            // Площадь окна и двери (вычитаемые площади)
            double windowArea = windowHeight * windowWidth;
            double doorArea = doorHeight * doorWidth;

            // Полезная площадь оклейки (общая площадь минус окно и дверь)
            double usefulArea = wallsArea - windowArea - doorArea;

            // Площадь одного рулона обоев (ширина * длина)
            double rollArea = rollWidth * 10.5; // 10.5 м - стандартная длина рулона

            // Количество рулонов (округляем вверх до целого числа)
            int rollsCount = (int)Math.Ceiling(usefulArea / rollArea);

            // Проверка на минимальное количество
            if (rollsCount < 0) rollsCount = 0;

            return rollsCount;
        }

        /// <summary>
        /// Рассчитывает количество метров линолеума для покрытия пола в комнате.
        /// </summary>
        /// <param name="roomWidth">Ширина комнаты (м).</param>
        /// <param name="roomLength">Длина комнаты (м).</param>
        /// <param name="linoleumWidth">Ширина линолеума (м).</param>
        /// <returns>Количество метров линолеума, необходимого для данной комнаты.</returns>
        public double LayLinoleum(double roomWidth, double roomLength, double linoleumWidth)
        {
            // Проверка входных параметров
            if (roomWidth <= 0 || roomLength <= 0)
                throw new ArgumentException("Размеры комнаты должны быть положительными");

            if (linoleumWidth != 2.0 && linoleumWidth != 2.5 && linoleumWidth != 3.0 && linoleumWidth != 3.5)
                throw new ArgumentException("Ширина линолеума должна быть 2.0, 2.5, 3.0 или 3.5 м");

            // Площадь комнаты
            double roomArea = roomWidth * roomLength;

            // Количество полос линолеума (округляем вверх)
            int stripsCount = (int)Math.Ceiling(roomWidth / linoleumWidth);

            // Общая длина линолеума (количество полос * длина комнаты)
            double totalLength = stripsCount * roomLength;

            return totalLength;
        }

        /// <summary>
        /// Рассчитывает количество банок водоэмульсионной краски для покраски потолка.
        /// </summary>
        /// <param name="roomWidth">Ширина комнаты (м).</param>
        /// <param name="roomLength">Длина комнаты (м).</param>
        /// <param name="paintConsumption">Расход краски на 1 м² (в литрах).</param>
        /// <param name="canVolume">Объём банки (в литрах).</param>
        /// <returns>Количество банок краски, необходимое для покраски потолка.</returns>
        public int CeilingPainting(double roomWidth, double roomLength, double paintConsumption, double canVolume)
        {
            // Проверка входных параметров
            if (roomWidth <= 0 || roomLength <= 0)
                throw new ArgumentException("Размеры комнаты должны быть положительными");

            if (paintConsumption <= 0)
                throw new ArgumentException("Расход краски должен быть положительным");

            if (canVolume <= 0)
                throw new ArgumentException("Объём банки должен быть положительным");

            // Площадь потолка
            double ceilingArea = roomWidth * roomLength;

            // Общее количество краски (площадь * расход)
            double totalPaint = ceilingArea * paintConsumption;

            // Количество банок (округляем вверх)
            int cansCount = (int)Math.Ceiling(totalPaint / canVolume);

            return cansCount;
        }
    }
}
