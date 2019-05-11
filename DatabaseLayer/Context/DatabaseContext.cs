﻿using DataTransferObjects.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseLayer.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=JiraDB") { }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Priority> Priority { get; set; }
        public virtual DbSet<Queue> Queues { get; set; }
        public virtual DbSet<Rule> Rule { get; set; }
        public virtual DbSet<Quest> Quests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}