//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Casino.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int Id { get; set; }
        public decimal MoneyMoving { get; set; }
        public int UserId { get; set; }
        public System.DateTime Date { get; set; }
        public int TypeId { get; set; }
    
        public virtual TransactionType TransactionType { get; set; }
        public virtual User User { get; set; }
    }
}
