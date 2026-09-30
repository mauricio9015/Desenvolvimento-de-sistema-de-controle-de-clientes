using System;
using System.Globalization;

namespace Atividade
{
    class Program
    {
        static CultureInfo cultura;

        static void Main(string[] args)
        {
            try { cultura = new CultureInfo("pt-BR"); }
            catch (CultureNotFoundException) { cultura = CultureInfo.CurrentCulture; }

            string continuar;
            do
            {
                Console.WriteLine("\n=== SISTEMA DE CONTROLE DE CLIENTES ===");

                string var_nome = LerTexto("Informar Nome");
                string var_endereco = LerTexto("Informar Endereço");

                string var_tipo;
                do
                {
                    Console.WriteLine("Pessoa Física (f) ou Jurídica (j) ?");
                    var_tipo = Console.ReadLine().Trim().ToLower();
                    if (var_tipo != "f" && var_tipo != "j")
                        Console.WriteLine("Opção inválida. Digite 'f' ou 'j'.");
                } while (var_tipo != "f" && var_tipo != "j");

                // Polimorfismo: a referência é do tipo da classe-pai,
                // o objeto criado depende da escolha do usuário.
                Clientes cliente;

                if (var_tipo == "f")
                {
                    Pessoa_Fisica pf = new Pessoa_Fisica();
                    pf.cpf = LerTexto("Informar CPF:");
                    pf.rg = LerTexto("Informar RG:");
                    cliente = pf;
                }
                else
                {
                    Pessoa_Juridica pj = new Pessoa_Juridica();
                    pj.cnpj = LerTexto("Informar CNPJ:");
                    pj.ie = LerTexto("Informar IE:");
                    cliente = pj;
                }

                cliente.nome = var_nome;
                cliente.endereco = var_endereco;

                float val_pag = LerValor("Informar Valor de Compra:");

                // Chama a versão correta (10% ou 20%) conforme o tipo real do objeto.
                cliente.Pagar_Imposto(val_pag);

                Exibir(cliente);

                Console.WriteLine("\nDeseja cadastrar outro cliente? (s/n)");
                continuar = Console.ReadLine().Trim().ToLower();

            } while (continuar == "s");

            Console.WriteLine("Sistema encerrado.");
        }

        static string LerTexto(string mensagem)
        {
            string texto;
            do
            {
                Console.WriteLine(mensagem);
                texto = Console.ReadLine().Trim();
                if (texto == "")
                    Console.WriteLine("Campo obrigatório.");
            } while (texto == "");
            return texto;
        }

        static float LerValor(string mensagem)
        {
            float valor;
            while (true)
            {
                Console.WriteLine(mensagem);
                string entrada = Console.ReadLine();
                if (float.TryParse(entrada, NumberStyles.Float, cultura, out valor) && valor > 0)
                    return valor;
                Console.WriteLine("Valor inválido. Informe um número maior que zero (ex.: 2500,00).");
            }
        }

        static void Exibir(Clientes c)
        {
            if (c is Pessoa_Fisica pf)
            {
                Console.WriteLine("\n-------- Pessoa Física ---------");
                Console.WriteLine("Nome ..........: " + pf.nome);
                Console.WriteLine("Endereço ......: " + pf.endereco);
                Console.WriteLine("CPF ...........: " + pf.cpf);
                Console.WriteLine("RG ............: " + pf.rg);
            }
            else if (c is Pessoa_Juridica pj)
            {
                Console.WriteLine("\n-------- Pessoa Jurídica ---------");
                Console.WriteLine("Nome ..........: " + pj.nome);
                Console.WriteLine("Endereço ......: " + pj.endereco);
                Console.WriteLine("CNPJ ..........: " + pj.cnpj);
                Console.WriteLine("IE ............: " + pj.ie);
            }
            Console.WriteLine("Valor de Compra: " + c.valor.ToString("C", cultura));
            Console.WriteLine("Imposto .......: " + c.valor_imposto.ToString("C", cultura));
            Console.WriteLine("Total a Pagar .: " + c.total.ToString("C", cultura));
        }
    }
}
