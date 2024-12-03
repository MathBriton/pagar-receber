using System;
using System.Collections.Generic;

namespace ContasAPagarEReceber
{
    public enum TipoConta
    {
        Pagar,
        Receber
    }

    public abstract class Conta
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool EstaPago { get; private set; }

        public void Pagar()
        {
            EstaPago = true;
            Console.WriteLine($"Conta '{Descricao}' foi marcada como paga.");
        }
    }

    public class ContaPagar : Conta
    {
        public string Fornecedor { get; set; }
    }

    public class ContaReceber : Conta
    {
        public string Cliente { get; set; }
    }

    public class SistemaContas
    {
        private List<Conta> contas = new List<Conta>();
        private int proximoId = 1;

        public void AdicionarConta(Conta conta)
        {
            conta.Id = proximoId++;
            contas.Add(conta);
            Console.WriteLine($"Conta adicionada com sucesso! ID: {conta.Id}");
        }

        public void ListarContas()
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta registrada.");
                return;
            }

            foreach (var conta in contas)
            {
                Console.WriteLine($"ID: {conta.Id} | Descrição: {conta.Descricao} | Valor: {conta.Valor:C} | Vencimento: {conta.DataVencimento:dd/MM/yyyy} | Tipo: {(conta is ContaPagar ? "Pagar" : "Receber")} | Status: {(conta.EstaPago ? "Pago" : "Pendente")}");
            }
        }

        public void PagarConta(int id)
        {
            var conta = contas.Find(c => c.Id == id);
            if (conta == null)
            {
                Console.WriteLine("Conta não encontrada.");
                return;
            }

            if (conta.EstaPago)
            {
                Console.WriteLine("Conta já está paga.");
                return;
            }

            conta.Pagar();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SistemaContas sistema = new SistemaContas();

            while (true)
            {
                Console.WriteLine("\n===== Menu Contas a Pagar e Receber =====");
                Console.WriteLine("1. Adicionar Conta a Pagar");
                Console.WriteLine("2. Adicionar Conta a Receber");
                Console.WriteLine("3. Listar Contas");
                Console.WriteLine("4. Pagar Conta");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Write("Descrição: ");
                        string descPagar = Console.ReadLine();
                        Console.Write("Fornecedor: ");
                        string fornecedor = Console.ReadLine();
                        Console.Write("Valor: ");
                        decimal valorPagar = decimal.Parse(Console.ReadLine());
                        Console.Write("Data de Vencimento (dd/MM/yyyy): ");
                        DateTime vencPagar = DateTime.Parse(Console.ReadLine());

                        var contaPagar = new ContaPagar
                        {
                            Descricao = descPagar,
                            Fornecedor = fornecedor,
                            Valor = valorPagar,
                            DataVencimento = vencPagar
                        };
                        sistema.AdicionarConta(contaPagar);
                        break;

                    case "2":
                        Console.Write("Descrição: ");
                        string descReceber = Console.ReadLine();
                        Console.Write("Cliente: ");
                        string cliente = Console.ReadLine();
                        Console.Write("Valor: ");
                        decimal valorReceber = decimal.Parse(Console.ReadLine());
                        Console.Write("Data de Vencimento (dd/MM/yyyy): ");
                        DateTime vencReceber = DateTime.Parse(Console.ReadLine());

                        var contaReceber = new ContaReceber
                        {
                            Descricao = descReceber,
                            Cliente = cliente,
                            Valor = valorReceber,
                            DataVencimento = vencReceber
                        };
                        sistema.AdicionarConta(contaReceber);
                        break;

                    case "3":
                        sistema.ListarContas();
                        break;

                    case "4":
                        Console.Write("ID da conta a pagar: ");
                        int id = int.Parse(Console.ReadLine());
                        sistema.PagarConta(id);
                        break;

                    case "5":
                        Console.WriteLine("Saindo do sistema...");
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
    }
}
