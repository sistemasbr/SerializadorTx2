using System;

namespace SistemasBR.SerializadorTx2.Atributos
{
    public class Tx2CampoAttribute : Attribute
    {
        internal string NomeCorrespondente { get; }
        internal bool Obrigatorio { get; }
        internal int TamanhoMaximo { get; }

        public Tx2CampoAttribute(string nomeCorrespondente, bool obrigatorio, int tamanhoMaximo)
        {
            NomeCorrespondente = nomeCorrespondente;
            Obrigatorio = obrigatorio;
            TamanhoMaximo = tamanhoMaximo;
        }

        public Tx2CampoAttribute(string nomeCorrespondente, bool obrigatorio)
        {
            NomeCorrespondente = nomeCorrespondente;
            Obrigatorio = obrigatorio;
        }
    }
}
