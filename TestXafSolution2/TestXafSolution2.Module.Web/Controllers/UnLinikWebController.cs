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
using DevExpress.ExpressApp.Web.SystemModule;
using TestXafSolution2.Module.TestWork2;

namespace TestXafSolution2.Module.Web.Controllers
{

    /// <summary>
    /// Класс веб-контроллера добавления и удаления объектов
    /// </summary>
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class UnLinikWebController : WebLinkUnlinkController
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public UnLinikWebController()
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
            try
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
            catch (UserFriendlyException e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        /// <summary>
        /// Метод для добавления объектов в коллекцию
        /// </summary>
        /// <param name="args">Связаных объекты</param>
        protected override void Link(PopupWindowShowActionExecuteEventArgs args)
        {
            try
            {
                base.Link(args);

                this.ObjectSpace.CommitChanges();
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

    }
}
