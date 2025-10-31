using Construction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr7border
{
    public partial class MainWindow : Window
    {
        private Builder _builder;

        public MainWindow()
        {
            InitializeComponent();
            _builder = new Builder();
        }

        
        private void CalculateWallpaper_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double roomWidth = ParseDouble(txtRoomWidth.Text, "ширина комнаты");
                double roomLength = ParseDouble(txtRoomLength.Text, "длина комнаты");
                double roomHeight = ParseDouble(txtRoomHeight.Text, "высота комнаты");
                double windowWidth = ParseDouble(txtWindowWidth.Text, "ширина окна");
                double windowHeight = ParseDouble(txtWindowHeight.Text, "высота окна");
                double doorWidth = ParseDouble(txtDoorWidth.Text, "ширина двери");
                double doorHeight = ParseDouble(txtDoorHeight.Text, "высота двери");
                double rollWidth = cmbRollWidth.SelectedIndex == 0 ? 1.0 : 0.5;

                int rollsCount = _builder.PasteWallpaper(
                    roomWidth, roomLength, roomHeight,
                    windowHeight, windowWidth,
                    doorHeight, doorWidth,
                    rollWidth);

                ShowWallpaperResult(roomWidth, roomLength, roomHeight,
                                  windowWidth, windowHeight,
                                  doorWidth, doorHeight,
                                  rollWidth, rollsCount);
            }
            catch (Exception ex)
            {
                ShowWallpaperError(ex.Message);
            }
        }

        private void ShowWallpaperResult(double roomWidth, double roomLength, double roomHeight,
                                       double windowWidth, double windowHeight,
                                       double doorWidth, double doorHeight,
                                       double rollWidth, int rollsCount)
        {
            string result = $"Комната: {roomWidth}м × {roomLength}м × {roomHeight}м\n" +
                           $"Окно: {windowWidth}м × {windowHeight}м\n" +
                           $"Дверь: {doorWidth}м × {doorHeight}м\n" +
                           $"Рулон: {rollWidth}м × 10,5м\n\n" +
                           $"ТРЕБУЕТСЯ РУЛОНОВ: {rollsCount} шт.\n\n";

            string advice;
            if (rollsCount == 0)
                advice = "Проверьте введённые данные";
            else if (rollsCount == 1)
                advice = "Рекомендуется взять 2 рулона с учётом подгонки рисунка";
            else if (rollsCount <= 3)
                advice = "Рекомендуется взять 1 дополнительный рулон про запас";
            else if (rollsCount <= 6)
                advice = "Для сложных помещений возьмите 2 дополнительных рулона";
            else
                advice = "Для больших помещений рекомендуется запас 2-3 рулона";

            txtWallpaperResult.Text = result + advice;
            wallpaperResultBorder.Visibility = Visibility.Visible;
        }

        private void ShowWallpaperError(string message)
        {
            txtWallpaperResult.Text = $"❌ ОШИБКА: {message}";
            wallpaperResultBorder.Visibility = Visibility.Visible;
        }

       
        private void CalculateLinoleum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double roomWidth = ParseDouble(txtLinRoomWidth.Text, "ширина комнаты");
                double roomLength = ParseDouble(txtLinRoomLength.Text, "длина комнаты");
                double linoleumWidth = GetLinoleumWidth();

                double linoleumLength = _builder.LayLinoleum(roomWidth, roomLength, linoleumWidth);

                ShowLinoleumResult(roomWidth, roomLength, linoleumWidth, linoleumLength);
            }
            catch (Exception ex)
            {
                ShowLinoleumError(ex.Message);
            }
        }

        private double GetLinoleumWidth()
        {
            switch (cmbLinoleumWidth.SelectedIndex)
            {
                case 0: return 2.0;
                case 1: return 2.5;
                case 2: return 3.0;
                case 3: return 3.5;
                default: return 2.0;
            }
        }

        private void ShowLinoleumResult(double roomWidth, double roomLength, double linoleumWidth, double linoleumLength)
        {
            string result = $" Комната: {roomWidth}м × {roomLength}м\n" +
                           $" Ширина линолеума: {linoleumWidth}м\n\n" +
                           $" ТРЕБУЕТСЯ ЛИНОЛЕУМА: {linoleumLength:F2} м\n\n" +
                           $" Площадь комнаты: {roomWidth * roomLength:F2} м²\n" +
                           $" Количество полос: {Math.Ceiling(roomWidth / linoleumWidth)} шт.";

            txtLinoleumResult.Text = result;
            linoleumResultBorder.Visibility = Visibility.Visible;
        }

        private void ShowLinoleumError(string message)
        {
            txtLinoleumResult.Text = $"ОШИБКА: {message}";
            linoleumResultBorder.Visibility = Visibility.Visible;
        }

       
        private void CalculatePaint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double roomWidth = ParseDouble(txtPaintRoomWidth.Text, "ширина комнаты");
                double roomLength = ParseDouble(txtPaintRoomLength.Text, "длина комнаты");
                double paintConsumption = ParseDouble(txtPaintConsumption.Text, "расход краски");
                double canVolume = ParseDouble(txtCanVolume.Text, "объём банки");

                int cansCount = _builder.CeilingPainting(roomWidth, roomLength, paintConsumption, canVolume);

                ShowPaintResult(roomWidth, roomLength, paintConsumption, canVolume, cansCount);
            }
            catch (Exception ex)
            {
                ShowPaintError(ex.Message);
            }
        }

        private void ShowPaintResult(double roomWidth, double roomLength, double paintConsumption, double canVolume, int cansCount)
        {
            double ceilingArea = roomWidth * roomLength;
            double totalPaint = ceilingArea * paintConsumption;

            string result = $"Комната: {roomWidth}м × {roomLength}м\n" +
                           $"Площадь потолка: {ceilingArea:F2} м²\n" +
                           $"Расход краски: {paintConsumption} л/м²\n" +
                           $"Объём банки: {canVolume} л\n\n" +
                           $"ТРЕБУЕТСЯ БАНОК: {cansCount} шт.\n\n" +
                           $"Общее количество краски: {totalPaint:F2} л";

            txtPaintResult.Text = result;
            paintResultBorder.Visibility = Visibility.Visible;
        }

        private void ShowPaintError(string message)
        {
            txtPaintResult.Text = $"ОШИБКА: {message}";
            paintResultBorder.Visibility = Visibility.Visible;
        }

        
        private double ParseDouble(string text, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException($"Поле '{fieldName}' не заполнено");

            text = text.Replace(".", ",");

            if (!double.TryParse(text, out double result))
                throw new ArgumentException($"Некорректное значение в поле '{fieldName}'");

            if (result <= 0)
                throw new ArgumentException($"Значение '{fieldName}' должно быть положительным");

            return result;
        }
    }
}
