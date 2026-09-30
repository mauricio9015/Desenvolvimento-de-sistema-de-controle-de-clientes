namespace Atividade
{
    // Herança: Pessoa_Fisica é um Clientes. Usa o Pagar_Imposto da classe-pai (10%).
    class Pessoa_Fisica : Clientes
    {
        public string cpf { get; set; }
        public string rg { get; set; }
    }
}
