using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.DataSourcesFile;


namespace ArcMapAddin1
{
    public class marketshares : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public marketshares()
        {
        }

        protected override void OnClick()
        {

            Form1 forms = new Form1();
            forms.ShowDialog();
            //
            //  TODO: Sample code showing how to access button host
            //
            ArcMap.Application.CurrentTool = null;

        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
