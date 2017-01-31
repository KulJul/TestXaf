﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace TestXafSolution2.Module.TestWork2
{

    public partial class Store : XPLiteObject
    {
        int fNumber;
        [Key(true)]
        [Browsable(false)]
        public int Number
        {
            get { return fNumber; }
            set { SetPropertyValue<int>("Number", ref fNumber, value); }
        }
        string fName;
        [Size(10)]
        [DevExpress.Persistent.Validation.RuleRequiredField, DevExpress.Persistent.Validation.RuleUniqueValue("", DevExpress.Persistent.Validation.DefaultContexts.Save,
   CriteriaEvaluationBehavior = DevExpress.Persistent.Validation.CriteriaEvaluationBehavior.BeforeTransaction)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        [Association(@"PicketReferencesStore")]
        public XPCollection<Picket> Pickets { get { return GetCollection<Picket>("Pickets"); } }
    }

}
