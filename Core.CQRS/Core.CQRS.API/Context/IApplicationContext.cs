using Core.CQRS.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Core.CQRS.API.Context
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }

        Task<int> SaveChanges();
    }
}