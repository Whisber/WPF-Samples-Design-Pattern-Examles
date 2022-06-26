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

namespace FormattedTextExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            DrawFormattedText(VisualTreeHelper.GetDpi(this));
        }

        private void OnDpiChanged(DpiScale oldDpiScaleInfo, DpiScale newDpiScaleInfo)
        {

            DrawFormattedText(newDpiScaleInfo);
        }
        private void DrawFormattedText(DpiScale dpiInfo)
        {
            CommandClass com = new CommandClass(dpiInfo);
            Draw drawIt = new Draw(com);
            MakeBigger big = new MakeBigger(com);
            drawIt.Execute();
            big.Execute();

        } /// <summary>
          /// //////////////////////Burda uygulamaya basladık command ///////////////////////////////
          /// </summary>
        public interface Icommand
        {
            void Execute();
        }
        public class CommandClass : MainWindow, Icommand
        {
            DpiScale dpiInfo;

            public CommandClass(DpiScale dpiInfo)
            {
                this.dpiInfo = dpiInfo;
            }
            public void DrawFormattedText() //1. fonksiyon
            {
                FormattedText formattedText = new FormattedText(
                    "FABLE",
                    new System.Globalization.CultureInfo("en-US"),
                    FlowDirection.LeftToRight,
                    new Typeface(
                        new System.Windows.Media.FontFamily("Segoe UI"),
                        FontStyles.Normal,
                        FontWeights.Bold,
                        FontStretches.Normal),
                    120,
                    System.Windows.Media.Brushes.Red,
                    dpiInfo.PixelsPerDip);

                // Build a geometry out of the text.
                Geometry geometry = new PathGeometry();
                geometry = formattedText.BuildGeometry(new System.Windows.Point(0, 0));

                // Adjust the geometry to fit the aspect ration of the video by scaling it.
                ScaleTransform myScaleTransform = new ScaleTransform();
                myScaleTransform.ScaleX = .85;
                myScaleTransform.ScaleY = 2.0;
                geometry.Transform = myScaleTransform;

                // Flatten the geometry and create a PathGeometry out of it.
                PathGeometry pathGeometry = new PathGeometry();
                pathGeometry = geometry.GetFlattenedPathGeometry();

                // Use the PathGeometry for the empty placeholder Path element in XAML.
                path.Data = pathGeometry;

                // Use the PathGeometry for the animated ball that follows the path of the text outline.
                matrixAnimation.PathGeometry = pathGeometry;
            }
            public void MakeItBigger() // Burada oluşacak olan fable text i, daha büyük olacak ve rengi "red" yerine "blue" olacak
            { // 2. fonksiyon
                FormattedText formattedText = new FormattedText(
                    "FABLE",
                    new System.Globalization.CultureInfo("en-US"),
                    FlowDirection.LeftToRight,
                    new Typeface(
                        new System.Windows.Media.FontFamily("Segoe UI"),
                        FontStyles.Normal,
                        FontWeights.Bold,
                        FontStretches.Normal),
                    180, // Burada büyütme işlemi yapıldı ve renk degisikligi saglandı
                    System.Windows.Media.Brushes.Blue,
                    dpiInfo.PixelsPerDip);

                // Build a geometry out of the text.
                Geometry geometry = new PathGeometry();
                geometry = formattedText.BuildGeometry(new System.Windows.Point(0, 0));

                // Adjust the geometry to fit the aspect ration of the video by scaling it.
                ScaleTransform myScaleTransform = new ScaleTransform();
                myScaleTransform.ScaleX = .85;
                myScaleTransform.ScaleY = 2.0;
                geometry.Transform = myScaleTransform;

                // Flatten the geometry and create a PathGeometry out of it.
                PathGeometry pathGeometry = new PathGeometry();
                pathGeometry = geometry.GetFlattenedPathGeometry();

                // Use the PathGeometry for the empty placeholder Path element in XAML.
                path.Data = pathGeometry;

                // Use the PathGeometry for the animated ball that follows the path of the text outline.
                matrixAnimation.PathGeometry = pathGeometry;
            }
            public void Execute()
            {
                this.DrawFormattedText();
            }
        }
        public class Draw : Icommand
        {
            private CommandClass cc;
            public Draw(CommandClass cc)
            {
                this.cc = cc;
            }
            public void Execute()
            {
                cc.DrawFormattedText();
            }
        }
        public class MakeBigger : Icommand
        {
            private CommandClass cc;
            public MakeBigger(CommandClass cc)
            {
                this.cc = cc;
            }
            public void Execute()
            {
                cc.MakeItBigger();
            }
        }
    }
}