using System;

namespace SistemasBR.SerializadorTx2.Atributos
{
    public class Tx2CampoAttribute : Attribute
    {
        public Tx2CampoAttribute(string nomeCorrespondente, bool obrigatorio, int tamanhoMaximo)
        {
        }

        public Tx2CampoAttribute(string nomeCorrespondente, bool obrigatorio)
        {
        }
    }
}
