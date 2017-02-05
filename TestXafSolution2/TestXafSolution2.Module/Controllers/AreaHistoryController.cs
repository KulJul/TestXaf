using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using TestXafSolution2.Module.TestWork2;
using DevExpress.ExpressApp.Web.SystemModule;

namespace TestXafSolution2.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class AreaHistoryController : ViewController
    {
        public AreaHistoryController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void AreaHistoryAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            var paramValue = e.ParameterCurrentValue;
            

            ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("([Create_Area] <= ? And [Delete_Area] >= ?) " +
                                                                                           "Or ([Create_Area] <= ? And [Delete_Area] Is Null)",
                                                                                           paramValue, paramValue, paramValue);
        }
    }
}
