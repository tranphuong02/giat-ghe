﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CL.Transverse.Model;

namespace CL.Transverse.Migrations
{
    public class DatabaseMigrations
    {
        public static void ApplyDatabaseMigrations()
        {
            //Configuration is the class created by Enable-Migrations
            DbMigrationsConfiguration dbMgConfig = new Configuration()
            {
                //DbContext subclass generated by EF power tools
                ContextType = typeof(CLContext)
            };
            using (var databaseContext = new CLContext())
            {
                try
                {
                    var database = databaseContext.Database;
                    bool isExistsDatabase = database.Exists();

                    var migrationConfiguration = dbMgConfig;

                    migrationConfiguration.TargetDatabase =
                        new DbConnectionInfo(database.Connection.ConnectionString, "System.Data.SqlClient");
                    var migrator = new DbMigrator(migrationConfiguration);

                    // update or create database
                    migrator.Update();

                    // if database is first initial, then initial data for it
                    if (isExistsDatabase)
                    {
                        return;
                    }

                    //InitUsers(databaseContext);
                }
                catch (AutomaticDataLossException adle)
                {
                    dbMgConfig.AutomaticMigrationDataLossAllowed = true;
                    var mg = new DbMigrator(dbMgConfig);
                    var scriptor = new MigratorScriptingDecorator(mg);
                    string script = scriptor.ScriptUpdate(null, null);
                    throw new Exception(adle.Message + " : " + script);
                }
            }
        }

        #region Private Methods

        //private static void InitUsers(CLContext context)
        //{
        //    User user = new User
        //    {
        //        Username = "Admin",
        //        Password = "e10adc3949ba59abbe56e057f20f883e",
        //        IsDeleted = false,
        //        LastUpdate = DateTime.Now
        //    };
        //    context.Users.AddOrUpdate(user);
        //    context.SaveChanges();
        //}


        #endregion
    }
}
