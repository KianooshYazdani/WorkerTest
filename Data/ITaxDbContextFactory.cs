using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxService.Data
{
    public interface ITaxDbContextFactory
    {
        TaxDbContext CreateDbContext();
    }
}
