namespace TestXafSolution2.Module.Controllers
{
    partial class CargoHistoryController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CargoHistoryAction = new DevExpress.ExpressApp.Actions.ParametrizedAction(this.components);
            // 
            // CargoHistoryAction
            // 
            this.CargoHistoryAction.Caption = "Cargo History Action";
            this.CargoHistoryAction.ConfirmationMessage = null;
            this.CargoHistoryAction.Id = "CargoHistoryAction";
            this.CargoHistoryAction.NullValuePrompt = null;
            this.CargoHistoryAction.ShortCaption = null;
            this.CargoHistoryAction.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.CargoHistoryAction.ToolTip = null;
            this.CargoHistoryAction.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.CargoHistoryAction.ValueType = typeof(System.DateTime);
            this.CargoHistoryAction.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(this.CargoHistoryAction_Execute);
            // 
            // CargoHistoryController
            // 
            this.Actions.Add(this.CargoHistoryAction);
            this.TargetViewId = "CargoHistory_ListView";
            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.ParametrizedAction CargoHistoryAction;
    }
}
