using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SharpMap.Layers;
using SharpMap.Data.Providers.ShapeFile;


namespace SharpMap.Test {
    public partial class Form1 : Form {
        public Form1()
        {
            InitializeComponent();

            //debug   //
#if !DEBUG_PC
            OperatingSystem os = Environment.OSVersion;
            PlatformID pid = os.Platform;
            if (pid != PlatformID.WinCE) // && DEBUG_PC == false)
                System.Diagnostics.Trace.Assert(false, "Pon DEBUG_PC para emular en el PC");
#endif
            //end debug   // 

            GuiTest1();
            //System.Diagnostics.Trace.Assert(false, "hola");
        }


        //String shapesDirPDA = @"..\TestData\roads.shp";
#if DEBUG_PC
        String shapesDir = @"..\..\..\TestData\roads.shp";
#else
        String shapesDir = @"\Storage Card\TestData\rivers.shp";
#endif


        public void GuiTest1()
        {
            Map myMap = new Map("Mi mapa 1");

            ShapeFileProvider shpData = new ShapeFileProvider(shapesDir, false);
            //Assert.IsNotNull(shpData);
            shpData.Open();
            //Assert.IsTrue(!File.Exists(@"..\..\..\TestData\BCROADS.shp.sidx"));


            //GeometryLayer gLayer = new GeometryLayer("My layer", shpData);


            //myMap.Layers.Add(gLayer);




        }


    }
}