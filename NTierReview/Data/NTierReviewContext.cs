using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NTierReview.Models;

namespace NTierReview.Data
{
    public class NTierReviewContext : DbContext
    {
        public NTierReviewContext (DbContextOptions<NTierReviewContext> options)
            : base(options)
        {
        }

        public NTierReviewContext()
        {

        }

        public virtual DbSet<NTierReview.Models.Employee> Employee { get; set; } = default!;
    }
}
