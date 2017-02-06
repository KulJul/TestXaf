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
using DevExpress.Xpo;

namespace TestXafSolution2.Module.Controllers
{

    /// <summary>
    /// Класс контроллера истории товара
    /// </summary>
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CargoHistoryController : ViewController
    {
        /// <summary>
        /// Конструктор 
        /// </summary>
        public CargoHistoryController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }


        /// <summary>
        /// Метод поиска товара по дате
        /// </summary>        
        /// <param name="e">Аргументы поступающие в параметризированный action, включая дату</param>
        private void CargoHistoryAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {

            var paramValue = e.ParameterCurrentValue;
            
            ((ListView)View).CollectionSource.Criteria["Filter2"] = CriteriaOperator.Parse("([Create_Cargo] <= ? And [Delete_Cargo] >= ? )" +
                                                                                           "Or ([Create_Cargo] <= ? And [Delete_Cargo] Is Null)", 
                                                                                           paramValue, paramValue, paramValue);
        }
        
    }
}
