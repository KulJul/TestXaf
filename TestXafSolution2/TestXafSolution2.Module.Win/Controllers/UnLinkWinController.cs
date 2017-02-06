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

namespace TestXafSolution2.Module.Win.Controllers
{

    /// <summary>
    /// Класс контроллера добавления и удаления объектов
    /// </summary>
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class UnLinkWinController : LinkUnlinkController
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public UnLinkWinController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }


        /// <summary>
        /// Метод для удаления объектов из коллекции
        /// </summary>
        /// <param name="args">Связаные объекты</param>
        protected override void Unlink(SimpleActionExecuteEventArgs args)
        {
            if (View.GetType() == typeof(ListView) && !View.IsRoot && View.ObjectTypeInfo.Type.Name == "Picket")
            {
                // Нельзя отсоединить пикет, если на площадке есть груз
                foreach (object item in args.SelectedObjects)
                {
                    var picket = item as Picket;
                    if (picket.NumberArea == null || picket.NumberArea.Cargoes.Count == 0 || picket.NumberArea.Cargoes.All(p => p.Delete_Cargo != DateTime.MinValue))
                        base.Unlink(args);
                    else
                        throw new UserFriendlyException(new Exception(" Error : " + "Нельзя отсоединить пикет, так как на нем есть груз"));
                }
            }
            else
            {
                base.Unlink(args);
            }

            this.ObjectSpace.CommitChanges();
        }


        /// <summary>
        /// Метод для добавления объектов в коллекцию
        /// </summary>
        /// <param name="args">Связаных объекты</param>
        protected override void Link(PopupWindowShowActionExecuteEventArgs args)
        {
            base.Link(args);

            this.ObjectSpace.CommitChanges();
        }


        /// <summary>
        /// Метод активации контроллера
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            if (View.ObjectTypeInfo.Type.Name == "AuditDataItemPersistent" ||
               (View.GetType() == typeof(ListView) && !View.IsRoot && 
                View.ObjectTypeInfo.Type.Name == "Picket" && View.Id == "Store_Pickets_ListView"))
                this.Active.SetItemValue("Error", false);
        }

    }
}
