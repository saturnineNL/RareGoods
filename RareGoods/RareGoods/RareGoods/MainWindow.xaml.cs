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

        public MainWindow()
        {
            InitializeComponent();

            route2.Document = routing;

            routing.Blocks.Add(currentBlock);

            starSystemSet = calculatedData.starSystemSet;

            CreateView();

        }

        private void fetchRoutes(int startSystem,int calculatedRange=180)
        {
            
            calculatedRange /= 20;

            Tuple< int,int> getSystems = new Tuple<int, int>(startSystem,calculatedRange);

            int[] endSystemList = calculatedData.markerSystems[getSystems];

            foreach (int endSystem in endSystemList)
            {
                if (starSystemSet[endSystem].StationDistance<5000)
                {
                    CalculateRoute(startSystem, endSystem);
                }
               
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

                int[] startSystemList = calculatedData.markerSystems[startID];
                int[] endSystemList = calculatedData.markerSystems[endID];

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

                    if (starSystemSet[matchSystem].StationDistance < 5000)
                    {
                        string textAdd = "";
                        textAdd+= "\t[" + factorMin + "-" + factorMax + "] (" + distanceToStart.ToString("F") + "):" + starSystemSet[matchSystem].SystemName + " (" + distanceToEnd.ToString("F") + ")";
                        textLine.Text += "\t[" + factorMin + "-" + factorMax + "] (";
                        textLine.Text += distanceToStart.ToString("F") + "):";
                        textLine.Text += starSystemSet[matchSystem].SystemName;
                        textLine.Text += " (" + distanceToEnd.ToString("F") + ")\t";

                        if (textAdd.Length < 30) textLine.Text += "\t";
                        

                        if (textAdd.Length < 39)textLine.Text += "\t";

                        textLine.Text += " total: " + (distanceToStart + distanceToEnd).ToString("F");
                        textLine.Text += "\t(" + distanceToPrev.ToString("F") + ") -> " +
                                         starSystemSet[matchSystem].StationDistance + "="+textAdd.Length+ "\n";
                    }
                    
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

            textLine.Text = "";           

            fetchRoutes(fetchSystem);

        }
    }
}
