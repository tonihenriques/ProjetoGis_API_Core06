using Hunger_Map.Entidade;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hunger_Map.Context
{
    public class HungerContext: DbContext
    {
        public HungerContext() { }
        public HungerContext(DbContextOptions<HungerContext> options) : base(options) { }


        public DbSet<Address> Address { get; set; }
        public DbSet<ListAlimentos> ListAlimentos { get; set; }
        public DbSet<DoacaoAnjo> DoacaoAnjo { get; set; }
    }
}
