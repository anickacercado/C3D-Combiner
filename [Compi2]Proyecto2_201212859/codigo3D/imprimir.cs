﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi2_Proyecto2_201212859.codigo3D
{
    class imprimir
    {
        public expresion expresion;
        public simbolo padre = null;

        public imprimir(expresion expresion) {
            this.expresion = expresion;
        }
    }
}