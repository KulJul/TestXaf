using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;

namespace TestXafSolution2.Module.TestWork2
{
    /// <summary>
    /// Класс товара
    /// </summary>
    [DefaultClassOptions, ImageName("Bo_Cargo")]
    [Appearance("Delete", TargetItems = "*", Criteria = "Delete_Cargo > null && Delete_Cargo >= Create_Cargo && Create_Cargo > null",
                                                                                           BackColor = "MistyRose", Enabled = false)]
    [RuleCriteria("Delete_Cargo >= Create_Cargo")]
    public partial class Cargo
    {

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="session">Сессия</param>
        public Cargo(Session session) : base(session) { this.Create_Cargo = DateTime.Now; }
        

        /// <summary>
        /// Метод сохранения
        /// </summary>
        protected override void OnSaving()
        {
            if (this.Delete_Cargo.CompareTo(default(DateTime)) != 0)
            {
                //Добавления истории об удаленном грузе
                var areaFilter = new XPCollection<Area>(this.Session, CriteriaOperator.Parse("Number == " + this.Number_Area.Number));
                
                this.NameDelStore = areaFilter[0].Pickets[0].NumberStore.Name;
            }


        }

        /// <summary>
        /// История товара
        /// </summary>
        private XPCollection<AuditDataItemPersistent> auditTrail;


        /// <summary>
        /// Свойство истории товара
        /// </summary>
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (auditTrail == null)
                {
                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return auditTrail;
            }
        }
    }   
        

}
