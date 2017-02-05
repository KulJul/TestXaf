using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl;
using System.Linq;

namespace TestXafSolution2.Module.TestWork2
{

    [DefaultClassOptions, ImageName("Bo_Picket")]
    public partial class Picket
    {
        public Picket(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }


        protected override void OnSaving()
        {
            if(!this.IsDeleted)
            {
                // Проверки существования склада
                if (this.NumberStore == null)
                    throw new UserFriendlyException(new Exception(" Error : " + "Нет склада"));

                
                // Делаем проверку на вход. данные, либо 1 элемент
                int number = 0;
                if (!int.TryParse(this.Name, out number))
                    throw new UserFriendlyException(new Exception(" Error : Не верный формат пикета." + 
                                                                  " Он должен быть числом"));
                
                // Пикеты должны принадлежать 1 складу
                if (this.NumberArea != null)
                {
                    var AreaFilter = new XPCollection<Area>(this.Session, CriteriaOperator.Parse("Number == " + this.NumberArea.Number));
                    if (AreaFilter.Count != 0 && AreaFilter[0].Pickets.Select(p => p.NumberStore).Distinct().Count() != 1)
                        throw new UserFriendlyException(new Exception(" Error : " + "пикеты должны находится на 1 складе"));
                }
            }

            base.OnSaving();
        }

        protected override void OnDeleting()
        {
            if (this.NumberArea != null)
            {
                //Проверка операции удаления пикетов с площадок
                var AreaFilter = new XPCollection<Area>(this.Session, CriteriaOperator.Parse("Number == " + this.NumberArea.Number));

                
                if (AreaFilter.Count != 0 && AreaFilter[0].Cargoes.Count != 0 && AreaFilter[0].Cargoes.Any(p => p.Delete_Cargo == DateTime.MinValue))
                    throw new UserFriendlyException(new Exception(" Error : " + "На площадке есть груз, уменьшение кол-ва пикетов невозможно"));


                if (AreaFilter[0].Pickets.Count < 2)
                    throw new UserFriendlyException(new Exception(" Error : " + "На площадке должен быть хотя бы 1 пикет, уменьшение кол-ва пикетов невозможно"));
                
            }

            base.OnDeleting();
        }
        
        private XPCollection<AuditDataItemPersistent> auditTrail;
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
