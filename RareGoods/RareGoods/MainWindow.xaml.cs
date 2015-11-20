using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RareGoods
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly Dictionary<int, double[]> location = new Dictionary<int, double[]>();
        private readonly Dictionary<int, string> starSystem = new Dictionary<int, string>();
        private readonly Dictionary<int, string> station = new Dictionary<int, string>();
        private readonly Dictionary<int,string> goods = new Dictionary<int, string>(); 
        private readonly Dictionary<int,int> stationDistance = new Dictionary<int, int>();

        private Dictionary<int,double[]> distance = new Dictionary<int, double[]>();
        private Dictionary<int,int[]> distanceOrder = new Dictionary<int, int[]>(); 
        private Dictionary<int,string[]> distanceSystem = new Dictionary<int, string[]>();
        private Dictionary<int, double> Rotation = new Dictionary<int, double>();


        List<string> starSystemList = new List<string>();
        List<string> stationList = new List<string>();
        List<string[]> distanceSystemList = new List<string[]>();
        List<double[]> distanceList = new List<double[]>();

        private int[] jumpList = new int[120];
        private int[] soldList = new int[120];
        private int jumpListCounter = 0;
        private int currentSystemID = 0;

        public MainWindow()
        {
            InitializeComponent();

            RawData();

            setRotation();

            setDistance();

            CreateView();

            CalculateRoute();
        }

        private void RawData()
        {
            // id
            // star system 
            // station name
            // location x,y,z
            // item name
            // item price

            location.Add(0, new double[] { -7.31, -20.28, -50.91 });
            location.Add(1, new double[] { -14.13, -116.97, -32.53 });
            location.Add(2, new double[] { -14.13, -116.97, -32.53 });
            location.Add(3, new double[] { 46.91, 23.63, -59.75 });
            location.Add(4, new double[] { 100.75, -102.63, 8.41 });
            location.Add(5, new double[] { -11.56, 43.81, 11.63 });
            location.Add(6, new double[] { -32.41, 169.53, -49.44 });
            location.Add(7, new double[] { 3.03, -0.09, 3.16 });
            location.Add(8, new double[] { -12.31, -2.75, 11 });
            location.Add(9, new double[] { -77.28, -57.437, 30.53 });
            location.Add(10, new double[] { 124.63, 2.5, 61.25 });
            location.Add(11, new double[] { 125.66, -1.72, 14.09 });
            location.Add(12, new double[] { 104.97, -6.53, -4.41 });
            location.Add(13, new double[] { 19.66, 26.84, -38.09 });
            location.Add(14, new double[] { 85.16, -56.31, 40.34 });
            location.Add(15, new double[] { 49.53, 15.75, -91.72 });
            location.Add(16, new double[] { -36.47, 16.16, -34.94 });
            location.Add(17, new double[] { 81.63, -94.88, -58.56 });
            location.Add(18, new double[] { 67.88, -21.5, 51.16 });
            location.Add(19, new double[] { 5.19, 84.53, -16.75 });
            location.Add(20, new double[] { 26.28, -51.75, 4.63 });
            location.Add(21, new double[] { 20.75, -82.25, 33.59 });
            location.Add(22, new double[] { -40.53, 4.18, -124.25 });
            location.Add(23, new double[] { -12.16, 62.63, 29.72 });
            location.Add(24, new double[] { 53.91, -130.69, 14.66 });
            location.Add(25, new double[] { 137.31, 3.84, -35.91 });
            location.Add(26, new double[] { 72.16, 48.75, 70.75 });
            location.Add(27, new double[] { -29.66, 32.69, 104.84 });
            location.Add(28, new double[] { 3.13, -8.88, 7.13 });
            location.Add(29, new double[] { -22.84, 36.53, -1.19 });
            location.Add(30, new double[] { 120.78, -247.19, -16.44 });
            location.Add(31, new double[] { -107.88, 29.56, -20.94 });
            location.Add(32, new double[] { -30.03, 72.34, -23.81 });
            location.Add(33, new double[] { -6.03, -30.38, -59.03 });
            location.Add(34, new double[] { 22.5, 23.78, 171.06 });
            location.Add(35, new double[] { -12.09, -16, -14.22 });
            location.Add(36, new double[] { -7.03, -12.88, -56.38 });
            location.Add(37, new double[] { 150.88, -173.78, 25.28 });
            location.Add(38, new double[] { -142.03, -13.34, -43.91 });
            location.Add(39, new double[] { -88.75, -76.75, -39.63 });
            location.Add(40, new double[] { -56, -25.13, -44.28 });
            location.Add(41, new double[] { -76.97, 71.91, 69.19 });
            location.Add(42, new double[] { -23.19, 80.03, 61.84 });
            location.Add(43, new double[] { -45.78, -93, -83.91 });
            location.Add(44, new double[] { -8.16, 74.81, -105.13 });
            location.Add(45, new double[] { 122.97, -9.06, 69 });
            location.Add(46, new double[] { -101.59, 93.78, 9.91 });
            location.Add(47, new double[] { 58.69, -170.97, -41.97 });
            location.Add(48, new double[] { 58.53, -55.81, 91.25 });
            location.Add(49, new double[] { 140.72, -96.97, 67.78 });
            location.Add(50, new double[] { 39.53, 21.28, 56.63 });
            location.Add(51, new double[] { 157.53, -110.53, 28.25 });
            location.Add(52, new double[] { -11.03, -79.22, -92.31 });
            location.Add(53, new double[] { -105.38, -73.34, 27.75 });
            location.Add(54, new double[] { 5.53, -183.41, 63.84 });
            location.Add(55, new double[] { -123.88, -81.59, 45.19 });
            location.Add(56, new double[] { 12.47, -66.72, -22.91 });
            location.Add(57, new double[] { -125.59, 44.03, 78.41 });
            location.Add(58, new double[] { 134.03, -163.59, 71.06 });
            location.Add(59, new double[] { -67.41, -7.41, 150.06 });
            location.Add(60, new double[] { -104.16, 82.72, -32.38 });
            location.Add(61, new double[] { 81.03, 52.84, 31.5 });
            location.Add(62, new double[] { 75.75, 48.75, 70.75 });
            location.Add(63, new double[] { -28.72, -35.72, -61.44 });
            location.Add(64, new double[] { 72.75, 48.75, 68.25 });
            location.Add(65, new double[] { 72.75, 48.75, 68.25 });
            location.Add(66, new double[] { -45.47, 18.56, 12.59 });
            location.Add(67, new double[] { 9.53, 59.31, -13.22 });
            location.Add(68, new double[] { 67.03, 39.59, -70.09 });
            location.Add(69, new double[] { 12.78, 4.81, 39.34 });
            location.Add(70, new double[] { 73.44, 58.34, -0.22 });
            location.Add(71, new double[] { -34.94, -44.16, -77.34 });
            location.Add(72, new double[] { -147.47, -64.13, 46.09 });
            location.Add(73, new double[] { 6.25, -5.78, 35.94 });
            location.Add(74, new double[] { 75.44, 2.63, -30.88 });
            location.Add(75, new double[] { 62.75, -74.19, 109.5 });
            location.Add(76, new double[] { -122.78, -102.53, -22.56 });
            location.Add(77, new double[] { 169.5, -42.34, 87.06 });
            location.Add(78, new double[] { 8.88, -205.44, 64.38 });
            location.Add(79, new double[] { -139.06, -2.31, -6.66 });
            location.Add(80, new double[] { 68.84, 48.75, 76.75 });
            location.Add(81, new double[] { 44.28, -82.97, 52.5 });
            location.Add(82, new double[] { 52.09, -112.09, -40.78 });
            location.Add(83, new double[] { -96.94, 143.44, 4.63 });
            location.Add(84, new double[] { 94.16, -167.38, -23.19 });
            location.Add(85, new double[] { -64.75, 57.22, -82.63 });
            location.Add(86, new double[] { 153.44, 61.44, 79.03 });
            location.Add(87, new double[] { 55.72, 17.59, 27.16 });
            location.Add(88, new double[] { 1.69, -77.88, -30.09 });
            location.Add(89, new double[] { -13.13, -81, -20.28 });
            location.Add(90, new double[] { -49.75, -19.03, -45 });
            location.Add(91, new double[] { -90.69, 8.13, -79.53 });
            location.Add(92, new double[] { 10.5, -34.78, 25.06 });
            location.Add(93, new double[] { 155.09, -12.41, 61.06 });
            location.Add(94, new double[] { 68.84, 48.75, 74.75 });
            location.Add(95, new double[] { -0.16, -102.75, -5.56 });
            location.Add(96, new double[] { -95.22, 9.88, -74.13 });
            location.Add(97, new double[] { -44.94, 36.94, 13.47 });
            location.Add(98, new double[] { -88.5, -12.31, 98.84 });
            location.Add(99, new double[] { -21.91, 8.13, 9 });
            location.Add(100, new double[] { -50.94, 90.22, 65.81 });
            location.Add(101, new double[] { 36.53, 100.06, -135.72 });
            location.Add(102, new double[] { 145.13, -140.06, 131.97 });
            location.Add(103, new double[] { -13.25, -52.06, -65.88 });
            location.Add(104, new double[] { -126.09, -83.09, -85.56 });
            location.Add(105, new double[] { 68.31, -190.03, 12.38 });
            location.Add(106, new double[] { -19.81, -35.38, 34 });
            location.Add(107, new double[] { 4.94, -175.91, -0.91 });
            location.Add(108, new double[] { 80.75, 48.75, 69.25 });
            location.Add(109, new double[] { -19.81, -3.25, -14.28 });

            starSystem.Add(0, "39 Tauri");
            starSystem.Add(1, "47 Ceti");
            starSystem.Add(2, "47 Ceti");
            starSystem.Add(3, "Aegaeon");
            starSystem.Add(4, "Aerial");
            starSystem.Add(5, "Aganippe");
            starSystem.Add(6, "Alacarakmo");
            starSystem.Add(7, "Alpha Centauri");
            starSystem.Add(8, "Altair");
            starSystem.Add(9, "Alya");
            starSystem.Add(10, "Anduliga");
            starSystem.Add(11, "Any Na");
            starSystem.Add(12, "Arouca");
            starSystem.Add(13, "AZ Cancri");
            starSystem.Add(14, "Baltah'Sine");
            starSystem.Add(15, "Banki");
            starSystem.Add(16, "Bast");
            starSystem.Add(17, "Belalans");
            starSystem.Add(18, "CD-75 661");
            starSystem.Add(19, "Cherbones");
            starSystem.Add(20, "Chi Eridani");
            starSystem.Add(21, "Coquim");
            starSystem.Add(22, "Damna");
            starSystem.Add(23, "Dea Motrona");
            starSystem.Add(24, "Delta Phoenicis");
            starSystem.Add(25, "Deuringas");
            starSystem.Add(26, "Diso");
            starSystem.Add(27, "Eleu");
            starSystem.Add(28, "Epsilon Indi");
            starSystem.Add(29, "Eranin");
            starSystem.Add(30, "Eshu");
            starSystem.Add(31, "Esuseku");
            starSystem.Add(32, "Ethgreze");
            starSystem.Add(33, "Fujin");
            starSystem.Add(34, "Geawen");
            starSystem.Add(35, "George Pantazis");
            starSystem.Add(36, "Geras");
            starSystem.Add(37, "Goman");
            starSystem.Add(38, "Haiden");
            starSystem.Add(39, "Havasupai");
            starSystem.Add(40, "Hecate");
            starSystem.Add(41, "Heike");
            starSystem.Add(42, "Helvetitj");
            starSystem.Add(43, "HIP 10175");
            starSystem.Add(44, "HIP 41181");
            starSystem.Add(45, "HIP 59533");
            starSystem.Add(46, "HIP 80364");
            starSystem.Add(47, "Holva");
            starSystem.Add(48, "HR 7221");
            starSystem.Add(49, "Irukama");
            starSystem.Add(50, "Jaradharre");
            starSystem.Add(51, "Jaroua");
            starSystem.Add(52, "Jotun (permit)");
            starSystem.Add(53, "Kachirigin");
            starSystem.Add(54, "Kamitra");
            starSystem.Add(55, "Kamorin");
            starSystem.Add(56, "Kappa Fornacis");
            starSystem.Add(57, "Karetii");
            starSystem.Add(58, "Karsuki Ti");
            starSystem.Add(59, "Kinago");
            starSystem.Add(60, "Kongga");
            starSystem.Add(61, "Korro Kung");
            starSystem.Add(62, "Lave");
            starSystem.Add(63, "LDS 883");
            starSystem.Add(64, "Leesti");
            starSystem.Add(65, "Leesti");
            starSystem.Add(66, "LFT 1421");
            starSystem.Add(67, "LP 375-25");
            starSystem.Add(68, "Mechucos");
            starSystem.Add(69, "Medb");
            starSystem.Add(70, "Mokojing");
            starSystem.Add(71, "Momus Reach");
            starSystem.Add(72, "Mukusubii");
            starSystem.Add(73, "Mulachi");
            starSystem.Add(74, "Neritus");
            starSystem.Add(75, "Ngadandari");
            starSystem.Add(76, "Nguna");
            starSystem.Add(77, "Ngurii");
            starSystem.Add(78, "Njangari");
            starSystem.Add(79, "Ochoeng");
            starSystem.Add(80, "Orrere");
            starSystem.Add(81, "Phiagre");
            starSystem.Add(82, "Quechua");
            starSystem.Add(83, "Rajukru");
            starSystem.Add(84, "Rapa Bao");
            starSystem.Add(85, "Rusani");
            starSystem.Add(86, "Sanuma");
            starSystem.Add(87, "Shinrarta Dezhra");
            starSystem.Add(88, "Tanmark");
            starSystem.Add(89, "Tarach Tor");
            starSystem.Add(90, "Terra Mater");
            starSystem.Add(91, "Thrutis");
            starSystem.Add(92, "Tiolce");
            starSystem.Add(93, "Toxandji");
            starSystem.Add(94, "Uszaa");
            starSystem.Add(95, "Utgaroar");
            starSystem.Add(96, "Uzumoku");
            starSystem.Add(97, "V1090 Herculis");
            starSystem.Add(98, "Vanayequi");
            starSystem.Add(99, "Vega");
            starSystem.Add(100, "Vidavanta");
            starSystem.Add(101, "Volkhab");
            starSystem.Add(102, "Wheemete");
            starSystem.Add(103, "Witchhaul");
            starSystem.Add(104, "Wulpa");
            starSystem.Add(105, "Wuthielo Ku");
            starSystem.Add(106, "Xihe");
            starSystem.Add(107, "Yaso Kondi");
            starSystem.Add(108, "Zaonce");
            starSystem.Add(109, "Zeessze");

            station.Add(0, "Porta");
            station.Add(1, "Glushko Station");
            station.Add(2, "Kaufmanis Hub");
            station.Add(3, "Schweikart Station");
            station.Add(4, "Andrade Legacy");
            station.Add(5, "Julian Market");
            station.Add(6, "Weyl Gateway");
            station.Add(7, "Hutton Oribtal");
            station.Add(8, "Solo Orbiter");
            station.Add(9, "Malaspina Gateway");
            station.Add(10, "Celsius Estate");
            station.Add(11, "Libby Orbital");
            station.Add(12, "Shipton Orbital");
            station.Add(13, "Fisher Station");
            station.Add(14, "Baltha'sine Station");
            station.Add(15, "Antonio De Andrade Vista");
            station.Add(16, "Hart Station");
            station.Add(17, "Boscovich Ring");
            station.Add(18, "Kirk Dock");
            station.Add(19, "Chalker Landing");
            station.Add(20, "Steve Masters station");
            station.Add(21, "Hirayama Installation");
            station.Add(22, "Nemere Market");
            station.Add(23, "Pinzon Dock");
            station.Add(24, "Trading post");
            station.Add(25, "Shukor Hub");
            station.Add(26, "Shifnalport");
            station.Add(27, "Finney Dock");
            station.Add(28, "Mansfield Orbiter");
            station.Add(29, "Azeban City");
            station.Add(30, "Shajn Terminal");
            station.Add(31, "Savinykh Orbital");
            station.Add(32, "Bloch Station");
            station.Add(33, "Futen Spaceport");
            station.Add(34, "Obruchev Legacy");
            station.Add(35, "Zamka Platform");
            station.Add(36, "Yurchikhin Port");
            station.Add(37, "Gustav Sporer Port");
            station.Add(38, "Searfoss Enterprise");
            station.Add(39, "Lovelace Port");
            station.Add(40, "RJH1972");
            station.Add(41, "Brunel City");
            station.Add(42, "Friend Orbital");
            station.Add(43, "Stefanyshyn-Piper Station");
            station.Add(44, "Andersson Station");
            station.Add(45, "Burnham Beacon");
            station.Add(46, "Stasheff Colony");
            station.Add(47, "Kreutz Orbital");
            station.Add(48, "Veron City");
            station.Add(49, "Blaauw City");
            station.Add(50, "Gohar Station");
            station.Add(51, "Mccool City");
            station.Add(52, "Icelock");
            station.Add(53, "Nowak Orbital");
            station.Add(54, "Hammel Terminal");
            station.Add(55, "Godwin Vision");
            station.Add(56, "Harvestport");
            station.Add(57, "Sinclair Platform");
            station.Add(58, "West Market");
            station.Add(59, "Fozard Ring");
            station.Add(60, "Laplace Ring");
            station.Add(61, "Lonchakov Orbital");
            station.Add(62, "Lave Station");
            station.Add(63, "Smith Reserve");
            station.Add(64, "George Lucas");
            station.Add(65, "George Lucas");
            station.Add(66, "Ehrlich Orbital");
            station.Add(67, "King Gateway");
            station.Add(68, "Brandenstein Port");
            station.Add(69, "Vela dock");
            station.Add(70, "Noli Terminal");
            station.Add(71, "Tartarus Point");
            station.Add(72, "Ledyard Dock");
            station.Add(73, "Clark Terminal");
            station.Add(74, "Toll Ring");
            station.Add(75, "Consolmagno Horizons");
            station.Add(76, "Biggle Hub");
            station.Add(77, "Cheranovsky City");
            station.Add(78, "Lee Hub");
            station.Add(79, "Roddenberry Gateway");
            station.Add(80, "Sharon Lee Free Market");
            station.Add(81, "Greeboski's Outpost");
            station.Add(82, "Crown Ring");
            station.Add(83, "Snyder Terminal");
            station.Add(84, "Flagg Gateway");
            station.Add(85, "Fernandes Market");
            station.Add(86, "Dunyach Gateway");
            station.Add(87, "Jameson Memorial");
            station.Add(88, "Cassie-L-Peia");
            station.Add(89, "Tranquillity");
            station.Add(90, "Gr8minds");
            station.Add(91, "Kingsbury Dock");
            station.Add(92, "Gordon Terminal");
            station.Add(93, "Tsunenaga Orbital");
            station.Add(94, "Guest Installation");
            station.Add(95, "Fort Klarix");
            station.Add(96, "Sevrdup Ring");
            station.Add(97, "Kaku Plant");
            station.Add(98, "Clauss Hub");
            station.Add(99, "Taylor City");
            station.Add(100, "Lee Mines");
            station.Add(101, "Vernadsky Dock");
            station.Add(102, "Eisinga Enterprise");
            station.Add(103, "Hornby Terminal");
            station.Add(104, "Williams Gateway");
            station.Add(105, "Tarter Dock");
            station.Add(106, "Zhen Dock");
            station.Add(107, "Wheeler Market");
            station.Add(108, "Ridley Scott");
            station.Add(109, "Nicollier Hanger");

            stationDistance.Add(0, 991);
            stationDistance.Add(1, 600000);
            stationDistance.Add(2, 600000);
            stationDistance.Add(3, 11655);
            stationDistance.Add(4, 180);
            stationDistance.Add(5, 133);
            stationDistance.Add(6, 16);
            stationDistance.Add(7, 6942672);
            stationDistance.Add(8, 661);
            stationDistance.Add(9, 540000);
            stationDistance.Add(10, 626220);
            stationDistance.Add(11, 581);
            stationDistance.Add(12, 410);
            stationDistance.Add(13, 16);
            stationDistance.Add(14, 356);
            stationDistance.Add(15, 531869);
            stationDistance.Add(16, 202);
            stationDistance.Add(17, 78);
            stationDistance.Add(18, 339);
            stationDistance.Add(19, 576740);
            stationDistance.Add(20, 2181);
            stationDistance.Add(21, 606);
            stationDistance.Add(22, 2532);
            stationDistance.Add(23, 1700000);
            stationDistance.Add(24, 3799);
            stationDistance.Add(25, 798);
            stationDistance.Add(26, 284);
            stationDistance.Add(27, 333);
            stationDistance.Add(28, 143);
            stationDistance.Add(29, 297);
            stationDistance.Add(30, 1742);
            stationDistance.Add(31, 276);
            stationDistance.Add(32, 351);
            stationDistance.Add(33, 559);
            stationDistance.Add(34, 426261);
            stationDistance.Add(35, 45);
            stationDistance.Add(36, 1052931);
            stationDistance.Add(37, 291);
            stationDistance.Add(38, 561080);
            stationDistance.Add(39, 601314);
            stationDistance.Add(40, 570);
            stationDistance.Add(41, 406);
            stationDistance.Add(42, 444);
            stationDistance.Add(43, 4850);
            stationDistance.Add(44, 536714);
            stationDistance.Add(45, 529600);
            stationDistance.Add(46, 2632);
            stationDistance.Add(47, 22000);
            stationDistance.Add(48, 1886);
            stationDistance.Add(49, 323);
            stationDistance.Add(50, 526178);
            stationDistance.Add(51, 143);
            stationDistance.Add(52, 80);
            stationDistance.Add(53, 340);
            stationDistance.Add(54, 82);
            stationDistance.Add(55, 445);
            stationDistance.Add(56, 919);
            stationDistance.Add(57, 990);
            stationDistance.Add(58, 28);
            stationDistance.Add(59, 1723);
            stationDistance.Add(60, 254);
            stationDistance.Add(61, 622040);
            stationDistance.Add(62, 302);
            stationDistance.Add(63, 54480);
            stationDistance.Add(64, 257);
            stationDistance.Add(65, 257);
            stationDistance.Add(66, 773000);
            stationDistance.Add(67, 514700);
            stationDistance.Add(68, 534137);
            stationDistance.Add(69, 1686844);
            stationDistance.Add(70, 539400);
            stationDistance.Add(71, 413);
            stationDistance.Add(72, 505430);
            stationDistance.Add(73, 594550);
            stationDistance.Add(74, 524);
            stationDistance.Add(75, 502357);
            stationDistance.Add(76, 1870);
            stationDistance.Add(77, 1265);
            stationDistance.Add(78, 21400);
            stationDistance.Add(79, 484);
            stationDistance.Add(80, 963);
            stationDistance.Add(81, 85);
            stationDistance.Add(82, 120);
            stationDistance.Add(83, 117);
            stationDistance.Add(84, 2338);
            stationDistance.Add(85, 72);
            stationDistance.Add(86, 6000);
            stationDistance.Add(87, 347);
            stationDistance.Add(88, 414);
            stationDistance.Add(89, 359);
            stationDistance.Add(90, 504);
            stationDistance.Add(91, 288);
            stationDistance.Add(92, 158);
            stationDistance.Add(93, 7000);
            stationDistance.Add(94, 4350);
            stationDistance.Add(95, 171);
            stationDistance.Add(96, 505300);
            stationDistance.Add(97, 1233592);
            stationDistance.Add(98, 81);
            stationDistance.Add(99, 1100);
            stationDistance.Add(100, 556882);
            stationDistance.Add(101, 408);
            stationDistance.Add(102, 551628);
            stationDistance.Add(103, 219);
            stationDistance.Add(104, 28);
            stationDistance.Add(105, 145);
            stationDistance.Add(106, 1965);
            stationDistance.Add(107, 114);
            stationDistance.Add(108, 400);
            stationDistance.Add(109, 489);


            goods.Add(0, "Tauri Chimes");
            goods.Add(1, "Aepyornis Egg");
            goods.Add(2, "Ceti Rabbits");
            goods.Add(3, "Chateau De Aegaeon");
            goods.Add(4, "Edan Apples of Aerial");
            goods.Add(5, "Aganippe Rush");
            goods.Add(6, "Alacarakmo Skin Art");
            goods.Add(7, "Centauri Mega Gin");
            goods.Add(8, "Altairian Skin");
            goods.Add(9, "Alya Body Soup");
            goods.Add(10, "Anduliga Fire Works");
            goods.Add(11, "Any Na Coffee");
            goods.Add(12, "Arouca Conventula Sweets");
            goods.Add(13, "Az Cancri Formula 42");
            goods.Add(14, "Baltha'sine Vacuum Krill");
            goods.Add(15, "Banki Amphibious Leather");
            goods.Add(16, "Bast Snake Gin");
            goods.Add(17, "Belalans Ray Leather");
            goods.Add(18, "CD-75 Kitten Brand Coffee");
            goods.Add(19, "Cherbones Blood Crystals");
            goods.Add(20, "Chi Eridani Marine Paste");
            goods.Add(21, "Coquim Spongiform Victuals");
            goods.Add(22, "Damna Carapaces");
            goods.Add(23, "Motrona Experience Jelly");
            goods.Add(24, "Delta Phoenicis Palms");
            goods.Add(25, "Deuringas Truffles");
            goods.Add(26, "Diso Ma Corn");
            goods.Add(27, "Eleu Thermals");
            goods.Add(28, "Indi Bourbon ");
            goods.Add(29, "Eranin Whiskey");
            goods.Add(30, "Eshu Umbrellas");
            goods.Add(31, "Esuseku Caviar");
            goods.Add(32, "Ethgreze Tea Buds");
            goods.Add(33, "Fujin Tea");
            goods.Add(34, "Geawen Dance Dust");
            goods.Add(35, "Pantaa Prayer Sticks");
            goods.Add(36, "Gerasian Gueuze Beer");
            goods.Add(37, "Goman Yaupon Coffee");
            goods.Add(38, "Haidne Black Brew");
            goods.Add(39, "Havasupai Dream Catcher");
            goods.Add(40, "Live Hecate Sea Worms");
            goods.Add(41, "Ceremonial Heike Tea");
            goods.Add(42, "Helvetitj Pearls");
            goods.Add(43, "HIP 10175 Bush Meat");
            goods.Add(44, "HIP Proto-Squid");
            goods.Add(45, "Burnham Bile Distillate");
            goods.Add(46, "HIP Organophospates");
            goods.Add(47, "Holva Duelling Blades");
            goods.Add(48, "HR 7221 Wheat");
            goods.Add(49, "Giant Irukama Snails");
            goods.Add(50, "Jaradharre Puzzle Box");
            goods.Add(51, "Jaroua Rice");
            goods.Add(52, "Jotun Mookah");
            goods.Add(53, "Kachirigin Filter Leeches");
            goods.Add(54, "Kamitra Cigars");
            goods.Add(55, "Kamorin Historic Weapons");
            goods.Add(56, "Onion Head");
            goods.Add(57, "Karetii Couture");
            goods.Add(58, "Karsuki Locusts");
            goods.Add(59, "Kinago Violins");
            goods.Add(60, "Kongga Ale");
            goods.Add(61, "Koro Kung Pellets");
            goods.Add(62, "Lavian Brandy ");
            goods.Add(63, "Chameleon Cloth");
            goods.Add(64, "Leestian Evil Juice");
            goods.Add(65, "Azure Milk");
            goods.Add(66, "Void Extract Coffee");
            goods.Add(67, "Honesty Pills");
            goods.Add(68, "Mechucos High Tea");
            goods.Add(69, "Medb starlube");
            goods.Add(70, "Mokojing Beast Feast");
            goods.Add(71, "Momus Bog Spaniel");
            goods.Add(72, "Mukusubii Chitin-os");
            goods.Add(73, "Mulachi Giant Fungus");
            goods.Add(74, "Neritus Berries");
            goods.Add(75, "Ngandandari Fire Opals");
            goods.Add(76, "Nguna Modern Antiques");
            goods.Add(77, "Soontill Relics");
            goods.Add(78, "Njangari Saddles");
            goods.Add(79, "Ochoeng Chillies");
            goods.Add(80, "Orrerian Vicious Brew");
            goods.Add(81, "Giant Verrix");
            goods.Add(82, "Albino Quechua Mammoth Meat");
            goods.Add(83, "Rajukru Multi-Stoves");
            goods.Add(84, "Rapa Bao Snake Skins");
            goods.Add(85, "Rusani Old Smokey");
            goods.Add(86, "Sanuma Decorative Meat");
            goods.Add(87, "Waters of Shintara");
            goods.Add(88, "Tanmark Tranquil Tea");
            goods.Add(89, "Tarach Spice");
            goods.Add(90, "Terra Mater Blood Bores");
            goods.Add(91, "Thrutis Cream");
            goods.Add(92, "Tiolce Waste 2 Paste");
            goods.Add(93, "Toxandji Virocide");
            goods.Add(94, "Uszaian Tree Grub");
            goods.Add(95, "Utgaroar Millennial Eggs");
            goods.Add(96, "Uzumoku Low-G Wings");
            goods.Add(97, "V Herculis Body Rub");
            goods.Add(98, "Vanayequi Ceratomorpha Fur");
            goods.Add(99, "Vega Slimweed");
            goods.Add(100, "Vidavantian Lace");
            goods.Add(101, "Volkhab Bee Drones");
            goods.Add(102, "Wheemete Wheat Cakes");
            goods.Add(103, "Witchhaul Kobe Beef ");
            goods.Add(104, "Wulpa Hyperbore Systems");
            goods.Add(105, "Wuthielo Ku Froth");
            goods.Add(106, "Xihe Biomorphic Companions");
            goods.Add(107, "Yaso Kondi Leaf");
            goods.Add(108, "Leathery Eggs");
            goods.Add(109, "Zeessze Ant Grub Glue");

            jumpList[0] = -1;

            var starList = new List<SystemData>();
            
        }

        private void setDistance()
        {
            double startX = 0;
            double startY = 0;
            double startZ = 0;

            Dictionary<int, double> tempRotation = new Dictionary<int, double>();

            foreach (var startLoc in location)
            {
                double[] start = startLoc.Value;

                startX = start[0];
                startY = start[1];
                startZ = start[2];

                Dictionary<int, double> tempDistance = new Dictionary<int, double>();

                double endX = 0;
                double endY = 0;
                double endZ = 0;

                foreach (var endLoc in location)
                {
                    double[] end = endLoc.Value;

                    endX = end[0];
                    endY = end[1];
                    endZ = end[2];

                    tempDistance[endLoc.Key] = calculateDistance(startX, startY, startZ, endX, endY, endZ);
                    
                }

                var sortedDistance = from pair in tempDistance orderby pair.Value ascending select pair;

                int keyCount = 0;

                int[] sortedKeys = new int[location.Count];
                double[] sortedValues = new double[location.Count];
 
                foreach (KeyValuePair<int,double> pair in sortedDistance)
                {
                    sortedKeys[keyCount] = pair.Key;
                    sortedValues[keyCount] = pair.Value;

                    keyCount += 1;
                }

                distance[startLoc.Key] = sortedValues;
                distanceOrder[startLoc.Key] = sortedKeys;

                int[] locationKey = sortedKeys;
                double[] distanceNames = sortedValues;
                
                string[] systemNames = new string[locationKey.Length];

                for (int step=0;step<locationKey.Length;step+=1)

                {

                    int systemID = locationKey[step];

                    string systemName;
                    double distanceString = distanceNames[step];
                    double rotationString = Rotation[systemID];
                    starSystem.TryGetValue(systemID , out systemName);
                    systemNames[step] = systemName+"\n["+distanceString.ToString("##.##")+" : "+rotationString.ToString("##.##")+"]";

                }
                
                distanceSystem[startLoc.Key] = systemNames;

            }

        }

        private void setRotation()
        {
            double startX = 0;
            double startY = 0;
            double startZ = 0;

            

            foreach (var startLoc in location)
            {
                double[] start = startLoc.Value;

                startX = start[0];
                startY = start[1];
                startZ = start[2];

                double endX = 0;
                double endY = 0;
                double endZ = 0;

                foreach (var endLoc in location)
                {
                    double[] end = endLoc.Value;

                    endX = end[0];
                    endY = end[1];
                    endZ = end[2];

                    Rotation[endLoc.Key] = calculateRotation(startX, startY, startZ, endX, endY, endZ);

                }

            }

        }

        private void CreateView()
        {

            List<SystemData> masterList = new List<SystemData>();

            ListViewItem masterItem = new ListViewItem();

            foreach (var systemName in starSystem)
            {
                starSystemList.Add(systemName.Value);
            }

            foreach (var stationName in station)
            {
                stationList.Add(stationName.Value);
            }

            foreach (var systemNames in distanceSystem)
            {
                distanceSystemList.Add(systemNames.Value);
            }

            foreach (var distances in distance)
            {
                distanceList.Add(distances.Value);
            }

            foreach (var distanceSys in distanceSystem)
            {
                distanceSystemList.Add(distanceSys.Value);
            }

            GridView starGridView = new GridView();
            GridViewColumn idName = new GridViewColumn();
            GridViewColumn sysName = new GridViewColumn();
            GridViewColumn statName = new GridViewColumn();

            idName.Header = "Star System";
            idName.Width = 25;
            idName.DisplayMemberBinding = new Binding("systemID");

            sysName.Header = "Star System";
            sysName.Width = 150;
            sysName.DisplayMemberBinding = new Binding("systemName");

            statName.Header = "Station Name";
            statName.Width = 150;
            statName.DisplayMemberBinding = new Binding("stationName");

            starGridView.Columns.Add(idName);
            starGridView.Columns.Add(sysName);
            starGridView.Columns.Add(statName);

            for (int col = 0; col < distanceSystem.Count; col += 1)
            {
            
                GridViewColumn target = new GridViewColumn();

                target.Header = "["+col+"]";
                target.Width = 100;
                target.DisplayMemberBinding = new Binding("distanceSystem[" + col + "]");

               if(col>0) { starGridView.Columns.Add(target);}

        }

        stars.View = starGridView;

            int count = 0;

            foreach (string system in starSystemList)
            {
                string station;
                double[] distant;
                string[] distantSys;

                station = stationList[count];
                distant = distanceList[count];
                distantSys = distanceSystemList[count];
                
                stars.Items.Add(new SystemData() {systemID=count,systemName = system, stationName = station, distanceSystem = distantSys});

                count += 1;
            }

        }

        private void CalculateRoute()
        {


            for (int i = 0; i < soldList.Length; i += 1)
            {
                soldList[i] = -1;
            }

            currentSystemID = 0;

            Jump(currentSystemID,"25");
            route.Text += "----------------\n";
            Jump(currentSystemID,"50");
            route.Text += "----------------\n";
            Jump(currentSystemID, "100");
            route.Text += "----------------\n";
            Jump(currentSystemID, "25");
            route.Text += "----------------\n";
            Jump(currentSystemID, "50");
            route.Text += "----------------\n";
            Jump(currentSystemID, "100");
            route.Text += "----------------\n";
            Jump(currentSystemID, "200");
            route.Text += "----------------\n";
            
        }

        private void Jump(int currentID,string jump)
        {
            double[] currentDistance = distanceList[currentID];
            int[] currentOrderList = distanceOrder[currentID];
            string currentStation = stationList[currentID];
            string currentSystem = starSystem[currentID];
            int currentStatDistance = stationDistance[currentID];

            int maxStep = 0;
            int jumpRange = 0;

            if (jump == "25")
            {
                maxStep = starSystem.Count;
                jumpRange = 25;
            }

            if (jump == "50")
            {
                maxStep = starSystem.Count;
                jumpRange = 50;
            }

            if (jump == "100")
            {
                maxStep = 2;
                jumpRange = 100;
            }

            if (jump == "200")
            {
                maxStep = 2;
                jumpRange = 200;
            }

            double totalDistance = 0;

            if (jumpList[0] == -1) { jumpList[0] = currentID; }

            for (int steps = 1; steps < maxStep; steps += 1)
            {

                int count = 0;

                while (count < starSystem.Count)
                {
                    int nextStopID = currentOrderList[count];

                    double nextDistance = currentDistance[count];

                    bool jumpCheck = false;

                    if (jumpRange == 25) jumpCheck = nextDistance <= 25;
                    if (jumpRange == 50) jumpCheck = nextDistance <= 50;
                    if (jumpRange == 100) jumpCheck = nextDistance >= 100;
                    if (jumpRange == 200) jumpCheck = nextDistance >= 200;

                    if (!jumpList.Contains(nextStopID) && jumpCheck && stationDistance[nextStopID] < 6000)
                    {
                        currentSystemID = nextStopID;

                        currentOrderList = distanceOrder[currentSystemID];
                        currentDistance = distanceList[currentSystemID];
                        currentSystem = starSystem[currentSystemID];
                        currentStation = stationList[currentSystemID];
                        currentStatDistance = stationDistance[currentSystemID];

                        totalDistance += nextDistance;

                        route.Text +=jumpListCounter+ ": " 
                                    + nextDistance.ToString("##.#") + "  [" 
                                    + totalDistance.ToString("##.#") + "] "                 
                                    + currentSystem + ":  " 
                                    + currentStation + " ( " 
                                    + currentStatDistance + ") -> " +sellCheck(currentSystemID) 
                                    +"\n";

                        jumpListCounter += 1;

                        jumpList[jumpListCounter] = currentSystemID;
                        soldList[jumpListCounter] = currentSystemID;

                        break;
                    }

                    count += 1;

                }

            }

        }

        private string sellCheck(int currentID)
        {
            double[] currentDistance = distanceList[currentID];
            int[] currentOrderList = distanceOrder[currentID];

            string sellItem = "";

            for (int counter = 0; counter < currentDistance.Length; counter += 1)
            {
                int fetch = currentOrderList[counter];

                if (currentDistance[counter] >= 160 && soldList.Contains(fetch))
                {
                    for (int lookup = 0; lookup < soldList.Length; lookup += 1)
                    {
                        if (soldList[lookup] == fetch)
                        {
                            double sellDistance = currentDistance[counter];

                            sellItem += starSystem[fetch] + "(" + sellDistance.ToString("##.##") + ") ,";

                            soldList[lookup] = -1;
                        }
                    }
                }
            }

             return sellItem;

        }

        private double calculateRotation(double startX, double startY, double startZ, double endX, double endY,double endZ)
        {
            double rotation = 0;

            double rad = 360/(Math.PI*2);

            double distanceX = 0;
            double distanceY = 0;
            double distanceZ = 0;

            if (startX < 0 && endX < 0) distanceX = Math.Min(startX, endX) + Math.Abs(Math.Max(startX, endX));
            if (startY < 0 && endY < 0) distanceY = Math.Min(startY, endY) + Math.Abs(Math.Max(startY, endY));
            if (startZ < 0 && endZ < 0) distanceZ = Math.Min(startZ, endZ) + Math.Abs(Math.Max(startZ, endZ));

            if ((startX < 0 && endX > 0) || (startX > 0 && endX < 0)) distanceX = Math.Abs(Math.Min(startX, endX)) + Math.Max(startX, endX);
            if ((startY < 0 && endY > 0) || (startY > 0 && endY < 0)) distanceY = Math.Abs(Math.Min(startY, endY)) + Math.Max(startY, endY);
            if ((startZ < 0 && endZ > 0) || (startZ > 0 && endZ < 0)) distanceZ = Math.Abs(Math.Min(startZ, endZ)) + Math.Max(startZ, endZ);

            rotation = (Math.Tan(distanceX / distanceZ)) * rad;

            return rotation;


        }

        private double calculateDistance(double startX, double startY, double startZ, double endX, double endY,double endZ)
        {
            double distance = 0;

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

            distance = Math.Pow(distanceX, 2) + Math.Pow(distanceZ, 2);
            distance = Math.Sqrt(distance);

            distance = Math.Pow(distance, 2) + Math.Pow(distanceY, 2);
            distance = Math.Sqrt(distance);

          // rotation = (Math.Tan(distance / distanceZ)) * rad;

            return distance;
        }


        public class SystemData
        {
            public int systemID { get; set; }
            public string systemName { get; set; }
            public string stationName { get; set; }
            public double[] location { get; set; }
            public string goods { get; set; }
            public double[] distance { get; set; }
            public int[] distanceOrder { get; set; }
            public string[] distanceSystem { get; set; }
            public List<string> StarSystemList { get; set; } 
            
        }
    }
}