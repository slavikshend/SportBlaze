﻿using System.ComponentModel.DataAnnotations.Schema;
using webapi.DAL.Entities.Main;

namespace webapi.DAL.Entities.MN
{
    [Table("payments")]
    public class Payment : Entity
    {
        [ForeignKey("User")]
        [Column("user_email")]
        public string UserEmail { get; set; }

        [Column("order_id")]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Column("payment_amount")]
        public decimal PaymentAmount { get; set; }

        [Column("payment_date")]
        public DateTimeOffset PaymentDate { get; set; }

        public User User { get; set; }
        public Order Order { get; set; }
    }
}