using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace bright.Models
{
    public class User
    {
        [Key]
        public int userid { get; set; }
        public string name { get; set; }
        public string alias { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at { get; set; }
        public List<Like> Liking { get; set; }

        public User()
        {
            Liking = new List<Like>();
        }


    }
}