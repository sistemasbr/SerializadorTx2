using SistemasBR.SerializadorTx2.Atributos;

namespace ConsumidorSerializador
{
    [Tx2Cabecalho("0000")]
    public class Mock
    {
        [Tx2Campo("AMD_11", false)]
        public string Teste { get; set; }

        [Tx2Campo("CONS_111", false, 5)]
        public int Teste2 { get; set; }

        public string Nao { get; set; }
    }
}
