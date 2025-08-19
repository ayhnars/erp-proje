using System.Collections.Generic;
using Entities;


namespace ErpProject.Repository.Config
{
    public static class ModulesSeedData
    {
        public static List<Modules> GetModules()
        {
            return new List<Modules>
            {
                new Modules { Id = 1, ModuleName = "Order", ModuleDescription = "Sales management module", Price = "100" },

            };
        }
    }
}