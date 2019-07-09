using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMMC.Auth.Web.API.Models.Metadata;
using VMMC.Auth.Web.API.ScaffoldDb;

namespace VMMC.Auth.Web.API.Mapping
{
    /// <summary>
    /// Mapper
    /// </summary>
    public class Mapper
    {
        private Mapper() { }

        /// <summary>
        /// Initialise and instance for single pattern
        /// </summary>
        public static Mapper Instance { get; } = new Mapper();


        public Funders Funder(FunderModel model)
        {
            return new Funders
            {
                Name = model.Name,
                Partners = null
            };
        }
    }
}
