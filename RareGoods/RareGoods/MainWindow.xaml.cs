using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using RareGoods.RawData;
using RareGoods.StarSystemData;

namespace RareGoods
{
    
    public partial class MainWindow : Window
    {
        private CalculatedData calculatedData = new CalculatedData();
        private Dictionary<int, StarSystem> starSystemSet = new Dictionary<int, StarSystem>();

        private FlowDocument routing = new FlowDocument();
        private Run textLine = new Run();
        private Paragraph currentBlock = new Paragraph();

        private int currentSelection = -1;

        private double centerX = 462;
        private double centerY = 490;

        private double yFactor = 0.0;
        private double zFactor = 1.0;


        public MainWindow()
        {
            InitializeComponent();

            route2.Document = routing;

            routing.Blocks.Add(currentBlock);

            int tempCounter = 0;

            foreach (var ID in calculatedData.starSystemSet)
            {
                if (calculatedData.starSystemSet[ID.Key].StationDistance <=5000)
                {
                    starSystemSet[tempCounter] = calculatedData.starSystemSet[ID.Key];

                    tempCounter += 1;
                }
            }
            
            calculatedData.starSystemSet= starSystemSet;

           calculatedData.CalculateDistance();
   
            CreateView();

            //  PlotAmounts();
            
            PlotSystems();
            


        }

        

        private void fetchRoutes(int startSystem,int calculatedRange=180)
        {
            
            calculatedRange /= 20;

            Tuple< int,int> getSystems = new Tuple<int, int>(startSystem,calculatedRange);

            int[] endSystemList = calculatedData.markerSystems[getSystems];

            foreach (int endSystem in endSystemList)
            {

                int halfSystem = CalculateHalfSystem(startSystem, endSystem);

                CalculateRoute(startSystem, halfSystem, endSystem);

               

            }
 
        }

        private int CalculateHalfSystem(int startSystem, int endSystem)
        {
            double calculatedRange = DistanceBetween(startSystem, endSystem);

            int range = Convert.ToInt32(calculatedRange);

            range /= 20;

            range += 2;

            double distanceBetweenStartEnd = 200;

            int halfSystem = endSystem;

            for (int factor = 0; factor <= range; factor += 1)
            {
                Tuple<int, int> startID = new Tuple<int, int>(startSystem, factor);
                Tuple<int, int> endID = new Tuple<int, int>(endSystem, range - factor);

                int[] startSystemList = calculatedData.markerSystems[startID];
                int[] endSystemList = calculatedData.markerSystems[endID];

                var match = startSystemList.Intersect(endSystemList);

                foreach (int matchSystem in match)
                {

                    var distanceToStart = DistanceBetween(startSystem, matchSystem);
                    var distanceToEnd = DistanceBetween(endSystem, matchSystem);

                    if (Math.Abs(distanceToEnd - distanceToStart) < distanceBetweenStartEnd)
                    {
                        distanceBetweenStartEnd = distanceToEnd - distanceToStart;

                        if (distanceBetweenStartEnd < Math.Abs(25))
                        {
                            halfSystem = matchSystem;
                        }
                    }

                }

            }
            return halfSystem;
        }

        private void CalculateRoute(int startSystem, int halfSystem, int endSystem)
        {
         
            PlotSystems();
            
            

            double calculatedRange = DistanceBetween(startSystem, endSystem);
            double halfpointRange = DistanceBetween(startSystem, halfSystem) - DistanceBetween(endSystem, halfSystem);
            
            currentBlock.Inlines.Add(textLine);

            int range = 9;

            Tuple<int, int> getStartSystems = new Tuple<int, int>(startSystem, range);

            int[] startSystemList = calculatedData.markerSystems[getStartSystems];

            Tuple<int, int> getHalfSystems = new Tuple<int, int>(halfSystem, range);

            int[] halfSystemList = calculatedData.markerSystems[getHalfSystems];

            Tuple<int, int> getEndSystems = new Tuple<int, int>(endSystem, range);

            int[] endSystemList = calculatedData.markerSystems[getEndSystems];

            var match = halfSystemList.Intersect(endSystemList);

            textLine.Text += "\n -> " + starSystemSet[startSystem].SystemName + " (" + starSystemSet[startSystem].StationName + ")\n -> " + 
                                      starSystemSet[halfSystem].SystemName + " (" + starSystemSet[halfSystem].StationName + ")\n -> " +
                                      starSystemSet[endSystem].SystemName + " (" + starSystemSet[endSystem].StationName + ")\n"; 

            bool hasMatch = false;

            foreach (int end in endSystemList)
            {
                int halfEndSystem = CalculateHalfSystem(end, endSystem);

                for (int factor = 0; factor <= range; factor += 1)
                {
                    Tuple<int, int> halfID = new Tuple<int, int>(halfSystem, 9);
                    Tuple<int, int> endID = new Tuple<int, int>(endSystem, range - factor);

                    int[] halfList = calculatedData.markerSystems[halfID];
                    int[] endList = calculatedData.markerSystems[endID];

                    var matchHalfList = halfList.Intersect(endList);

                    foreach (var matching in matchHalfList)
                    {
                        if (matching == halfEndSystem && 
                            startSystem!=halfSystem &&
                            DistanceBetween(endSystem, matching)> -150 && 
                            DistanceBetween(endSystem, matching) < 150)
                        {

                            PlotAmounts(startSystem);
                            PlotAmounts(halfSystem);
                            PlotAmounts(endSystem);
                            PlotAmounts(matching);
                            PlotAmounts(end);

                            PlotBlackMarket(startSystem);
                            PlotBlackMarket(halfSystem);
                            PlotBlackMarket(endSystem);
                            PlotBlackMarket(matching);
                            PlotBlackMarket(end);

                            

                            plotRoute(startSystem, halfSystem, 0xF0, 0xF0, 0xF0);
                            plotRoute(halfSystem, endSystem, 0xF0, 0xF0, 0xF0);
                            plotRoute(endSystem, matching, 0xF0, 0xF0, 0xF0);
                            plotRoute(matching, end, 0xF0, 0xF0, 0xF0);

                            PlotSystem(startSystem,20,0x80,0xF0,0xF0,0xF0);
                            PlotSystem(halfSystem,15, 0x80, 0xF0,0x40,040);
                            PlotSystem(endSystem,20, 0x80, 0x40,0xF0,0x40);
                            PlotSystem(matching, 15, 0x80, 0x40, 0xF0, 0x40);
                            PlotSystem(end, 20, 0x80, 0x80, 0x80, 0xF0);

                            PlotLabel(startSystem, 20, 0xA0, 0xF0, 0x80, 0x80);
                            PlotLabel(halfSystem, 15, 0x80, 0xF0, 0x80, 0x80);
                            PlotLabel(endSystem, 20, 0xA0, 0x80, 0xF0, 0x80);
                            PlotLabel(matching, 15, 0x80, 0xF0, 0x80, 0xF0);
                            PlotLabel(end, 20, 0xA0, 0x80, 0x80, 0xF0);

                            hasMatch = true;

                            textLine.Text +="\n -> "+ starSystemSet[startSystem].SystemName + " ("+ starSystemSet[startSystem].StationName+")\n -> " +
                                             starSystemSet[halfSystem].SystemName + " (" + starSystemSet[halfSystem].StationName + ")\n -> " +
                                             starSystemSet[endSystem].SystemName + " (" + starSystemSet[endSystem].StationName + ")\n -> " +
                                             starSystemSet[matching].SystemName + " (" + starSystemSet[matching].StationName + ")\n -> " + 
                                             starSystemSet[end].SystemName + " (" + starSystemSet[end].StationName + ")\n";
                        }
                    }
                }
            }

            if (!hasMatch)
            {
                PlotAmounts(startSystem);
                PlotAmounts(halfSystem);
                PlotAmounts(endSystem);

                PlotSystem(startSystem, 20, 0x40, 0xF0, 0xF0, 0xF0);
                plotRoute(startSystem, halfSystem, 0x80, 0x80, 0x80);
                PlotLabel(startSystem, 20, 0xA0, 0xF0, 0xF0, 0xF0);

                PlotSystem(halfSystem, 15, 0x40, 0x80, 0xF0, 0xF0);
                plotRoute(halfSystem, endSystem, 0x80, 0x80, 0x80);
                PlotLabel(halfSystem, 15, 0x80, 0x80, 0xF0, 0xF0);

                PlotSystem(endSystem, 20, 0x40, 0xF0, 0x80, 0xF0);
                PlotLabel(endSystem, 20, 0xA0, 0xF0, 0x80, 0xF0);

                PlotBlackMarket(startSystem);
                PlotBlackMarket(halfSystem);
                PlotBlackMarket(endSystem);

                

            }

            if (hasMatch) textLine.Text += "----------------------------------------------------------------------------------------------------";

            

            // PlotSystem(startSystem, 20,0x20, 0xFF, 0xFF, 0xFF);

        }

        private void PlotSystems()
        {
            for (int count = 0; count < starSystemSet.Count; count += 1)
            {
                PlotSystem(count, 8, 0x10, 0x40, 0x40, 0x40);


            }
        }

        private void PlotAmounts(int system)
        {
            int marketValue = 0;

            
                marketValue = (starSystemSet[system].MinAmount + starSystemSet[system].MaxAmount) / 2;

                PlotSystem(system, starSystemSet[system].MinAmount * 3, 0x10, 0x80, 0x40, 0x10);
                PlotSystem(system, marketValue * 3, 0x20, 0xA0, 0x60, 0x10);
                PlotSystem(system, starSystemSet[system].MaxAmount * 3, 0x40, 0xF0, 0x80, 0x10);

           
        }

        private void PlotBlackMarket(int system)
        {
           
                if (starSystemSet[system].blackMarket == 0)
                {
                    PlotSystem(system, 15, 0x80, 0x40, 0x10, 0x10);
                }

                if (starSystemSet[system].blackMarket == 1)
                {
                    PlotSystem(system, 15, 0x80, 0x10, 0x40, 0x10);
                }

                if (starSystemSet[system].blackMarket == 2)
                {
                    PlotSystem(system,15, 0x80, 0xF0, 0xF0, 0x20);
                    
                }

            

        }

        private void plotInRange(int system, int range)
        {
            Tuple<int,int> rangeSystems = new Tuple<int, int>(system,range);

            PlotSystem(system, (5*range)*20, 0x20, 0xF0, 0xF0, 0xF0);
            PlotSystem(system, (4*range) *20, 0x20, 0x00, 0x00, 0x00);
            int[] rangeList = calculatedData.markerSystems[rangeSystems];

            int previousSystem = -1;

            for (int rangeID = 0; rangeID < rangeList.Length; rangeID += 1)
            {
                int currentSystem = rangeList[rangeID];

                if (rangeID != system)
                {

                    PlotSystem(currentSystem, 20, 0x80, 0xF0, 0x80, 0x80);

                }

            }
            
        }

        private void PlotSystem(int system, double size,byte alpha, byte red, byte green, byte blue)
        {
            Dot  systemDot = new Dot();
            Canvas systemCanvas = new Canvas();
            
            systemDot.originX = centerX;
            systemDot.originY = centerY;

            systemDot.x = starSystemSet[system].SystemX*2.4;
            systemDot.y = ((starSystemSet[system].SystemY*2.4)* yFactor) + ((starSystemSet[system].SystemZ * 2.4)* zFactor); 
           
            systemCanvas = systemDot.Draw(Color.FromArgb(alpha, red, green, blue), size);

            systemCanvas.Tag = system;

           systemCanvas.MouseDown +=new MouseButtonEventHandler(selectHandler);

    
            stage.Children.Add(systemCanvas);
        }

        

        private void plotRoute(int start, int end, byte red, byte green, byte blue)
        {
            Point startPoint = new Point();
            Point endPoint = new Point();

            Line line = new Line();

            line.X1 = starSystemSet[start].SystemX*2.4;
            line.X2 = starSystemSet[end].SystemX * 2.4;

              line.Y1 = ((starSystemSet[start].SystemY * 2.4)* yFactor) + ((starSystemSet[start].SystemZ * 2.4) * zFactor);
              line.Y2 = ((starSystemSet[end].SystemY * 2.4)* yFactor) + ((starSystemSet[end].SystemZ * 2.4) * zFactor);

            line.Stroke=new SolidColorBrush(Color.FromArgb(0x80,red,green,blue));

            line.Margin=new Thickness(centerX,centerY,0,0);

            stage.Children.Add(line);
        }

        private void PlotLabel(int system, double size, byte alpha, byte red, byte green, byte blue)
        {
            Label systemLabel = new Label();

            double offsetX = starSystemSet[system].SystemX * 2.4;
            double offsetY = ((starSystemSet[system].SystemY * 2.4)* yFactor) + ((starSystemSet[system].SystemZ * 2.4) * zFactor);

            systemLabel.Content = starSystemSet[system].SystemName;

            systemLabel.Margin = new Thickness(centerX + offsetX , centerY + offsetY , 0, 0);
            systemLabel.Foreground = new SolidColorBrush(Color.FromArgb(alpha,red,green,blue));

            stage.Children.Add(systemLabel);
        }

        private double DistanceBetween(int systemA,int systemB)
        {
            double distance = 0;
            double[] distanceListA = new double[starSystemSet.Count];
            int[] systemListA = new int[starSystemSet.Count];

            distanceListA = starSystemSet[systemA].SortedDistances;
            systemListA = starSystemSet[systemA].SortedSystems;

            foreach (int system in systemListA)
            {
                int index = Array.FindIndex(systemListA, search => search == systemB);

                distance = distanceListA[index];

            }

            return distance;
        }

        private double[] Displacement(int sysA, int sysB)
        {
            double[] distanceTable = new double[3];

            double startX = starSystemSet[sysA].SystemX;
            double startY = starSystemSet[sysA].SystemY;
            double startZ = starSystemSet[sysA].SystemZ;

            double endX = starSystemSet[sysB].SystemX;
            double endY = starSystemSet[sysB].SystemY;
            double endZ = starSystemSet[sysB].SystemZ;

            double distanceX = 0;
            double distanceY = 0;
            double distanceZ = 0;

            if (startX < 0 && endX < 0) distanceX = Math.Min(startX, endX) + Math.Abs(Math.Max(startX, endX));
            if (startY < 0 && endY < 0) distanceY = Math.Min(startY, endY) + Math.Abs(Math.Max(startY, endY));
            if (startZ < 0 && endZ < 0) distanceZ = Math.Min(startZ, endZ) + Math.Abs(Math.Max(startZ, endZ));

            if ((startX < 0 && endX > 0) || (startX > 0 && endX < 0)) distanceX = Math.Abs(Math.Min(startX, endX)) + Math.Max(startX, endX);
            if ((startY < 0 && endY > 0) || (startY > 0 && endY < 0)) distanceY = Math.Abs(Math.Min(startY, endY)) + Math.Max(startY, endY);
            if ((startZ < 0 && endZ > 0) || (startZ > 0 && endZ < 0)) distanceZ = Math.Abs(Math.Min(startZ, endZ)) + Math.Max(startZ, endZ);

            if (startX > 0 && endX > 0) distanceX = Math.Max(startX, endX) - Math.Min(startX, endX);
            if (startY > 0 && endY > 0) distanceY = Math.Max(startY, endY) - Math.Min(startY, endY);
            if (startZ > 0 && endZ > 0) distanceZ = Math.Max(startZ, endZ) - Math.Min(startZ, endZ);

            distanceTable[0] = distanceX;
            distanceTable[1] = distanceY;
            distanceTable[2] = distanceZ;

            return distanceTable;
        }

        private void CreateView()
        {

            Resources["starSystemSet"] = calculatedData.starSystemSet;

            GridView starGridView = new GridView();

            GridViewColumn systemName = new GridViewColumn();
            GridViewColumn stationName = new GridViewColumn();
            GridViewColumn goodsName = new GridViewColumn();

            systemName.Header = "Star System";
            systemName.Width = 150;
            systemName.DisplayMemberBinding = new Binding("DisplaySystem");

            stationName.Header = "Station Name";
            stationName.Width = 150;
            stationName.DisplayMemberBinding = new Binding("DisplayStation");

            goodsName.Header = "Goods";
            goodsName.Width = 150;
            goodsName.DisplayMemberBinding = new Binding("DisplayGoods");

            starGridView.Columns.Add(systemName);
            starGridView.Columns.Add(stationName);
            starGridView.Columns.Add(goodsName);

            for (int col = 0; col < starSystemSet.Count; col += 1)
            {

                GridViewColumn target = new GridViewColumn();

                target.Header = "[" + col + "]";
                target.Width = 100;
                target.DisplayMemberBinding = new Binding("DisplayDistances[" + col + "]");

                if (col > 0)
                {
                    starGridView.Columns.Add(target);
                }
            }

            stars.View = starGridView;
            
            foreach (var name in starSystemSet)
            {
                starSystemSet[name.Key].DisplaySystem = starSystemSet[name.Key].SystemName + "\n" +
                                                        starSystemSet[name.Key].GoodsName;
                starSystemSet[name.Key].DisplayStation = starSystemSet[name.Key].StationName + "\n" +
                                                         starSystemSet[name.Key].StationDistance;
                starSystemSet[name.Key].DisplayGoods = starSystemSet[name.Key].GoodsPrices +
                                                       "  [" + starSystemSet[name.Key].MinAmount + "-" +
                                                       starSystemSet[name.Key].MaxAmount + "]\n" +
                                                       "[" + (starSystemSet[name.Key].MinAmount*16) + "K -" +
                                                       (starSystemSet[name.Key].MaxAmount*16) + "K ]";

                stars.Items.Add(new StarSystem()
                {
                    DisplaySystem = starSystemSet[name.Key].DisplaySystem,
                    DisplayStation = starSystemSet[name.Key].DisplayStation,
                    DisplayGoods = starSystemSet[name.Key].DisplayGoods,
                    DisplayDistances = starSystemSet[name.Key].DisplayDistances
                });

                SelectSystemFrom.Items.Add(starSystemSet[name.Key].SystemName);
            }
        }

        private void SelectSystemFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int fetchSystem = SelectSystemFrom.SelectedIndex;

            if (fetchSystem < 0) fetchSystem = 0;

            currentSelection = fetchSystem;

            textLine.Text = "";

            stage.Children.Clear();

            fetchRoutes(fetchSystem);

        }

        void selectHandler(object sender, MouseButtonEventArgs e)
        {
            int target = Convert.ToInt32((sender as Canvas).Tag);

            textLine.Text = "";

            stage.Children.Clear();

            PlotSystems();

            currentSelection = target;

            fetchRoutes(target);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {

            yFactor += 0.1;
            zFactor -= 0.1;

            if(yFactor > 1) yFactor = 1;
            if (zFactor < 0) zFactor = 0;

            if (currentSelection > -1)
            {
                textLine.Text = "";

                stage.Children.Clear();

                fetchRoutes(currentSelection);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            yFactor -= 0.1;
            zFactor += 0.1;
            
            if (yFactor < 0) yFactor = 0;
            if (zFactor > 1) zFactor = 1;

            if (currentSelection > -1)
            {
                textLine.Text = "";

                stage.Children.Clear();

                

                fetchRoutes(currentSelection);

                
            }
        }
    }
}
