//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContactDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Email
    {
        public System.Guid id { get; set; }
        public System.Guid contactId { get; set; }
        public string primaryEmail { get; set; }
        public string secondaryEmail { get; set; }
    
        public virtual Contact Contact { get; set; }
    }
}
