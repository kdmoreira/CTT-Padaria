﻿using Padaria.Domain.Interface;
using System;

namespace Padaria.Domain.Model
{
    public class MateriaPrima : IBaseEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Unidades UnidadeMedida { get; set; }
        public float Quantidade { get; set; }
        public bool Ativa { get; set; }
    }
    public enum Unidades
    { 
        Grama, 
        Mililitro, 
        Unidade, 
    }

}

    