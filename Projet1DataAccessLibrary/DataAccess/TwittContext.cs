﻿using Microsoft.EntityFrameworkCore;
using Projet1DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1DataAccessLibrary.DataAccess
{
    /// <summary>
    /// Klasa oblugujaca baze danych w Enity Framework
    /// </summary>
    public class TwittContext: DbContext
    {
        public TwittContext(DbContextOptions options) : base(options) { }
        public DbSet<DBTwitt> Twitt { get; set; }
    }
}
