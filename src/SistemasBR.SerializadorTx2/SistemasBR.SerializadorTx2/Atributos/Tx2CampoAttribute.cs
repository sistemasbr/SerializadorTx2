using System;

namespace SistemasBR.SerializadorTx2.Atributos
{
    public class Tx2CampoAttribute : Attribute
    {
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

        private string NomeCorrespondente { get; }
        private bool Obrigatorio { get; }
        private int TamanhoMaximo { get; }
    }
}
