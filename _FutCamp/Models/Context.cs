using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CampeonatoFut.Models
    //conexão com meu db.
{
    public class TabelaContext : DbContext
    {
        public DbSet <Tabela> TB_Brasileirao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder db)
        {
            db.UseSqlServer(connectionString: @"Server=N-TIC-4LJRWD3\SQLEXPRESS;Database=FutCamp;Trusted_Connection=True");
        }
    } 
       


   
} 