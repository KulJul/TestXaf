namespace TestXafSolution2.Module.Controllers
{
    partial class AreaHistoryController
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
            this.AreaHistoryAction = new DevExpress.ExpressApp.Actions.ParametrizedAction(this.components);
            // 
            // AreaHistoryAction
            // 
            this.AreaHistoryAction.Caption = "Area History Action";
            this.AreaHistoryAction.ConfirmationMessage = null;
            this.AreaHistoryAction.Id = "AreaHistoryAction";
            this.AreaHistoryAction.NullValuePrompt = null;
            this.AreaHistoryAction.ShortCaption = null;
            this.AreaHistoryAction.ToolTip = null;
            this.AreaHistoryAction.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.AreaHistoryAction.ValueType = typeof(System.DateTime);
            this.AreaHistoryAction.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(this.AreaHistoryAction_Execute);
            // 
            // AreaHistoryController
            // 
            this.Actions.Add(this.AreaHistoryAction);
            this.TargetViewId = "AreaHistory_ListView";
            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.ParametrizedAction AreaHistoryAction;
    }
}
