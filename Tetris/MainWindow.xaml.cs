using System.Text;
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
        private readonly Image[,] ImgControl;
        private GState gameState = new GState();
        public MainWindow()
        {
            InitializeComponent();
            ImgControl = SetupGCanvas(gameState.GGrid);
        }
        private Image[,] SetupGCanvas(Grid grid)
        {
            Image[,] ImgControls = new Image[grid.row, grid.column];
            int cellSize = 25;
            for (int r = 0; r < grid.row; r++)
            {
                for (int c = 0; c < grid.column; c++)
                {
                    Image ImgControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };
                    
                    GCanvas.Children.Add(ImgControl);
                    Canvas.SetTop(ImgControl, (r - 2) * cellSize);
                    Canvas.SetLeft(ImgControl, c * cellSize);
                    ImgControls[r, c] = ImgControl;
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
                    ImgControl[r, c].Opacity = 1;
                    ImgControl[r, c].Source = tileImg[id];
                }
            }
        
        }
        private void DrawBlock(Block block)
        {
            foreach (Pos p in block.TilePos())
            {
                ImgControl[p.Row, p.Column].Opacity = 1;
                ImgControl[p.Row, p.Column].Source = tileImg[block.id];
            }
        }
        private void Draw(GState gameState)
        {
            DrawGrid(gameState.GGrid);
            DrawBlock(gameState.CurrentBlock);
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
                    gameState.RBCW();
                    break;

                case Key.Y:
                    gameState.RBCCW();
                    break;

                default:
                    return;
            }
            Draw(gameState);
        }
        private void GCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            Draw(gameState);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}