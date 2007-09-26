using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.Layers;
using SharpMap.Data.Providers.ShapeFile;

using NUnit.Framework;
using System.IO;

namespace SharpMap.Test.CFGui
{
class GuiTest1
{

    Map myMap = new Map("Mi mapa 1");

    public GuiTest1()
    {

        ShapeFileProvider shpData = new ShapeFileProvider(@"..\..\..\TestData\BCROADS.SHP", true);
        //Assert.IsNotNull(shpData);
        //shpData.Open();
        //Assert.IsTrue(!File.Exists(@"..\..\..\TestData\BCROADS.shp.sidx"));
      

        GeometryLayer gLayer = new GeometryLayer("My layer", shpData);


        myMap.Layers.Add(gLayer);




    }


    public static void Main(String[] args)
    {
        GuiTest1 test = new GuiTest1();


        Console.WriteLine("Press enter to exit");
        Console.ReadLine();
    }


}
}

