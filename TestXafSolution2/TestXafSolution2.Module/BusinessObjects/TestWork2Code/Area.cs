using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp;
using System.Linq;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Actions;
using TestXafSolution2.Module.TestWork2;
using DevExpress.ExpressApp.DC;

namespace TestXafSolution2.Module.TestWork2
{
    /// <summary>
    /// Класс площадки
    /// </summary>
    [DefaultClassOptions, ImageName("Bo_Area")]
    [Appearance("Delete", TargetItems = "*", Criteria = "Delete_Area > null && Delete_Area >= Create_Area && Create_Area > null",
                                                                                       BackColor = "MistyRose", Enabled = false)]
    [RuleCriteria("Delete_Area >= Create_Area")]
    public partial class Area
    {
        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="session">Сессия</param>
        public Area(Session session) : base(session) { this.Create_Area = DateTime.Now; }

        
        /// <summary>
        /// Перечесление пикетов в виде строки
        /// </summary>
        [Browsable(false)]
        public string PicketsString
        {
            get
            {
                return string.Join(",", Pickets.Select(pt => pt.Name));
            }
        }


        /// <summary>
        /// Метод удаления
        /// </summary>
        protected override void OnDeleting()
        {
            //Проверка существования груза на площадке. 
            var CargoFilter = new XPCollection<Cargo>(this.Session, CriteriaOperator.Parse("Number_Area == " + this.Number));
            
            if (CargoFilter.Count != 0)
                throw new UserFriendlyException(new Exception(" Error : " + "На площадке лежит груз"));
            
           
            // Обнуления связи с пикетами при удалении площадки 
            var PicketFilter = new XPCollection<Picket>(this.Session, CriteriaOperator.Parse("NumberArea == " + this.Number));
            
            foreach (var pic in PicketFilter)
                pic.NumberArea = null;

            base.OnDeleting();
        }


        /// <summary>
        /// Метод сохранения
        /// </summary>
        protected override void OnSaving()
        {            

            if (!this.IsDeleted)
            {
                // Если площадка удаляется, то происходит проверка на существование груза на площадке
                // Если площадка удаляется, то происходит  проверка на существование пикетов на площадке

                if (this.Delete_Area.CompareTo(default(DateTime)) != 0)
                {
                    var CargoFilter = new XPCollection<Cargo>(this.Session, CriteriaOperator.Parse("Number_Area == " + this.Number));
                    var PicketFilter = new XPCollection<Picket>(this.Session, CriteriaOperator.Parse("NumberArea == " + this.Number));

                    if (CargoFilter.Count != 0 && CargoFilter.Any(p => p.Delete_Cargo == DateTime.MinValue))
                        throw new UserFriendlyException(new Exception(" Error : " + "Груз лежит на площадке"));


                    if (CargoFilter.Count != 0 && CargoFilter.Any(p => p.Delete_Cargo > this.Delete_Area))
                        throw new UserFriendlyException(new Exception(" Error : " + "Груз отправляется позже, чем будет закрыта площадка"));

                    // добавление в историю
                    this.NameDelPicketHistory = string.Join(",", PicketFilter.Select(pt => pt.Name.Trim()));
                    this.NameDelStore = PicketFilter[0].NumberStore.Name;

                    foreach (var pic in PicketFilter)
                        pic.NumberArea = null;

                    return;
                }

                // Проверка наличия пикетов на площадке
                if (this.Pickets.Count == 0)
                    throw new UserFriendlyException(new Exception(" Error : " + "На площадке должен быть минимум 1 пикет"));

                // нельзя разорвать диапазон. проверка
                if (this.Pickets != null)
                {
                    var picCollection = this.Pickets.Select(pic => Convert.ToInt32(pic.Name)).ToList();

                    var query = picCollection.OrderBy(name => name).ToList();

                    for (var p = 0; p < query.Count - 1; p++)
                    {
                        if (query[p + 1] - query[p] != 1)
                            throw new UserFriendlyException(new Exception(" Error : " + "пикеты должны быть не разрывными"));
                    }

                    // Пикеты должны принадлежать 1 складу
                    if (this.Pickets.Select(p=>p.NumberStore).Distinct().Count() != 1)
                        throw new UserFriendlyException(new Exception(" Error : " + "Пикет принадлежит площадке на которой лежит груз"));
                    
                }
            }

            base.OnSaving();
        }

        
        /// <summary>
        /// История площадки
        /// </summary>
        private XPCollection<AuditDataItemPersistent> auditTrail;


        /// <summary>
        /// Свойство истории площадки
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
