//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectowebB.ModeloBD
{
    using System;
    using System.Collections.Generic;
    
    public partial class Errors
    {
        public long id_error { get; set; }
        public string description { get; set; }
        public System.DateTime date_time { get; set; }
        public string origin { get; set; }
        public int id_user { get; set; }
    
        public virtual User_tb User_tb { get; set; }
    }
}
