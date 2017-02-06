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
using DevExpress.ExpressApp.DC;

namespace TestXafSolution2.Module.Controllers
{

    /// <summary>
    /// Класс контроллера обнуления полей detail view
    /// </summary>
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ClearFields : ViewController
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ClearFields()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        /// <summary>
        /// Метод обнуления полей
        /// </summary>
        /// <param name="e">Аргументы поступающие в параметризированный action</param>
        private void ClearFieldsAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                foreach (PropertyEditor item in ((DetailView)View).GetItems<PropertyEditor>())
                {
                    if (item.AllowEdit)
                    {
                        try
                        {
                            item.PropertyValue = null;
                        }
                        catch (IntermediateMemberIsNullException)
                        {
                            item.Refresh();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Tracing.Tracer.LogText(ex.ToString() + "Action : Clear Fields");
            }
        }

        /// <summary>
        /// Метод активации контроллера
        /// </summary>
        /// <param name="e">Аргументы поступающие в параметризированный action</param>
        private void ClearFields_Activated(object sender, EventArgs e)
        {
            if (View.GetType() == typeof(DetailView))
            {
                ClearFieldsAction.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                ((DetailView)View).ViewEditModeChanged += new EventHandler<EventArgs>(ClearFieldsController_ViewModeChanged);
            }
            else
            {
                this.Active.SetItemValue("Error", false);
            }
        }


        /// <summary>
        /// Метод для отслеживания изменения типа представления
        /// </summary>
        /// <param name="e">Аргументы поступающие в параметризированный action</param>
        void ClearFieldsController_ViewModeChanged(object sender, EventArgs e)
        {
            ClearFieldsAction.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
        }
    }
}
