﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class repetir_hasta
    {
        public expresion expresion;
        public ambito ambito;
        public simbolo padre = null;

        public repetir_hasta(expresion expresion, ambito ambito)
        {
            this.expresion = expresion;
            this.ambito = ambito;
        }
    }
}