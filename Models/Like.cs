using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bright.Models
{
    public class Like
    {
        [Key]
        public int likeid { get; set; }
        [ForeignKey("users")]
        public int likersid { get; set; }
        [ForeignKey("posts")]
        public int postsid { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at { get; set; }
        public User users {get; set; }
        public Bright posts {get; set; }
    }
}