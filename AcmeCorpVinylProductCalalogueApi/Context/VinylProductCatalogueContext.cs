using AcmeCorpVinylProductCalalogueApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorpVinylProductCalalogueApi.Context
{
    public class VinylProductCatalogueContext : DbContext
    {
        public VinylProductCatalogueContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<VinylAlbum> VinylAlbums { get; set; }
    }
}
