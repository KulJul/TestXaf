﻿using System;
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
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class UnLinikWebController : WebLinkUnlinkController
    {
        public UnLinikWebController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        
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


                this.ObjectSpace.Refresh();

            }
            catch (UserFriendlyException e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        protected override void Link(PopupWindowShowActionExecuteEventArgs args)
        {
            try
            {
                base.Link(args);

                this.ObjectSpace.Refresh();
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
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
    }
}
