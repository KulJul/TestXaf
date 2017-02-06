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

namespace TestXafSolution2.Module.Controllers
{

    /// <summary>
    /// Класс контроллера поиска склада по имени
    /// </summary>
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FindFieldData : ViewController
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public FindFieldData()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }


        /// <summary>
        /// Метод поиска склада по имени
        /// </summary>        
        /// <param name="e">Аргументы поступающие в параметризированный action, включая дату</param>
        private void FindFieldDataAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            try
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace();
                string paramValue = e.ParameterCurrentValue as string;
                object obj = objectSpace.FindObject(((ListView)View).ObjectTypeInfo.Type,
                    CriteriaOperator.Parse(string.Format("Contains([Name], '{0}')", paramValue)));
                
                if (obj != null)
                {
                    DetailView detailView = Application.CreateDetailView(objectSpace, obj);
                    detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                    e.ShowViewParameters.CreatedView = detailView;
                }
            }
            catch (Exception ex)
            {
                Tracing.Tracer.LogText(ex.ToString() + "Action : Find Field Data");
            }
        }
        
    }
}
