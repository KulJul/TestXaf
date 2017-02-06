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

namespace TestXafSolution2.Module.Controllers
{

    /// <summary>
    /// Класс контроллера поиска объекта
    /// </summary>
    /// 
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FilterController : DevExpress.ExpressApp.SystemModule.FilterController
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public FilterController()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод активации контроллера
        /// </summary>
        protected override void OnActivated()
        {
            //Тк в данной версии XAF (в win приложении) данный элемент работает с ошибками, его нужно отключить
            base.OnActivated();
            this.Active.SetItemValue("Error", false);
        }

    }
}
