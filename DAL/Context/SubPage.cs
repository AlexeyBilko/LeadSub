﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class SubPage
    {
        public int Id { get; set; }
        public string? InstagramLink { get; set; }
        public string? MaterialLink { get; set; }
        public string? Title { get; set; }
        public string? Avatar { get; set; }
        public string? Header { get; set; }
        public string? Description { get; set; }
        public string? GetButtonTitle { get; set; }
        public string? MainImage { get; set; }
        public string? SuccessDescription { get; set; }
        public string? SuccessButtonTitle { get; set; }
        public int SubscriptionsCount { get; set; }
        public int ViewsCount { get; set; }
        public DateTime CreationDate { get; set; }
        public string? UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BilledPages> BilledPages { get; set; } = new HashSet<BilledPages>();
    }
}
