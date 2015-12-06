using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using CL.Framework.DataAccess.Interfaces;
using CL.Transverse.Model.Post;
using CL.Transverse.Model.System;

namespace CL.Transverse.Model
{
    public class CLContext : DbContext, IDbContext
    {
        public CLContext()
            : base("Name=CLContext")
        {
            // Without this configure, EF 6 will break even have configure in app/web.config
            // see more: https://farooqmdotcom.wordpress.com/2013/10/24/entity-framework-6-weirdness/
            // Log Entity Framework Query to console of Visual Studio
            //this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s, "Entity Framework Information");

            //RetryDbConfiguration.SuspendExecutionStrategy = true;

            // ROLA - This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
            // As it is installed in the GAC, Copy Local does not work. It is required for probing.
            // Fixed "Provider not loaded" error
            //var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            //this.Configuration.LazyLoadingEnabled = false;

            //configure timeout
            //int sqlTimeout;
            //if (!int.TryParse(System.Configuration.ConfigurationManager.AppSettings["SQLTimeOut"], out sqlTimeout))
            //{
            //    sqlTimeout = 30;
            //}
            //Database.CommandTimeout = sqlTimeout;
        }

        public DbSet<P_Category> Categories { get; set; }
        public DbSet<P_Resource> Resources { get; set; }
        public DbSet<P_Page> Pages { get; set; }
        public DbSet<P_Post> Posts { get; set; }
        public DbSet<S_Configuration> Configurations { get; set; }
        public DbSet<S_User> Users { get; set; }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : class, new()
        {
            //add parameters to command
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        //output parameter
                        commandText += " output";
                    }
                }
            }

            var result = this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();

            return result;
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            IEnumerable<TElement> listData = this.Database.SqlQuery<TElement>(sql, parameters);
            return listData;
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }
    }
}
