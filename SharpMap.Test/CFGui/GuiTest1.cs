using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.Layers;
using SharpMap.Data.Providers.ShapeFile;

using NUnit.Framework;


namespace SharpMap.Test.CFGui
{
class GuiTest1
{

    //String shapesDirPDA = @"..\TestData\roads.shp";
    String shapesDirPDA = @"\Storage Card\TestData\rivers.shp";
    String shapesDirPC = @"..\..\..\TestData\roads.shp";

    Map myMap = new Map("Mi mapa 1");

    public GuiTest1()
    {
        
        ShapeFileProvider shpData = new ShapeFileProvider(shapesDirPC, true);
        //Assert.IsNotNull(shpData);
        shpData.Open();
        //Assert.IsTrue(!File.Exists(@"..\..\..\TestData\BCROADS.shp.sidx"));
      

        //GeometryLayer gLayer = new GeometryLayer("My layer", shpData);


        //myMap.Layers.Add(gLayer);




    }

/*
    public static void Main(String[] args)
    {
        GuiTest1 test = new GuiTest1();


        Console.WriteLine("Press enter to exit");
        Console.ReadLine();
    }
    */

}
}

