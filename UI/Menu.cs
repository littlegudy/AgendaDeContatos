using AgendaDeContatos.Repositories;
using AgendaDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;



namespace AgendaDeContatos.UI;


internal class Menu
{

    RepositorioContato repositorio = RepositorioContato.Instancia;

    public void ExibirMenu()
    {
        while(true)
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Contatos ===");
            Console.WriteLine("1. Adicionar Contato");
            Console.WriteLine("2. Listar Contatos");
            Console.WriteLine("3. Editar Contatos");
            Console.WriteLine("4. Deletar Contatos");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out int opcao))
            {
                Console.WriteLine("Opção inválida!");
            }

                switch (opcao)
            {
                case 1:
                    Console.Clear();
                    AdicionarContato();
                    break;
                case 2:
                    Console.Clear();
                    ListarContatos();
                    break;
                case 3:
                    Console.Clear();
                    EditarContatos();
                    break;
                case 4:
                    Console.Clear();
                    DeletarContatos();
                    break;
                case 5:
                    Console.Clear();
                    Sair();
                    break;
                default:
                    Console.WriteLine("Opção inexistente!");
                    break;
            }
        } 
    }

    public void AdicionarContato()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine()!;
        Console.Write("Telefone: ");
        string telefone = Console.ReadLine()!;
        Console.Write("E-mail: ");
        string email = Console.ReadLine()!;

        var c = new Contato {
            Nome = nome,
            Telefone = telefone,
            Email = email
        };
        repositorio.AdicionarContato(c);
        Console.WriteLine("Contato adicionado com sucesso!");
        Console.ReadKey();
        Console.Clear();
        return;

    }

    private void Pausar()
    {
        Console.ReadKey();
        Console.Clear();
        return;
    }

    public void ListarContatos()
    {
        var lista = repositorio.GetAll();
        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum contato encontrado.");
            Console.ReadKey();   
            Console.Clear();     
            return;
        }

        Console.WriteLine("**** LISTA DE CONTATOS ****");
        foreach (var contato in lista)
        {
            Console.WriteLine($"ID: {contato.Id} - Nome: {contato.Nome} - Tel: {contato.Telefone} - Email: {contato.Email}");      
        }
        Pausar();

    }

    public void EditarContatos()
    {
        Console.Write("Digite o ID do contato que deseja editar: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        var contato = repositorio.GetById(id);

        if (contato == null)
        {
            Console.WriteLine("Contato não encontrado.");
            return;
        }

        Console.Write("Digite o novo nome (deixe em branco para manter o atual): ");
        string nome = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(nome))
        {
            contato.Nome = nome;
        }

        Console.Write("Digite o novo telefone (deixe em branco para manter o atual): ");
        string telefone = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(telefone))
        {
            contato.Telefone = telefone;
        }

        Console.Write("Digite o novo e-mail (deixe em branco para manter o atual): ");
        string email = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(email))
        {
            contato.Email = email;
        }

        repositorio.Update(contato);
        Console.WriteLine("Contato editado com sucesso!");

        Pausar();



    }

    public void DeletarContatos()
    {
        Console.Write("Digite o ID do contato que deseja deletar: ");
        int id = int.Parse(Console.ReadLine()!);
        var contato = repositorio.GetById(id);
        if (contato == null)
        {
            Console.WriteLine("Contato não encontrado.");
            return;
        }

        Console.WriteLine($"Tem certeza de que deseja deletar o contato de {contato.Nome}? (s/n)");
        string resp = Console.ReadLine()!;
        if (resp != "s")
        {
            Console.WriteLine("Operação cancelada!");
            return;
        }
        repositorio.Delete(id);
        Console.WriteLine("Contato deletado com sucesso!");
        Pausar();
    }

    public void Sair()
    {
        Console.WriteLine("Saindo do programa. Até mais!");
        Environment.Exit(0);
    }
}
