using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class UrlInfo
    {
        public int Key { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Url { get; set; }
        public string UrlHash { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public long Clicks { get; set; }
        public DateTime LastClicked { get; set; }
        public string Title { get; set; }
    }
}
