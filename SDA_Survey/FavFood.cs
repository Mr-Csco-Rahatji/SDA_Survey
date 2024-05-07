using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Survey
{
    enum Type
    {
        PIZZA,
        PASTA,
        PAP_AND_WORS,
        OTHER
    }

    class FavFood
    {
        public Type type {  get; set; }

        public FavFood(Type type) {  this.type = type; }
    }
}
