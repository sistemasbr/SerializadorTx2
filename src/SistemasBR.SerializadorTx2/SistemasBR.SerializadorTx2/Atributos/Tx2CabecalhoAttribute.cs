using System;

namespace SistemasBR.SerializadorTx2.Atributos
{
    public class Tx2CabecalhoAttribute : Attribute
    {
        internal string NomeCabecalho { get; }

        public Tx2CabecalhoAttribute(string nomeCabecalho) =>
            NomeCabecalho = nomeCabecalho;
    }
}
