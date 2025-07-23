using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MinervaFoods.Domain.Common;
using MinervaFoods.Helpers.Security;
using System.Reflection;

namespace MinervaFoods.Data
{
    public class DefaultContext : DbContext
    {
        #region Fields
        public DbSet<Domain.Entities.Carne> Carnes { get; set; }
        public DbSet<Domain.Entities.Comprador> Compradores { get; set; }
        public DbSet<Domain.Entities.Pedido> Pedidos { get; set; }
        public DbSet<Domain.Entities.PedidoItem> PedidosItens { get; set; }
        public DbSet<Domain.Entities.Pais> Paises { get; set; }
        public DbSet<Domain.Entities.Estado> Estados { get; set; }


        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAuthenticate _userAuthenticate;
        #endregion

        #region Constructor
        public DefaultContext(DbContextOptions<DefaultContext> options,
        IHttpContextAccessor httpContextAccessor,
        IUserAuthenticate userAuthenticate) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _userAuthenticate = userAuthenticate;
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Atenção quando tiver authenticação somente colocar o id do usuario autenticado do claims
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (!ChangeTracker.HasChanges())
                return base.SaveChangesAsync(cancellationToken);

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.Entity == null)
                    continue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Id = Guid.NewGuid();
                        entry.Entity.Status = true;
                        entry.Entity.IdUserCreated = _userAuthenticate.UserId;
                        entry.Entity.CreatedAt = DateTime.Now;

                        break;

                    case EntityState.Modified:
                        entry.Entity.IdUserUpdated = _userAuthenticate.UserId;
                        entry.Entity.UpdatedAt = DateTime.Now;

                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


    }
    public class DefaultContextFactory : IDesignTimeDbContextFactory<DefaultContext>
    {
        public DefaultContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DefaultContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(
                   connectionString,
                   b => b.MigrationsAssembly("MinervaFoods.Data")
            );

            var httpContextAccessor = new HttpContextAccessor();
            var httpContextUserAuthenticate = new HttpContextUserAuthenticate();
            return new DefaultContext(builder.Options, httpContextAccessor, httpContextUserAuthenticate);
        }
    }
}
