using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.DataManagementTools;

namespace ArcMapAddin1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IGxDialog gxd = new GxDialog();
            gxd.AllowMultiSelect = false;
            gxd.ButtonCaption = "Add";
            gxd.Title = "Add a shapefile";
            gxd.RememberLocation = true;

            IGxObjectFilter filter1 = new GxFilterFileGeodatabases();
            IGxObjectFilter filter2 = new GxFilterShapefiles();
            IGxObjectFilterCollection filters = gxd as IGxObjectFilterCollection;
            filters.AddFilter(filter1, true);
            filters.AddFilter(filter2, false);

            IEnumGxObject enumObj;
            if (gxd.DoModalOpen(ArcMap.Application.hWnd, out enumObj) == false) // show dialog
                return;                              // return if clicking on cancel
            IGxObject gxObj = enumObj.Next();
            int len1 = gxObj.FullName.Length;
            int len2 = gxObj.Name.Length;
            string shpPath = gxObj.FullName.Substring(0, len1 - len2);
            string shpPath2 = gxObj.FullName.Substring(0);

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
            IWorkspace ws = wsf.OpenFromFile(shpPath, 0);
            IFeatureWorkspace fws = ws as IFeatureWorkspace;
            IFeatureClass featureClass = fws.OpenFeatureClass(gxObj.BaseName);
            IFeatureLayer featureLayer = new FeatureLayer();
            featureLayer.FeatureClass = featureClass;
            ILayer layer = (ILayer)featureLayer;
            layer.Name = gxObj.BaseName;
            mxdoc.AddLayer(layer);
            mxdoc.ActiveView.Refresh();
            mxdoc.UpdateContents();

            textBox1.Text = shpPath2;
            points = shpPath2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IGxDialog gxd = new GxDialog();
            gxd.AllowMultiSelect = false;
            gxd.ButtonCaption = "Add";
            gxd.Title = "Add a shapefile";
            gxd.RememberLocation = true;

            IGxObjectFilter filter1 = new GxFilterFileGeodatabases();
            IGxObjectFilter filter2 = new GxFilterShapefiles();
            IGxObjectFilterCollection filters = gxd as IGxObjectFilterCollection;
            filters.AddFilter(filter1, true);
            filters.AddFilter(filter2, false);

            IEnumGxObject enumObj;
            if (gxd.DoModalOpen(ArcMap.Application.hWnd, out enumObj) == false) // show dialog
                return;                              // return if clicking on cancel
            IGxObject gxObj = enumObj.Next();
            int len1 = gxObj.FullName.Length;
            int len2 = gxObj.Name.Length;
            string shpPath = gxObj.FullName.Substring(0, len1 - len2);
            string shpPath2 = gxObj.FullName.Substring(0);

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
            IWorkspace ws = wsf.OpenFromFile(shpPath, 0);
            IFeatureWorkspace fws = ws as IFeatureWorkspace;
            IFeatureClass featureClass = fws.OpenFeatureClass(gxObj.BaseName);
            IFeatureLayer featureLayer = new FeatureLayer();
            featureLayer.FeatureClass = featureClass;
            ILayer layer = (ILayer)featureLayer;
            layer.Name = gxObj.BaseName;
            mxdoc.AddLayer(layer);
            mxdoc.ActiveView.Refresh();
            mxdoc.UpdateContents();

            textBox2.Text = shpPath2;
            tracts = shpPath2;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            IGxDialog gxd = new GxDialog();
            gxd.AllowMultiSelect = false;
            gxd.ButtonCaption = "Add";
            gxd.Title = "Add a shapefile";
            gxd.RememberLocation = true;

            IGxObjectFilter filter1 = new GxFilterFileGeodatabases();
            IGxObjectFilter filter2 = new GxFilterShapefiles();
            IGxObjectFilterCollection filters = gxd as IGxObjectFilterCollection;
            filters.AddFilter(filter1, true);
            filters.AddFilter(filter2, false);

            IEnumGxObject enumObj;
            if (gxd.DoModalOpen(ArcMap.Application.hWnd, out enumObj) == false) // show dialog
                return;                              // return if clicking on cancel
            IGxObject gxObj = enumObj.Next();
            int len1 = gxObj.FullName.Length;
            int len2 = gxObj.Name.Length;
            string shpPath = gxObj.FullName.Substring(0, len1 - len2);
            string shpPath2 = gxObj.FullName.Substring(0);

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
            IWorkspace ws = wsf.OpenFromFile(shpPath, 0);
            IFeatureWorkspace fws = ws as IFeatureWorkspace;
            IFeatureClass featureClass = fws.OpenFeatureClass(gxObj.BaseName);
            IFeatureLayer featureLayer = new FeatureLayer();
            featureLayer.FeatureClass = featureClass;
            ILayer layer = (ILayer)featureLayer;
            layer.Name = gxObj.BaseName;
            mxdoc.AddLayer(layer);
            mxdoc.ActiveView.Refresh();
            mxdoc.UpdateContents();

            textBox3.Text = shpPath2;
            county = shpPath2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }


        private void button4_Click(object sender, EventArgs e)
        {
            IGxDialog gxd = new GxDialog();
            gxd.AllowMultiSelect = false;
            gxd.ButtonCaption = "Add";
            gxd.Title = "Add a shapefile";
            gxd.RememberLocation = true;

            IGxObjectFilter filter1 = new GxFilterFileGeodatabases();
            IGxObjectFilter filter2 = new GxFilterTables();
            IGxObjectFilterCollection filters = gxd as IGxObjectFilterCollection;
            filters.AddFilter(filter1, true);
            filters.AddFilter(filter2, false);

            IEnumGxObject enumObj;
            if (gxd.DoModalOpen(ArcMap.Application.hWnd, out enumObj) == false) // show dialog
                return;                              // return if clicking on cancel
            IGxObject gxObj = enumObj.Next();
            int len1 = gxObj.FullName.Length;
            int len2 = gxObj.Name.Length;
            string shpPath = gxObj.FullName.Substring(0, len1 - len2);
            string shpPath2 = gxObj.FullName.Substring(0);

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            textBox4.Text = shpPath2;
            population = shpPath2;
        }

        ILayer GetLayerByName(IMap map, string layername)
        {
            IEnumLayer enumLayer = map.Layers;
            ILayer layer = enumLayer.Next();
            while (layer != null)
            {
                if (layer.Name == layername)
                {
                    return layer;
                }
                layer = enumLayer.Next();
            }
            return null;
        }


        IMap map = ((IMxDocument)ArcMap.Application.Document).FocusMap;

        string points { get; set; }
        string tracts { get; set; }
        string county { get; set; }
        string population { get; set; }
        string output { get; set; }


        private void button6_Click(object sender, EventArgs e)
        {
            Geoprocessor gp = new Geoprocessor();
            gp.AddOutputsToMap = true;
            gp.OverwriteOutput = true;
            CreateThiessenPolygons ct = new CreateThiessenPolygons();
            ILayer points = GetLayerByName(map ,"public_library_shp");
            IFeatureClass fc = (points as IFeatureLayer2).FeatureClass;
            ct.in_features = fc;
            ct.out_feature_class = output;
            ILayer bounds = GetLayerByName(map, "cty00_shp");
            IEnvelope envelope = bounds.AreaOfInterest.Envelope;
            string envelopestr = string.Format("{0} {1} {2} {3}",
                envelope.XMin, envelope.YMax, envelope.XMax, envelope.YMin);
            gp.SetEnvironmentValue("Extent", envelopestr);
            gp.Execute(ct, null);

            AddJoin joined = new AddJoin();
            joined.join_table = population;
            joined.in_layer_or_view = "trt00_shp";
            joined.in_field = "STFID2";
            joined.join_field = "GEO_ID";
            joined.out_layer_or_view = output + @"\joinedoutput";
            gp.Execute(joined, null);

            Intersect intersect = new Intersect();
            intersect.in_features = "marketshare" + ";" + tracts;
            intersect.join_attributes = "ALL";
            intersect.out_feature_class = output + @"\intersect";
            gp.Execute(intersect, null);

            AddField addedfield = new AddField();
            addedfield.in_table = "intersect";
            addedfield.field_name = "field";
            addedfield.field_type = "FLOAT";
            gp.Execute(addedfield, null);

            AddJoin joined2 = new AddJoin();
            joined2.join_table = population;
            joined2.in_layer_or_view = "intersect";
            joined2.in_field = "STFID2";
            joined2.join_field = "GEO_ID";
            joined2.out_layer_or_view = output + @"\joinedoutput";
            gp.Execute(joined2, null);

            CalculateField calc = new CalculateField();
            calc.in_table = "intersect";
            calc.field = "field";
            calc.expression = "!population.P001001!*!intersect.Shape_Area!/!intersect_Area!";
            gp.Execute(calc, null);

            Dissolve dissolved = new Dissolve();
            dissolved.in_features = intersect;
            dissolved.statistics_fields = "field";
            dissolved.out_feature_class = output + @"\dissolve";
            gp.Execute(dissolved, null);


        }



        private void button5_Click(object sender, EventArgs e)
        {
            IGxDialog gxd = new GxDialog();
            gxd.AllowMultiSelect = false;
            gxd.ButtonCaption = "Add";
            gxd.Title = "Add a GDB";
            gxd.RememberLocation = true;

            IGxObjectFilter filter1 = new GxFilterFileGeodatabases();
            gxd.ObjectFilter = filter1;

            IEnumGxObject enumObj;
            if (gxd.DoModalOpen(ArcMap.Application.hWnd, out enumObj) == false) // show dialog
                return;                              // return if clicking on cancel
            IGxObject gxObj = enumObj.Next();
            int len1 = gxObj.FullName.Length;
            int len2 = gxObj.Name.Length;
            string shpPath = gxObj.FullName.Substring(0);

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            textBox5.Text = shpPath;
            output = shpPath;

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
 