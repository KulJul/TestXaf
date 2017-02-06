using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl;

namespace TestXafSolution2.Module.TestWork2
{

    /// <summary>
    /// Класс склада
    /// </summary>
    [DefaultClassOptions, ImageName("Bo_Store")]
    public partial class Store
    {

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="session">Сессия</param>
        public Store(Session session) : base(session) { }
        

        /// <summary>
        /// Метод удаления
        /// </summary>
        protected override void OnDeleting()
        {
            //Проверка существования пикетов на складе при удалении
            var PicketFilter = new XPCollection<Picket>(this.Session, CriteriaOperator.Parse("NumberStore == " + this.Number));

            if (PicketFilter.Count != 0)
                throw new UserFriendlyException(new Exception(" Error : " + "Нельзя удалить, так как на складе находятся пикеты"));

            base.OnDeleting();
        }


        /// <summary>
        /// История склада
        /// </summary>
        private XPCollection<AuditDataItemPersistent> auditTrail;

        /// <summary>
        /// Свойство истории склада
        /// </summary>
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (auditTrail == null)
                {
                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(this.Session, this);
                }
                return auditTrail;
            }
        }
    }

}
