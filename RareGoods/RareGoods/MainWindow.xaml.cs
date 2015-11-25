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
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using RareGoods.RawData;

namespace RareGoods
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private RawData.RawData current = new RawData.RawData();

        private Dictionary<int, StarSystem> starSystemSet = new Dictionary<int, StarSystem>();

        private Dictionary<Tuple<int,int>, double[]> markerDistance = new Dictionary<Tuple<int,int>, double[]>();
        private Dictionary<Tuple<int,int>, int[]> markerSystems = new Dictionary<Tuple<int,int>, int[]>();

        private FlowDocument routing = new FlowDocument();
        private Run textLine = new Run();
        private Paragraph currentBlock = new Paragraph();

        public MainWindow()
        {
            InitializeComponent();

            route2.Document = routing;

            routing.Blocks.Add(currentBlock);

            starSystemSet = current.starSystemDataSet;

            CalculateDistance();

            CalculateMarkers();

            for (int i = 0; i < starSystemSet.Count; i++)
            {
                fetchRoutes(i, 180);
            }

            CreateView();

        }

        private void CalculateDistance()
        {
            double startX = 0;
            double startY = 0;
            double startZ = 0;

            double[] distanceTable = new double[starSystemSet.Count];
            double distance = 0;

            foreach (var origin in starSystemSet)
            {

                startX = starSystemSet[origin.Key].SystemX;
                startY = starSystemSet[origin.Key].SystemY;
                startZ = starSystemSet[origin.Key].SystemZ;

                double endX = 0;
                double endY = 0;
                double endZ = 0;

                Dictionary<int, double> sortingTable = new Dictionary<int, double>();

                foreach (var destination in starSystemSet)
                {

                    endX = starSystemSet[destination.Key].SystemX;
                    endY = starSystemSet[destination.Key].SystemY;
                    endZ = starSystemSet[destination.Key].SystemZ;

                    double distanceX = 0;
                    double distanceY = 0;
                    double distanceZ = 0;

                    if (startX < 0 && endX < 0) distanceX = Math.Min(startX, endX) + Math.Abs(Math.Max(startX, endX));
                    if (startY < 0 && endY < 0) distanceY = Math.Min(startY, endY) + Math.Abs(Math.Max(startY, endY));
                    if (startZ < 0 && endZ < 0) distanceZ = Math.Min(startZ, endZ) + Math.Abs(Math.Max(startZ, endZ));

                    if ((startX < 0 && endX > 0) || (startX > 0 && endX < 0))
                        distanceX = Math.Abs(Math.Min(startX, endX)) + Math.Max(startX, endX);
                    if ((startY < 0 && endY > 0) || (startY > 0 && endY < 0))
                        distanceY = Math.Abs(Math.Min(startY, endY)) + Math.Max(startY, endY);
                    if ((startZ < 0 && endZ > 0) || (startZ > 0 && endZ < 0))
                        distanceZ = Math.Abs(Math.Min(startZ, endZ)) + Math.Max(startZ, endZ);

                    if (startX > 0 && endX > 0) distanceX = Math.Max(startX, endX) - Math.Min(startX, endX);
                    if (startY > 0 && endY > 0) distanceY = Math.Max(startY, endY) - Math.Min(startY, endY);
                    if (startZ > 0 && endZ > 0) distanceZ = Math.Max(startZ, endZ) - Math.Min(startZ, endZ);

                    distance = Math.Pow(distanceX, 2) + Math.Pow(distanceZ, 2);
                    distance = Math.Sqrt(distance);

                    distance = Math.Pow(distance, 2) + Math.Pow(distanceY, 2);
                    distance = Math.Sqrt(distance);

                    distanceTable[destination.Key] = distance;
                    sortingTable[destination.Key] = distance;
                }

                starSystemSet[origin.Key].DistanceTable = distanceTable;

                var sortedDistance = from pair in sortingTable orderby pair.Value select pair;

                int[] sortedSyst = new int[starSystemSet.Count];
                double[] sortedDist = new double[starSystemSet.Count];
                string[] sortedDisp = new string[starSystemSet.Count];

                int sortCounter = 0;

                foreach (KeyValuePair<int, double> dist in sortedDistance)
                {
                    sortedSyst[sortCounter] = dist.Key;
                    sortedDist[sortCounter] = dist.Value;
                    sortedDisp[sortCounter] = starSystemSet[dist.Key].SystemName + "\n" + dist.Value.ToString("F");

                    sortCounter += 1;
                }

                starSystemSet[origin.Key].SortedSystems = sortedSyst;
                starSystemSet[origin.Key].SortedDistances = sortedDist;
                starSystemSet[origin.Key].DisplayDistances = sortedDisp;

            }

        }

        private void CalculateMarkers()
        {
            foreach (var destination in starSystemSet)
            {

                double[] examineDistance = starSystemSet[destination.Key].SortedDistances;
               
                int[] examineSystem = starSystemSet[destination.Key].SortedSystems;

                for (int factor = 0; factor <= 12; factor += 1)
                {
                    double minDistance = 0;

                    if (factor > 0)
                    {
                        minDistance = (factor - 1)*20;
                    }

                    double maxDistance = factor*20;

                    var target = examineDistance.Where(distance => (distance >= minDistance && distance < maxDistance));

                    double[] targetDistance = new double[target.Count()];
                    int[] targetSystem = new int[target.Count()];

                    int targetDistanceCounter = 0;
                    int targetSystemCounter = 0;

                    Tuple<int,int> targetID = new Tuple<int, int>(destination.Key,factor);

                    foreach (double showDistance in target)
                    {
                        targetDistance[targetDistanceCounter] = showDistance;
                        targetDistanceCounter += 1;
                    }

                    foreach (double getDistance in targetDistance)
                    {

                        int index = Array.FindIndex(examineDistance, distance => distance == getDistance);

                        targetSystem[targetSystemCounter] = examineSystem[index];
                        targetSystemCounter += 1;
                    }

                    markerDistance[targetID] = targetDistance;
                    markerSystems[targetID] = targetSystem;

                }

            }

        }

        private void fetchRoutes(int startSystem,int calculatedRange=180)
        {
            
            calculatedRange /= 20;

            Tuple< int,int> getSystems = new Tuple<int, int>(startSystem,calculatedRange);

            int[] endSystemList = markerSystems[getSystems];

            foreach (int endSystem in endSystemList)
            {
                CalculateRoute(startSystem,endSystem);
            }

        }

        private void CalculateRoute(int startSystem, int endSystem)
        {

            double calculatedRange = DistanceBetween(startSystem, endSystem);

            int range = Convert.ToInt32(calculatedRange);

            range/=20;

            range += 2;

            textLine.Text += "\n" + "from " + starSystemSet[startSystem].SystemName + " -> " +
                          starSystemSet[endSystem].SystemName;

            textLine.Text += " current range: " + range+"\n\n";

            int prevSystem = -1;
            double halfPointDistance = 200;
            int halfPointSystem = 0;

            for (int factor = 0; factor <= range; factor += 1)
            {
                Tuple<int, int> startID = new Tuple<int, int>(startSystem, factor);
                Tuple<int, int> endID = new Tuple<int, int>(endSystem, range-factor);

                int[] startSystemList = markerSystems[startID];
                int[] endSystemList = markerSystems[endID];

                var match = startSystemList.Intersect(endSystemList);

                foreach (int matchSystem in match)
                {
                    int factorMin = 0;
                    int factorMax = factor*20;

                    if (factor > 0) factorMin = (factor - 1)*20;

                    double distanceToStart = DistanceBetween(startSystem, matchSystem);
                    double distanceToPrev = 0;
                    double distanceToEnd = DistanceBetween(endSystem, matchSystem);

                    if (Math.Abs(distanceToEnd - distanceToStart) < halfPointDistance)
            {
                        halfPointDistance = distanceToEnd - distanceToStart;
                        halfPointSystem = matchSystem;
                    }

                    if (prevSystem != -1) distanceToPrev = DistanceBetween(prevSystem, matchSystem);

                    prevSystem = matchSystem;

                    textLine.Text +=  "\t[" + factorMin + "-" + factorMax + "] (";
                    textLine.Text +=  distanceToStart.ToString("F") + "):";
                    textLine.Text +=  starSystemSet[matchSystem].SystemName;
                    textLine.Text +=  " (" + distanceToEnd.ToString("F") + ")\t";
                    textLine.Text +=  "\t total: " + (distanceToStart + distanceToEnd).ToString("F");
                    textLine.Text +=  "\t(" + distanceToPrev.ToString("F") + ")\n";
                    
                }
                
            }

            currentBlock.Inlines.Add(textLine);

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

    
        private void CreateView()
        {

            Resources["starSystemSet"] = starSystemSet;

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
    }
}
