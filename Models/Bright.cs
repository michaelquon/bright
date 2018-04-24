using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace bright.Models
{
    public class Bright
    {
        [Key]
        public int postid { get; set; }
        public string post { get; set; }
             
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at { get; set; }
        [ForeignKey("users")]
        public int likebyid { get; set; }
        public User users {get; set; }
        public List<Like> Likers { get; set; }
        public Bright()
        {
            Likers = new List<Like>();
        }

    }
}