using System;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.ReportsV2;
using TestXafSolution2.Module.DatabaseUpdate;
using TestXafSolution2.Module.Reports;
using TestXafSolution2.Module.TestWork2;

namespace TestXafSolution2.Module.Win {
    [ToolboxItemFilter("Xaf.Platform.Win")]
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppModuleBasetopic.aspx.
    public sealed partial class TestXafSolution2WindowsFormsModule : ModuleBase {
        //private void Application_CreateCustomModelDifferenceStore(Object sender, CreateCustomModelDifferenceStoreEventArgs e) {
        //    e.Store = new ModelDifferenceDbStore((XafApplication)sender, typeof(ModelDifference), true, "Win");
        //    e.Handled = true;
        //}
        private void Application_CreateCustomUserModelDifferenceStore(Object sender, CreateCustomModelDifferenceStoreEventArgs e) {
            e.Store = new ModelDifferenceDbStore((XafApplication)sender, typeof(ModelDifference), false, "Win");
            e.Handled = true;
        }
        public TestXafSolution2WindowsFormsModule() {
            InitializeComponent();
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            var predefinedReportsUpdater = new PredefinedReportsUpdater(Application, objectSpace, versionFromDB);
            predefinedReportsUpdater.AddPredefinedReport<AreaReport>("Отчет по площадкам", typeof(Area));
            predefinedReportsUpdater.AddPredefinedReport<CargoReport>("Отчет по товарам", typeof(Cargo));
            return new ModuleUpdater[] { predefinedReportsUpdater };
            //return ModuleUpdater.EmptyModuleUpdaters;
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            //application.CreateCustomModelDifferenceStore += Application_CreateCustomModelDifferenceStore;
            application.CreateCustomUserModelDifferenceStore += Application_CreateCustomUserModelDifferenceStore;
            // Manage various aspects of the application UI and behavior at the module level.
        }
    }
}
