using SistemasBR.SerializadorTx2.Atributos;

namespace ConsumidorSerializador
{
    public class Mock
    {
        [Tx2Campo("", true)]
        public string Teste { get; set; }

        [Tx2Campo("nome", false, 5)]
        public int Teste2 { get; set; }

        public string Nao { get; set; }

        [Tx2Campo("Outro_campo", false, 2)]
        public string Essevai { get; set; }
    }
}
