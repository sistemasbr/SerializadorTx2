using SistemasBR.SerializadorTx2.Atributos;

namespace ConsumidorSerializador
{
    [Tx2Cabecalho("")]
    public class Mock
    {
        [Tx2Campo("Arroz_Integral", true)]
        public string Teste { get; set; }

        [Tx2Campo("Epa", false, 5)]
        public int Teste2 { get; set; }

        public string Nao { get; set; }

        [Tx2Campo("", false, 2)]
        public string Essevai { get; set; }
    }
}
