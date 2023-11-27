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

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImg = new ImageSource[]
        {
            new BitmapImage(new Uri("Sprite/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/TileRed.png", UriKind.Relative))
        };
        private readonly ImageSource[] blockImg = new ImageSource[]
        {
            new BitmapImage(new Uri("Sprite/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Sprite/Block-Z.png", UriKind.Relative))
        };
        private readonly Image[,] ImgControls;
        private GState gameState = new GState();
        public MainWindow()
        {
            InitializeComponent();
            ImgControls = SetupGCanvas(gameState.Grid);
        }
        private Image[,] SetupGCanvas(Grid grid)
        {
            Image[,] ImgControls = new Image[grid.row, grid.column];
            int cellSize = 25;

            for (int r = 0; r < grid.row; r++)
            {
                for (int c = 0; c < grid.column; c++)
                {
                    Image imgControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };
                    Canvas.SetTop(imgControl, (r - 2) * cellSize + 10);
                    Canvas.SetLeft(imgControl, c * cellSize);
                    GCanvas.Children.Add(imgControl);
                    ImgControls[r, c] = imgControl;
                }
            }
            return ImgControls;
        }
        private void DrawGrid(Grid grid)
        {
            for (int r = 0; r < grid.row; r++)
            {
                for (int c = 0; c < grid.column; c++)
                {
                    int id = grid[r, c];
                    ImgControls[r, c].Opacity = 1;
                    ImgControls[r, c].Source = tileImg[id];
                }
            }
        
        }
        private void DrawBlock(Block block)
        {
            foreach (Pos p in block.TilePos())
            {
                ImgControls[p.Row, p.Column].Opacity = 1;
                ImgControls[p.Row, p.Column].Source = tileImg[block.id];
            }
        }
        private void DrawNBlock(Queue queue)
        {
            Block nextBloc = queue.NBlock;
            NextImg.Source = blockImg[nextBloc.id];
        }
        public void DrawHBlock(Block Hold)
        {
            if (Hold == null) { HoldImg.Source = blockImg[0]; }
            else { HoldImg.Source = blockImg[Hold.id]; }

        }
        private void Draw(GState gameState)
        {
            DrawGrid(gameState.Grid);
            DrawBlock(gameState.CurrentBlock);
            DrawNBlock(gameState.BloQueue);
            DrawHBlock(gameState.Hold);
            Score.Text = $"Score {gameState.Score}";
        }
        private async Task GLoop()
        {
            Draw(gameState);
            while (!gameState.GO)
            {
                await Task.Delay(500);
                gameState.MBDown();
                Draw(gameState);
            }
            GOMenu.Visibility = Visibility.Visible;
            FinalScore.Text = $"Score: {gameState.Score}";
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GO) { return; }
            switch (e.Key)
            {
                case Key.Left:
                    gameState.MBLeft();
                    break;

                case Key.Right:
                    gameState.MBRight();
                    break;

                case Key.Down:
                    gameState.MBDown();
                    break;

                case Key.X:
                    gameState.RBCCW();
                    break;

                case Key.Y:
                    gameState.RBCW();
                    break;

                case Key.C:
                    gameState.HoldBloc();
                    break;

                default:
                    return;
            }
            Draw(gameState);
        }
        private async void GCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GLoop();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GState();
            GOMenu.Visibility = Visibility.Hidden;
            await GLoop();
        }
    }
}