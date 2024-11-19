using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace shop_app.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("IdentityUserId")]
        public IdentityUser User { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int Quantity { get; set; }
    }
}
